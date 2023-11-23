using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{

public class Matricula
{
    [Key]
    public int MatriculaId { get; set; }
    public int AlunoId { get; set; }
    public int TurmaId { get; set; }
    public Aluno Aluno { get; set; }
    public Turma Turma { get; set; }
}
}