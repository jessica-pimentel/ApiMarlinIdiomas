using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.IRepositories;
using Domain.IServices;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaService _turmaService;
        private readonly AplicationDbContext _dbContext;

        public AlunoService(IAlunoRepository alunoRepository, ITurmaService turmaService, AplicationDbContext dbContext)
        {
            _alunoRepository = alunoRepository;
            _turmaService = turmaService;
            _dbContext = dbContext;
        }

        public async Task CadastrarAluno(string cpf, string nome, string email)
        {
            var aluno = new Aluno
            {
                CPF = cpf,
                Nome = nome,
                Email = email
            };

            await _alunoRepository.CadastrarAluno(aluno);
        }        public async Task<Aluno> ObterAlunoPorId(int alunoId)
        {
            return await _dbContext.Alunos.FindAsync(alunoId);
        }

        public async Task<bool> AtualizarAluno(Aluno aluno)
        {
            return await _alunoRepository.AtualizarAluno(aluno);
        }

        public async Task<List<Aluno>> ObterTodosAlunos()
        {
            return await _alunoRepository.ObterTodosAlunos();
        }

        public async Task<bool> ExcluirAluno(int alunoId)
        {
            return await _alunoRepository.ExcluirAluno(alunoId);
        }

        // validacao CPF repetido
        public async Task<Aluno> ObterAlunoPorCPF(string cpf)
        {
            return await _alunoRepository.ObterAlunoPorCPF(cpf);
        }

    }
}