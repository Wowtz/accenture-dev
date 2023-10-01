using Microsoft.EntityFrameworkCore;
using TesteAccenture.Models;

namespace TesteAccenture.Repositories
{
    public class ValidationRepository : IValidationRepository
    {
        private readonly ApplicationDbContext _context;

        public ValidationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FornecedorEmpresa>> EmpresasVinculadas(int id)
        {
            return await _context.FornecedorEmpresas
                .Where(e => e.FornecedorId == id)
                .ToListAsync();
        }

        public async Task<List<FornecedorEmpresa>> FornecedoresVinculados(int id)
        {
            return await _context.FornecedorEmpresas
                .Where(e => e.EmpresaId == id)
                .ToListAsync();
        }

        public async Task<List<FornecedorEmpresa>> AtualizarEmpresasVinculadas(int fornecedorId, List<FornecedorEmpresa> empresas, List<int> empresaIds)
        {
            var empresasAntigas = await _context.FornecedorEmpresas
                .Where(fe => fe.FornecedorId == fornecedorId)
                .ToListAsync();

            _context.FornecedorEmpresas.RemoveRange(empresasAntigas); 

            foreach (var empresa in empresaIds)
            {
                _context.FornecedorEmpresas.Add(new FornecedorEmpresa() { FornecedorId = fornecedorId, EmpresaId = empresa });
            }

            await _context.SaveChangesAsync();

            return await _context.FornecedorEmpresas
               .Where(e => e.FornecedorId == fornecedorId)
               .ToListAsync();
        }

        public async Task<List<FornecedorEmpresa>> AtualizarFornecedoresVinculados(int empresaId, List<FornecedorEmpresa> fornecedores, List<int> fornecedoresId)
        {
            var fornecedoresAntigos = await _context.FornecedorEmpresas
                .Where(fe => fe.EmpresaId == empresaId)
                .ToListAsync();

            _context.FornecedorEmpresas.RemoveRange(fornecedoresAntigos);

            foreach (var fornecedor in fornecedoresId)
            {
                _context.FornecedorEmpresas.Add(new FornecedorEmpresa() { EmpresaId = empresaId, FornecedorId = fornecedor });
            }

            await _context.SaveChangesAsync();

            return await _context.FornecedorEmpresas
               .Where(e => e.EmpresaId == empresaId)
               .ToListAsync();
        }
    }
}
