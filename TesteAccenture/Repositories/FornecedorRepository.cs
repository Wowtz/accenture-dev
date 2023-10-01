using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteAccenture.Models;

namespace TesteAccenture.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly ApplicationDbContext _context;

        public FornecedorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Fornecedor>> ObterTodosAsync()
        {
            return await _context.Fornecedores.ToListAsync();
        }

        public async Task<Fornecedor> ObterPorIdAsync(int id)
        {
            return await _context.Fornecedores.FindAsync(id);
        }

        public async Task<int> AdicionarAsync(Fornecedor fornecedor)
        {
            _context.Fornecedores.Add(fornecedor);
            await _context.SaveChangesAsync();
            return fornecedor.Id;
        }

        public async Task AtualizarAsync(Fornecedor fornecedor)
        {
            _context.Entry(fornecedor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor != null)
            {
                _context.Fornecedores.Remove(fornecedor);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<Fornecedor> FiltrarFornecedores(string nome, string cnpj, string email, string cep)
        {
            var query = _context.Fornecedores.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(e => e.Nome.Contains(nome));
            }

            if (!string.IsNullOrEmpty(cnpj))
            {
                query = query.Where(e => e.CNPJCPF.Contains(cnpj));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(e => e.Email.Contains(email));
            }

            if (!string.IsNullOrEmpty(cep))
            {
                query = query.Where(e => e.CEP.Contains(cep));
            }

            return query.ToList();
        }
    }
}
