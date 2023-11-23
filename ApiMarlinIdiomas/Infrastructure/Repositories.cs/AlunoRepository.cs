using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Domain.IRepositories;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AplicationDbContext _dbContext;

        public AlunoRepository(AplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // validacao CPF repetido
        public async Task<Aluno> ObterAlunoPorCPF(string cpf)
        {
            return await _dbContext.Alunos.FirstOrDefaultAsync(a => a.CPF == cpf);
        }

        public async Task CadastrarAluno(Aluno aluno)
        {
            _dbContext.Alunos.Add(aluno);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> AtualizarAluno(Aluno aluno)
        {
            var alunoExistente = _dbContext.Alunos.Find(aluno.AlunoId);

            if (alunoExistente == null)
                return false;

            alunoExistente.Nome = aluno.Nome;
            alunoExistente.CPF = aluno.CPF;
            alunoExistente.Email = aluno.Email;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Aluno>> ObterTodosAlunos()
        {
            return await _dbContext.Alunos.ToListAsync();
        }

        public async Task<bool> ExcluirAluno(int alunoId)
        {
            var aluno = await _dbContext.Alunos.FindAsync(alunoId);

            if (aluno == null)
                return false;

            _dbContext.Alunos.Remove(aluno);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Aluno> ObterAlunoPorId(int alunoId)
        {
            return await _dbContext.Alunos.FindAsync(alunoId);
        }
    }
}