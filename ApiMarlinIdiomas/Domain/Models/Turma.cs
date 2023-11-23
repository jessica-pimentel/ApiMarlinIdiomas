using System.ComponentModel.DataAnnotations;


namespace Domain.Models
{
    
public class Turma
{
    [Key]
    public int TurmaId { get; set; }
    public int Numero { get; set; }
    public int AnoLetivo { get; set; }
    public List<Matricula> Matriculas { get; set; }
    public List<Aluno> Alunos {get; set; }
}
}

