using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.IRepositories;
using Domain.IServices;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly ITurmaService _turmaService;
        private readonly AplicationDbContext _dbContext;

        public MatriculaService(IMatriculaRepository matriculaRepository, ITurmaService turmaService, AplicationDbContext dbContext)
        {
            _matriculaRepository = matriculaRepository;
            _turmaService = turmaService;
            _dbContext = dbContext;
        }

        public async Task<bool> AtualizarMatricula(Matricula matricula)
        {
            return await _matriculaRepository.AtualizarMatricula(matricula);
        }

        public async Task<List<Matricula>> ObterTodasMatriculas()
        {
            return await _matriculaRepository.ObterTodasMatriculas();
        }

        public async Task<bool> ExcluirMatricula(int matriculaId)
        {
            return await _matriculaRepository.ExcluirMatricula(matriculaId);
        }

        public async Task<bool> AlunoEstaMatriculado(int alunoId)
        {
            return await _dbContext.Matriculas.AnyAsync(m => m.AlunoId == alunoId);
        }

        public async Task<Matricula> ObterMatriculaPorId(int matriculaId)
        {
            return await _dbContext.Matriculas.FindAsync(matriculaId);
        }

        public async Task<int> ObterQuantidadeAlunosNaTurma(int turmaId)
        {
            return await _dbContext.Turmas
                .Where(t => t.TurmaId == turmaId)
                .SelectMany(t => t.Alunos)
                .CountAsync();
        }


        public async Task<Matricula> ObterMatriculaPorAlunoETurmaId(int alunoId, int turmaId)
        {
            return await _dbContext.Matriculas
                .FirstOrDefaultAsync(m => m.AlunoId == alunoId && m.TurmaId == turmaId);
        }

        public async Task CadastrarMatricula(int alunoId, int turmaId)
        {
            var matricula = new Matricula
            {
                AlunoId = alunoId,
                TurmaId = turmaId
            };

            _dbContext.Matriculas.Add(matricula);
            await _dbContext.SaveChangesAsync();
        }
    }
}