using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.IRepositories
{
    public interface IAlunoRepository
    {
        Task CadastrarAluno(Aluno aluno);

        // validacao CPF repetido
        Task<Aluno> ObterAlunoPorCPF(string cpf);
        
        Task<bool> AtualizarAluno(Aluno aluno);
        Task<List<Aluno>> ObterTodosAlunos();
        Task<bool> ExcluirAluno(int alunoId);
        Task<Aluno> ObterAlunoPorId(int alunoId);
    }
}