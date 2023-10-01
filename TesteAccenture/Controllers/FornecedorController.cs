using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TesteAccenture.Models;
using TesteAccenture.Services;

namespace TesteAccenture.Controllers
{
    [Route("v1/fornecedores")]
    public class FornecedorController : MainController
    {
        private readonly IFornecedorService _fornecedorService;

        public FornecedorController(IFornecedorService fornecedorService)
        {
            _fornecedorService = fornecedorService;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> ListarFornecedores()
        {
            try
            {
                var fornecedores = await _fornecedorService.ObterTodosAsync();
                return ResponseCustomizada(fornecedores);
            }
            catch (Exception ex)
            {
                AddErros("Ocorreu um erro ao buscar fornecedores: " + ex.Message);
                return ResponseCustomizada();
            }
        }

        [HttpGet("filtrar")]
        public IActionResult FiltrarFornecedores([FromQuery] string nome, [FromQuery] string cnpj, [FromQuery] string email, [FromQuery] string cep)
        {
            var empresas = _fornecedorService.FiltrarFornecedores(nome, cnpj, email, cep);
            return Ok(empresas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Fornecedor>> FornecedorPorId(int id)
        {
            try
            {
                var fornecedor = await _fornecedorService.ObterPorIdAsync(id);
                if (fornecedor == null)
                {
                    AddErros("Fornecedor não encontrado.");
                    return ResponseCustomizada();
                }

                return ResponseCustomizada(fornecedor);
            }
            catch (Exception ex)
            {
                AddErros("Ocorreu um erro ao buscar o fornecedor: " + ex.Message);
                return ResponseCustomizada();
            }
        }

        [HttpPost("adicionar")]
        public async Task<ActionResult<Fornecedor>> AdicionarFornecedor([FromBody] Fornecedor fornecedor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddErros("Dados inválidos na solicitação.");
                    return ResponseCustomizada(ModelState);
                }

                var fornecedorId = await _fornecedorService.AdicionarAsync(fornecedor);
                return CreatedAtAction(nameof(FornecedorPorId), new { id = fornecedorId }, fornecedor);
            }
            catch (Exception ex)
            {
                AddErros("Ocorreu um erro ao criar o fornecedor: " + ex.Message);
                return ResponseCustomizada();
            }
        }


        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> AtualizarFornecedor(int id, [FromBody]Fornecedor fornecedor)
        {
            try
            {
                if (id != fornecedor.Id)
                {
                    AddErros("ID do fornecedor na URL não corresponde ao ID do fornecedor na solicitação.");
                    return ResponseCustomizada();
                }

                if (!ModelState.IsValid)
                {
                    AddErros("Dados inválidos na solicitação.");
                    return ResponseCustomizada(ModelState);
                }

                await _fornecedorService.AtualizarAsync(fornecedor);
                return ResponseCustomizada(fornecedor);
            }
            catch (Exception ex)
            {
                AddErros("Ocorreu um erro ao atualizar o fornecedor: " + ex.Message);
                return ResponseCustomizada();
            }
        }

        [HttpDelete("deletar/{id}")]
        public async Task<IActionResult> DeletarFornecedor(int id)
        {
            try
            {
                await _fornecedorService.ExcluirAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                AddErros("Ocorreu um erro ao excluir o fornecedor: " + ex.Message);
                return ResponseCustomizada();
            }
        }
    }
}
