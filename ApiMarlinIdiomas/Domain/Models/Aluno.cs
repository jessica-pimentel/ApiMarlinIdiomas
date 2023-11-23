using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{

public class Aluno
{
        [Key]
        public int AlunoId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
}
}

