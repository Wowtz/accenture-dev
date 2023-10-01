namespace TesteAccenture.Models
{
    public class FornecedorEmpresa
    {
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
    }
}