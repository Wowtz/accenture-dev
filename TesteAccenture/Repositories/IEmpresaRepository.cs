using TesteAccenture.Models;

namespace TesteAccenture.Repositories
{
    public interface IEmpresaRepository
    {
        Task<IEnumerable<Empresa>> ObterTodasAsync();
        Task<Empresa> ObterPorIdAsync(int id);
        Task<int> AdicionarAsync(Empresa empresa);
        Task AtualizarAsync(Empresa empresa);
        Task ExcluirAsync(int id);
        public IEnumerable<Empresa> FiltrarEmpresa(string nomeFantasia, string cnpj, string cep);

    }
}
