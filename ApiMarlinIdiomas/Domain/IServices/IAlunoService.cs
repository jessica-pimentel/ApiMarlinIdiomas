using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.IServices
{
    public interface IAlunoService
    {
        // validacao CPF repetido
        Task<Aluno> ObterAlunoPorCPF(string cpf);
        
        Task CadastrarAluno(string cpf, string nome, string email);
        Task<bool> AtualizarAluno(Aluno aluno);
        Task<List<Aluno>> ObterTodosAlunos();
        Task<bool> ExcluirAluno(int alunoId);
        Task<Aluno> ObterAlunoPorId(int alunoId);
    }
}