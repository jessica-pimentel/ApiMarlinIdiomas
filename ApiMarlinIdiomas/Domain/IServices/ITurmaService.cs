using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.IServices
{
    public interface ITurmaService
    {
        Task CadastrarTurma(int numero, int anoLetivo);

        // Validação do limite de 5 alunos por turma
        Task<int> ObterQuantidadeAlunosNaTurma(int turmaId);
        Task<Turma> ObterTurmaPorId(int turmaId);
        Task CarregarAlunosAsync(Turma turma);

        Task<Turma> ObterTurmaPorNumero(int numero);
        Task<bool> TurmaExiste(int turmaId);
        Task<bool> AtualizarTurma(Turma turma);
        Task<List<Turma>> ObterTodasTurmas();
        Task<bool> ExcluirTurma(int turmaId);
        Task<bool> PossuiAlunosMatriculados(int turmaId);
    }
}