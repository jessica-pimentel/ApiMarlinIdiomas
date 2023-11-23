using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.IServices
{
    public interface IMatriculaService
    {
        Task CadastrarMatricula(int alunoId, int turmaId);

        // Validação do limite de 5 alunos por turma
        Task<Matricula> ObterMatriculaPorAlunoETurmaId(int alunoId, int turmaId);

        Task<bool> AtualizarMatricula(Matricula matricula);
        Task<List<Matricula>> ObterTodasMatriculas();
        Task<bool> ExcluirMatricula(int matriculaId);
        Task<int> ObterQuantidadeAlunosNaTurma(int turmaId);
        Task<bool> AlunoEstaMatriculado(int alunoId);
        Task<Matricula> ObterMatriculaPorId(int matriculaId);

        
    }
}