using TesteAccenture.Models;

namespace TesteAccenture.Services
{
    public interface IFornecedorService
    {
        public IEnumerable<Fornecedor> FiltrarFornecedores(string nomeFantasia, string cnpj, string email, string cep);
        Task<IEnumerable<Fornecedor>> ObterTodosAsync();
        Task<Fornecedor> ObterPorIdAsync(int id);
        Task<int> AdicionarAsync(Fornecedor fornecedor);
        Task AtualizarAsync(Fornecedor fornecedor);
        Task ExcluirAsync(int id);
    }
}
