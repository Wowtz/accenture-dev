using Microsoft.EntityFrameworkCore;
using TesteAccenture.Mapping;
using TesteAccenture.Models;

public class ApplicationDbContext : DbContext
{
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<FornecedorEmpresa> FornecedorEmpresas { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplicar as configurações dos mapeamentos
        modelBuilder.ApplyConfiguration(new FornecedorMapping());
        modelBuilder.ApplyConfiguration(new EmpresaMapping());
        modelBuilder.ApplyConfiguration(new FornecedorEmpresaMapping());
    }
}
