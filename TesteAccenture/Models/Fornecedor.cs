using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TesteAccenture.Enums;

namespace TesteAccenture.Models
{
    public class Fornecedor
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo CNPJ/CPF é obrigatório")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$|^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$", ErrorMessage = "O CNPJ/CPF deve estar no formato correto")]
        [JsonPropertyName("CNPJCPF")]
        public string CNPJCPF { get; set; }
        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        [JsonPropertyName("Nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo Email é obrigatório")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "O endereço de e-mail não é válido.")]
        [JsonPropertyName("Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo CEP é obrigatório")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "O CEP deve estar no formato correto")]
        [JsonPropertyName("CEP")]
        public string CEP { get; set; }
        [JsonPropertyName("RG")]
        public string? RG { get; set; }
        [JsonPropertyName("DataNascimento")]
        public DateTime? DataNascimento { get; set; }
        [JsonPropertyName("TipoPessoa")]
        public TipoPessoa TipoPessoa { get; set; }

        public List<FornecedorEmpresa>? FornecedorEmpresas { get; set; }
    }
}
