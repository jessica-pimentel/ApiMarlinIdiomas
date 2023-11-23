using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using Domain.Models;
using Domain.IServices;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/aluno")]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpPost("cadastrar-aluno")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarAluno(
            [FromQuery] string cpf,
            [FromQuery] string nome,
            [FromQuery] string email)
        {
            if (string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email))
                return BadRequest("Parâmetros inválidos.");

            // validacao CPF repetido
            var alunoExistente = await _alunoService.ObterAlunoPorCPF(cpf);
            if (alunoExistente != null)
                return BadRequest("Já existe um aluno cadastrado com este CPF.");

            try
            {
                await _alunoService.CadastrarAluno(cpf, nome, email);
                return Ok("Aluno cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao cadastrar aluno: {ex.Message}");
            }
        }

        [HttpPut("atualizar-aluno/{alunoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]       
        public async Task<IActionResult> AtualizarAluno(
            int alunoId,
            [FromQuery] string cpf,
            [FromQuery] string nome,
            [FromQuery] string email)
        {
            var alunoExistente = await _alunoService.ObterAlunoPorId(alunoId);

            if (alunoExistente == null)
                return NotFound("Aluno não encontrado.");

            try
            {
                var alunoAtualizado = new Aluno
                {
                    AlunoId = alunoId,
                    CPF = cpf,
                    Nome = nome,
                    Email = email
                };

                await _alunoService.AtualizarAluno(alunoAtualizado);
                return Ok("Aluno atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno no servidor: " + ex.Message);
            }
        }

        [HttpGet("obter-todos-alunos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        public async Task<IActionResult> ObterTodosAlunos()
        {
            try
            {
                var alunos = await _alunoService.ObterTodosAlunos();
                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno no servidor." + ex.Message);
            }
        }

        [HttpDelete("excluir-aluno/{alunoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ExcluirAluno(int alunoId)
        {
            var alunoExistente = await _alunoService.ObterAlunoPorId(alunoId);

            if (alunoExistente == null)
                return NotFound("Aluno não encontrado.");

            try
            {
                await _alunoService.ExcluirAluno(alunoId);
                return Ok("Aluno excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno no servidor: " + ex.Message);
            }
        }
    }
}