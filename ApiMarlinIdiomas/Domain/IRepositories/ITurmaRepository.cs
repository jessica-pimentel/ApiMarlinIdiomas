using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.IRepositories
{
    public interface ITurmaRepository
    {
        Task CadastrarTurma(Turma turma);

        // Validação do limite de 5 alunos por turma
        Task<int> ObterQuantidadeAlunosNaTurma(int turmaId);

        Task<Turma> ObterTurmaPorNumero(int numero);
        Task<bool> TurmaExiste(int turmaId);
        Task<bool> AtualizarTurma(Turma turma);
        Task<List<Turma>> ObterTodasTurmas();
        Task<bool> ExcluirTurma(int turmaId);
        Task<Turma> ObterTurmaPorId(int turmaId);
    }
}