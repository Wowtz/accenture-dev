using TesteAccenture.Models;

namespace TesteAccenture.Services
{
    public interface IValidationsService
    {
        public Task<ValidacaoCEP> ExisteCPF(string cep);
        public Task<List<FornecedorEmpresa>> EmpresasVinculadas(int id);
        public Task<List<FornecedorEmpresa>> FornecedoresVinculados(int id);
        public Task<List<FornecedorEmpresa>> VincularEmpresas(int fornecedorId, List<int> empresaIds);
        public Task<List<FornecedorEmpresa>> VincularFornecedores(int empresaId, List<int> fornecedoresIds);
    }
}
