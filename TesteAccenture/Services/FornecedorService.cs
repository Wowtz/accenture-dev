using System.Collections.Generic;
using System.Threading.Tasks;
using TesteAccenture.Models;
using TesteAccenture.Repositories;

namespace TesteAccenture.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        public IEnumerable<Fornecedor> FiltrarFornecedores(string nome, string cnpj, string email, string cep)
        {
            return _fornecedorRepository.FiltrarFornecedores(nome, cnpj, email, cep);
        }

        public FornecedorService(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task<IEnumerable<Fornecedor>> ObterTodosAsync()
        {
            return await _fornecedorRepository.ObterTodosAsync();
        }

        public async Task<Fornecedor> ObterPorIdAsync(int id)
        {
            return await _fornecedorRepository.ObterPorIdAsync(id);
        }

        public async Task<int> AdicionarAsync(Fornecedor fornecedor)
        {
            return await _fornecedorRepository.AdicionarAsync(fornecedor);
        }

        public async Task AtualizarAsync(Fornecedor fornecedor)
        {
            await _fornecedorRepository.AtualizarAsync(fornecedor);
        }

        public async Task ExcluirAsync(int id)
        {
            await _fornecedorRepository.ExcluirAsync(id);
        }
    }
}
