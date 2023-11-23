using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Domain.IServices;
using Microsoft.AspNetCore.Http; 

namespace Api.Controllers
{
    [ApiController]
    [Route("api/matricula")]
    public class MatriculaController : ControllerBase
    {
        private readonly IMatriculaService _matriculaService;
        private readonly ITurmaService _turmaService;
        private readonly IAlunoService _alunoService;

        public MatriculaController(IMatriculaService matriculaService, ITurmaService turmaService, IAlunoService alunoService)
        {
            _matriculaService = matriculaService;
            _turmaService = turmaService;
            _alunoService = alunoService;
        }

        [HttpPost("cadastrar-matricula")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarMatricula(
            [FromQuery] int alunoId,
            [FromQuery] int turmaId)
        {
             try
            {
                // Verifica turma existe
                var turmaExistente = await _turmaService.ObterTurmaPorId(turmaId);
                if (turmaExistente == null)
                    return BadRequest("Turma não encontrada.");

                // Verifica aluno existe
                var alunoExistente = await _alunoService.ObterAlunoPorId(alunoId);
                if (alunoExistente == null)
                    return BadRequest("Aluno não encontrado.");

                // Carregue a lista de alunos associados à turma
                await _turmaService.CarregarAlunosAsync(turmaExistente);

                // Verificar se a turma atingiu o limite de alunos
                if (turmaExistente.Alunos.Count >= 5)
                {
                    throw new Exception("A turma atingiu o limite máximo de alunos.");
                }

                // validacao se o aluno já está matriculado nesta turma
                var matriculaExistente = await _matriculaService.ObterMatriculaPorAlunoETurmaId(alunoId, turmaId);
                if (matriculaExistente != null)
                    return BadRequest("O aluno já está matriculado nesta turma.");

                // Cadastrar a matrícula
                await _matriculaService.CadastrarMatricula(alunoId, turmaId);

                return Ok("Matrícula cadastrada com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno no servidor: " + ex.Message);
            }
        }

        [HttpGet("obter-todas-matriculas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterTodasMatriculas()
        {
            try
            {
                var matriculas = await _matriculaService.ObterTodasMatriculas();
                return Ok(matriculas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno no servidor: " + ex.Message);
            }
        }

        [HttpDelete("excluir-matricula/{matriculaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ExcluirMatricula(int matriculaId)
        {
            try
            {
                var sucesso = await _matriculaService.ExcluirMatricula(matriculaId);
                return Ok(sucesso);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno no servidor: " + ex.Message);
            }
        }
    }
}