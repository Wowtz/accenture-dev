using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteAccenture.Models;

namespace TesteAccenture.Mapping
{
    public class FornecedorEmpresaMapping : IEntityTypeConfiguration<FornecedorEmpresa>
    {
        public void Configure(EntityTypeBuilder<FornecedorEmpresa> builder)
        {
            builder.HasKey(fe => new { fe.FornecedorId, fe.EmpresaId });

            // Configuração das chaves estrangeiras
            builder.HasOne(fe => fe.Fornecedor)
                .WithMany(f => f.FornecedorEmpresas)
                .HasForeignKey(fe => fe.FornecedorId);

            builder.HasOne(fe => fe.Empresa)
                .WithMany(e => e.FornecedorEmpresas)
                .HasForeignKey(fe => fe.EmpresaId);
        }
    }
}
