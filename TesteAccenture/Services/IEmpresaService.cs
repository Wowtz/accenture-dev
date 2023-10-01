using TesteAccenture.Models;

namespace TesteAccenture.Services
{
    public interface IEmpresaService
    {
        public IEnumerable<Empresa> FiltrarEmpresa(string nomeFantasia, string cnpj, string cep);
        Task<IEnumerable<Empresa>> ObterTodasAsync();
        Task<Empresa> ObterPorIdAsync(int id);
        Task<int> AdicionarAsync(Empresa empresa);
        Task AtualizarAsync(Empresa empresa);
        Task ExcluirAsync(int id);
    }
}
