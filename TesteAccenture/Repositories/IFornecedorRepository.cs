using TesteAccenture.Models;

namespace TesteAccenture.Repositories
{
    public interface IFornecedorRepository
    {
        public IEnumerable<Fornecedor> FiltrarFornecedores(string nome, string cnpj, string email, string cep);
        Task<IEnumerable<Fornecedor>> ObterTodosAsync();
        Task<Fornecedor> ObterPorIdAsync(int id);
        Task<int> AdicionarAsync(Fornecedor fornecedor);
        Task AtualizarAsync(Fornecedor fornecedor);
        Task ExcluirAsync(int id);
    }
}
