using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteAccenture.Models;

namespace TesteAccenture.Mapping
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.CNPJCPF).IsRequired();
            builder.Property(f => f.Nome).IsRequired();
            builder.Property(f => f.Email).IsRequired();
            builder.Property(f => f.CEP).IsRequired();
            builder.Property(f => f.RG).IsRequired(false);
            builder.Property(f => f.DataNascimento).IsRequired(false);
            builder.Property(f => f.TipoPessoa).IsRequired();

            builder.HasMany(f => f.FornecedorEmpresas)
                .WithOne(fe => fe.Fornecedor)
                .HasForeignKey(fe => fe.FornecedorId);
        }
    }
}
