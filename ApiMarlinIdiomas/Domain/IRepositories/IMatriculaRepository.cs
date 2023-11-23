using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.IRepositories
{
    public interface IMatriculaRepository
    {
        Task CadastrarMatricula(Matricula matricula);

        // validacao se o aluno já está matriculado nesta turma
        Task<Matricula> ObterMatriculaPorAlunoETurmaId(int alunoId, int turmaId);

        Task<int> ObterQuantidadeAlunosNaTurma(int turmaId);
        Task<bool> AlunoEstaMatriculado(int alunoId);
        Task<bool> AtualizarMatricula(Matricula matricula);
        Task<List<Matricula>> ObterTodasMatriculas();
        Task<bool> ExcluirMatricula(int matriculaId);
        Task<Matricula> ObterMatriculaPorId(int matriculaId);

    }
}