using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.IRepositories;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly AplicationDbContext _dbContext;

        public MatriculaRepository(AplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CadastrarMatricula(Matricula matricula)
        {
            _dbContext.Matriculas.Add(matricula);
            await _dbContext.SaveChangesAsync();
        }

        // validacao se o aluno já está matriculado nesta turma
        public async Task<Matricula> ObterMatriculaPorAlunoETurmaId(int alunoId, int turmaId)
        {
            return await _dbContext.Matriculas
                .FirstOrDefaultAsync(m => m.AlunoId == alunoId && m.TurmaId == turmaId);
        }

        public async Task<bool> AlunoEstaMatriculado(int alunoId)
        {
            return await _dbContext.Matriculas.AnyAsync(m => m.AlunoId == alunoId);
        }

        public async Task<bool> AtualizarMatricula(Matricula matricula)
        {
            var matriculaExistente = await _dbContext.Matriculas.FindAsync(matricula.MatriculaId);

            if (matriculaExistente == null)
                return false;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Matricula>> ObterTodasMatriculas()
        {
            return await _dbContext.Matriculas.ToListAsync();
        }

        public async Task<bool> ExcluirMatricula(int matriculaId)
        {
            var matricula = await _dbContext.Matriculas.FindAsync(matriculaId);

            if (matricula == null)
                return false;

            _dbContext.Matriculas.Remove(matricula);
            await _dbContext.SaveChangesAsync();
            return true;
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
    }
}