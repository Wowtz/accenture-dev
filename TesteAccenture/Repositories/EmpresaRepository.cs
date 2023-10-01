using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteAccenture.Models;

namespace TesteAccenture.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly ApplicationDbContext _context;

        public EmpresaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empresa>> ObterTodasAsync()
        {
            return await _context.Empresas.ToListAsync();
        }

        public async Task<Empresa> ObterPorIdAsync(int id)
        {
            return await _context.Empresas.FindAsync(id);
        }

        public async Task<int> AdicionarAsync(Empresa empresa)
        {
            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();
            return empresa.Id;
        }

        public async Task AtualizarAsync(Empresa empresa)
        {
            _context.Entry(empresa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa != null)
            {
                _context.Empresas.Remove(empresa);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<Empresa> FiltrarEmpresa(string nomeFantasia, string cnpj, string cep)
        {
            var query = _context.Empresas.AsQueryable();

            if (!string.IsNullOrEmpty(nomeFantasia))
            {
                query = query.Where(e => e.NomeFantasia.Contains(nomeFantasia));
            }

            if (!string.IsNullOrEmpty(cnpj))
            {
                query = query.Where(e => e.CNPJ.Contains(cnpj));
            }

            if (!string.IsNullOrEmpty(cep))
            {
                query = query.Where(e => e.CEP.Contains(cep));
            }

            return query.ToList();
        }

    }
}
