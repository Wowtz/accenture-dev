using TesteAccenture.Models;

namespace TesteAccenture.Repositories
{
    public interface IValidationRepository
    {
        public Task<List<FornecedorEmpresa>> EmpresasVinculadas(int id);
        public Task<List<FornecedorEmpresa>> FornecedoresVinculados(int id);
        public Task<List<FornecedorEmpresa>> AtualizarEmpresasVinculadas(int fornecedorId, List<FornecedorEmpresa> empresas, List<int> empresaIds);
        public Task<List<FornecedorEmpresa>> AtualizarFornecedoresVinculados(int empresaId, List<FornecedorEmpresa> fornecedores, List<int> fornecedoresId);

    }
}
