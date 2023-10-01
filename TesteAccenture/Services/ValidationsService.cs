using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TesteAccenture.Models;
using TesteAccenture.Repositories;

namespace TesteAccenture.Services
{
    public class ValidationsService : IValidationsService
    {
        private readonly HttpClient _httpClient;
        private readonly IValidationRepository _validationRepository;

        public ValidationsService(HttpClient httpClient, IValidationRepository validationRepository)
        {
            _httpClient = httpClient;
            _validationRepository = validationRepository;
        }

        public async Task<ValidacaoCEP> ExisteCPF(string cep)
        {
            try
            {
                var cepFormatado = FormatarCEP(cep);
                var validacaoCEP = new ValidacaoCEP();

                var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cepFormatado}/json/");

                var jsonStream = await response.Content.ReadAsStreamAsync();
                using (var jsonDocument = await JsonDocument.ParseAsync(jsonStream))
                {
                    var root = jsonDocument.RootElement;
                    if (root.TryGetProperty("uf", out var ufProperty))
                    {
                            if (ufProperty.GetString() == "PR")
                            {
                                validacaoCEP.Parana = true;
                                validacaoCEP.CEPValido = true;
                                return validacaoCEP;
                            }
                            validacaoCEP.Parana = false;
                            validacaoCEP.CEPValido = true;
                            return validacaoCEP;
                    } 
                    else
                    {
                        validacaoCEP.Parana = false;
                        validacaoCEP.CEPValido = false;
                        return validacaoCEP;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<FornecedorEmpresa>> EmpresasVinculadas(int id)
        {
            return await _validationRepository.EmpresasVinculadas(id);
        }

        public async Task<List<FornecedorEmpresa>> FornecedoresVinculados(int id)
        {
            return await _validationRepository.FornecedoresVinculados(id);
        }

        public async Task<List<FornecedorEmpresa>> VincularEmpresas(int fornecedorId, List<int> empresaIds)
        {
            var empresasVinculadasAntigas = await EmpresasVinculadas(fornecedorId);

            var resposta = await _validationRepository.AtualizarEmpresasVinculadas(fornecedorId, empresasVinculadasAntigas, empresaIds);

            return resposta;
        }

        public async Task<List<FornecedorEmpresa>> VincularFornecedores(int empresaId, List<int> fornecedoresIds)
        {
            var fornecedoresVinculadosAntigos = await EmpresasVinculadas(empresaId);

            var resposta = await _validationRepository.AtualizarFornecedoresVinculados(empresaId, fornecedoresVinculadosAntigos, fornecedoresIds);

            return resposta;
        }

        private static string FormatarCEP(string input)
        {
            return Regex.Replace(input, "[^0-9]", "");
        }
    }
}
