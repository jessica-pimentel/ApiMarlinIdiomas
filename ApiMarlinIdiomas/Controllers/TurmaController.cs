using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Domain.IServices;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/turma")]
    public class TurmaController : ControllerBase
    {
        private readonly ITurmaService _turmaService;

        public TurmaController(ITurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        [HttpPost("cadastrar-turma")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarTurma(
            [FromQuery] int numero,
            [FromQuery] int anoLetivo)
        {
           try
            {
                // var turmaExistente = await _turmaService.ObterTurmaPorNumero(numero);
                // if (turmaExistente != null)
                //     return BadRequest("Já existe uma turma cadastrada com este número.");

                await _turmaService.CadastrarTurma(numero, anoLetivo);
                return Ok(new { Message = "Turma cadastrada com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Erro ao cadastrar turma: {ex.Message}" });
            }
        }

        [HttpGet("obter-todas-turmas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterTodasTurmas()
        {
            try
            {
                var turmas = await _turmaService.ObterTodasTurmas();
                return Ok(turmas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno no servidor: " + ex.Message);
            }
        }

        [HttpPut("atualizar-turma")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarTurma(
            [FromQuery] int numero,
            [FromQuery] int anoLetivo)
        {
            try
            {
                var turmaExistente = await _turmaService.ObterTurmaPorNumero(numero);
                if (turmaExistente == null)
                    return NotFound("Turma não encontrada.");

                turmaExistente.AnoLetivo = anoLetivo;

                var sucesso = await _turmaService.AtualizarTurma(turmaExistente);
                return Ok(sucesso);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno no servidor: " + ex.Message);
            }
        }
        
        [HttpDelete("excluir-turma/{turmaId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ExcluirTurma(int turmaId)
        {
            //validacao para nao excluir turma se tiver alunos
            var possuiAlunosMatriculados = await _turmaService.PossuiAlunosMatriculados(turmaId);
            if (possuiAlunosMatriculados)
                return BadRequest("Não é possível excluir a turma pois possui alunos matriculados.");

            try
            {
                var sucesso = await _turmaService.ExcluirTurma(turmaId);
                return Ok(sucesso);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno no servidor: " + ex.Message);
            }    
        }
    }
}