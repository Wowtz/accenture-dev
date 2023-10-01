using TesteAccenture.Models;
using TesteAccenture.Repositories;

namespace TesteAccenture.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public IEnumerable<Empresa> FiltrarEmpresa(string nomeFantasia, string cnpj, string cep)
        {
            return _empresaRepository.FiltrarEmpresa(nomeFantasia, cnpj, cep);
        }

        public EmpresaService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<IEnumerable<Empresa>> ObterTodasAsync()
        {
            return await _empresaRepository.ObterTodasAsync();
        }

        public async Task<Empresa> ObterPorIdAsync(int id)
        {
            return await _empresaRepository.ObterPorIdAsync(id);
        }

        public async Task<int> AdicionarAsync(Empresa empresa)
        {
            return await _empresaRepository.AdicionarAsync(empresa);
        }

        public async Task AtualizarAsync(Empresa empresa)
        {
            await _empresaRepository.AtualizarAsync(empresa);
        }

        public async Task ExcluirAsync(int id)
        {
            await _empresaRepository.ExcluirAsync(id);
        }
    }
}
