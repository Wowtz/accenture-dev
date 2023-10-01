using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TesteAccenture.Models;
using TesteAccenture.Services;

namespace TesteAccenture.Controllers
{
    [Route("v1/empresas")]
    public class EmpresaController : MainController
    {
        private readonly IEmpresaService _empresaService;
        public EmpresaController(IEmpresaService empresasService)
        {
            _empresaService = empresasService;
        }

        [HttpGet("filtrar")]
        public IActionResult FiltrarEmpresa([FromQuery] string nomeFantasia, [FromQuery] string cnpj, [FromQuery] string cep)
        {
            var empresas = _empresaService.FiltrarEmpresa(nomeFantasia, cnpj, cep);
            return Ok(empresas);
        }

        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<Empresa>>> ListarEmpresas()
        {
            try
            {
                var empresas = await _empresaService.ObterTodasAsync();
                return ResponseCustomizada(empresas);
            }
            catch (Exception ex)
            {
                AddErros("Ocorreu um erro ao buscar empresas: " + ex.Message);
                return ResponseCustomizada();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> EmpresaPorId(int id)
        {
            try
            {
                var empresa = await _empresaService.ObterPorIdAsync(id);
                if (empresa == null)
                {
                    AddErros("Empresa não encontrada.");
                    return ResponseCustomizada();
                }

                return ResponseCustomizada(empresa);
            }
            catch (Exception ex)
            {
                AddErros("Ocorreu um erro ao buscar a empresa: " + ex.Message);
                return ResponseCustomizada();
            }
        }

        [HttpPost("adicionar")]
        public async Task<ActionResult<Empresa>> AdicionarEmpresa([FromBody]Empresa empresa)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    AddErros("Dados inválidos na solicitação.");
                    return ResponseCustomizada(ModelState);
                }

                var empresaId = await _empresaService.AdicionarAsync(empresa);
                return CreatedAtAction(nameof(EmpresaPorId), new { id = empresaId }, empresa);
            }
            catch (Exception ex)
            {
                AddErros("Ocorreu um erro ao criar a empresa: " + ex.Message);
                return ResponseCustomizada();
            }
        }

        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> AtualizarEmpresa(int id, [FromBody]Empresa empresa)
        {
            try
            {
                if (id != empresa.Id)
                {
                    AddErros("ID da empresa na URL não corresponde ao ID da empresa na solicitação.");
                    return ResponseCustomizada();
                }

                if (!ModelState.IsValid)
                {
                    AddErros("Dados inválidos na solicitação.");
                    return ResponseCustomizada(ModelState);
                }

                await _empresaService.AtualizarAsync(empresa);
                return ResponseCustomizada(empresa);
            }
            catch (Exception ex)
            {
                AddErros("Ocorreu um erro ao atualizar a empresa: " + ex.Message);
                return ResponseCustomizada();
            }
        }

        [HttpDelete("deletar/{id}")]
        public async Task<IActionResult> DeletarEmpresa(int id)
        {
            try
            {
                await _empresaService.ExcluirAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                AddErros("Ocorreu um erro ao excluir a empresa: " + ex.Message);
                return ResponseCustomizada();
            }
        }
    }
}
