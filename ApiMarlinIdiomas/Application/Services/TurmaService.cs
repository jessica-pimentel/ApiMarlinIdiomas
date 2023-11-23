using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.IRepositories;
using Domain.IServices;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly AplicationDbContext _dbContext;

        public TurmaService(ITurmaRepository turmaRepository, AplicationDbContext dbContext)
        {
            _turmaRepository = turmaRepository;
            _dbContext = dbContext;
        }

        public async Task CadastrarTurma(int numero, int anoLetivo)
        {
            var turma = new Turma
        {
            Numero = numero,
            AnoLetivo = anoLetivo
        };

            await _turmaRepository.CadastrarTurma(turma);
        }

        public async Task<bool> AtualizarTurma(Turma turma)
        {
            return await _turmaRepository.AtualizarTurma(turma);
        }

        public async Task<List<Turma>> ObterTodasTurmas()
        {
            return await _turmaRepository.ObterTodasTurmas();
        }

        public async Task<bool> PossuiAlunosMatriculados(int turmaId)
        {
            // validacao para nao excluir turma se tiver aluno
            var turma = await _turmaRepository.ObterTurmaPorId(turmaId);

            if (turma == null)
                throw new InvalidOperationException($"Turma com ID {turmaId} não encontrada.");

            return turma.Alunos.Any(); 
        }

        public async Task<bool> ExcluirTurma(int turmaId)
        {
            var turma = await _turmaRepository.ObterTurmaPorId(turmaId);

            if (turma == null)
                throw new InvalidOperationException($"Turma com ID {turmaId} não encontrada.");

            if (turma.Alunos.Any())
                throw new InvalidOperationException($"A turma {turmaId} possui alunos e não pode ser excluída.");

            return await _turmaRepository.ExcluirTurma(turmaId);
        }

        // Validação do limite de 5 alunos por turma
        public async Task<int> ObterQuantidadeAlunosNaTurma(int turmaId)
        {

            return await _dbContext.Matriculas
            .Where(m => m.TurmaId == turmaId)
            .CountAsync();

        }

        public async Task CarregarAlunosAsync(Turma turma)
        {
            await _dbContext.Entry(turma).Collection(t => t.Alunos).LoadAsync();
        }

        public async Task<bool> TurmaExiste(int turmaId)
        {
            return await Task.Run(() => _dbContext.Turmas.Any(t => t.TurmaId == turmaId));
        }

        public async Task<Turma> ObterTurmaPorId(int turmaId)
        {

           return await _dbContext.Turmas
            .Include(t => t.Alunos)
            .FirstOrDefaultAsync(t => t.TurmaId == turmaId);
        }

        public async Task<Turma> ObterTurmaPorNumero(int numero)
        {
            return await _dbContext.Turmas
                .FirstOrDefaultAsync(t => t.Numero == numero);
        }
    }
}