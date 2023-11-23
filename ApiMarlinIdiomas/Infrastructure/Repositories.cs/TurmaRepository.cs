using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.IRepositories;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly AplicationDbContext _dbContext;

        public TurmaRepository(AplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CadastrarTurma(Turma turma)
        {
            _dbContext.Turmas.Add(turma);
            await _dbContext.SaveChangesAsync();
        }

        // Validação do limite de 5 alunos por turma
        public async Task<int> ObterQuantidadeAlunosNaTurma(int turmaId)
        {
            return await _dbContext.Matriculas
            .Where(m => m.TurmaId == turmaId)
            .CountAsync();
        }

        public async Task<bool> TurmaExiste(int turmaId)
        {
            return await Task.Run(() => _dbContext.Turmas.Any(t => t.TurmaId == turmaId));
        }

        public async Task<bool> AtualizarTurma(Turma turma)
        {
            var turmaExistente = await _dbContext.Turmas.FindAsync(turma.TurmaId);

            if (turmaExistente == null)
                return false;

            turmaExistente.Numero = turma.Numero;
            turmaExistente.AnoLetivo = turma.AnoLetivo;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Turma>> ObterTodasTurmas()
        {
            return await _dbContext.Turmas.ToListAsync();
        }

        public async Task<bool> ExcluirTurma(int turmaId)
        {
            var turma = await _dbContext.Turmas.FindAsync(turmaId);

            if (turma == null)
                return false;

            _dbContext.Turmas.Remove(turma);
            await _dbContext.SaveChangesAsync();
            return true;
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