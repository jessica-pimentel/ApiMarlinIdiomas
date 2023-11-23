## 🚀 ApiMarlinIdiomas

## Criação da base de dados: 

  - Aluno - nome, cpf e e-mail;
  - Turma - número, ano letivo;
  - Matricula - alunoId, turmaId;

 
## 💻 Código fonte de API em C#: 
A API utiliza a arquitetura MVVM com DDD utilizando Code First Mapping. 
Os dados são manipulados utilizando o Entity Framework e a API possui os seguintes métodos: 

  - CRUD de Aluno 
  - CRUD de Turma 
  - CRUD de Matrícula
    

## 🏷️ Regras de negócio:
  - Aluno não pode ser cadastrado repetido (validação pelo CPF) 
  - No momento de cadastrar um aluno, deve-se informar pelo menos uma turma que ele irá cursar; 
  - O mesmo aluno pode ser matriculado em várias turmas diferentes, porém a Matrícula não pode ser repetida na mesma turma; 
  - Uma turma não pode ter mais de 5 alunos; 
  - Turma não pode ser excluída se possuir alunos; 


