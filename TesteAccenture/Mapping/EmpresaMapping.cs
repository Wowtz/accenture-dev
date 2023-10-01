using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteAccenture.Models;

namespace TesteAccenture.Mapping
{
    public class EmpresaMapping : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.CNPJ).IsRequired();
            builder.Property(e => e.NomeFantasia).IsRequired();
            builder.Property(e => e.CEP).IsRequired();

            builder.HasMany(e => e.FornecedorEmpresas)
                .WithOne(fe => fe.Empresa)
                .HasForeignKey(fe => fe.EmpresaId);
        }
    }
}
