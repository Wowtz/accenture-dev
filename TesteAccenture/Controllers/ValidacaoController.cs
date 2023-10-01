using Microsoft.AspNetCore.Mvc;
using TesteAccenture.Models;
using TesteAccenture.Services;

namespace TesteAccenture.Controllers
{
    [Route("v1/validacao")]
    public class ValidacaoController : Controller
    {
        private readonly IValidationsService _validationsService;

        public ValidacaoController(IValidationsService validationsService)
        {
            _validationsService = validationsService;
        }

        [HttpGet("validarCEP/{cep}")]
        public async Task<IActionResult> ValidarCPF(string cep)
        {
            var existeCep = await _validationsService.ExisteCPF(cep);

            return Ok(existeCep);
        }

        [HttpGet("empresas-vinculadas/{id}")]
        public async Task<List<FornecedorEmpresa>> EmpresasVinculadas(int id)
        {
            var fornecedorEmpresa = await _validationsService.EmpresasVinculadas(id);

            return fornecedorEmpresa;
        }

        [HttpGet("fornecedores-vinculados/{id}")]
        public async Task<List<FornecedorEmpresa>> FornecedoresVinculados(int id)
        {
            var fornecedorEmpresa = await _validationsService.FornecedoresVinculados(id);

            return fornecedorEmpresa;
        }

        [HttpPost("vincular-empresa/{id}")]
        public async Task<List<FornecedorEmpresa>> VincularEmpresas(int id, [FromBody]List<int> idEmpresas)
        {
            var fornecedorEmpresa = await _validationsService.VincularEmpresas(id, idEmpresas);

            return fornecedorEmpresa;
        }

        [HttpPost("vincular-fornecedor/{id}")]
        public async Task<List<FornecedorEmpresa>> VincularFornecedor(int id, [FromBody]List<int> idEmpresas)
        {
            var fornecedorEmpresa = await _validationsService.VincularFornecedores(id, idEmpresas);

            return fornecedorEmpresa;
        }
    }
}
