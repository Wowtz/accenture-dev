using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TesteAccenture.Models
{
    public class Empresa
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo CNPJ é obrigatório")]
        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$|^\d{14}$", ErrorMessage = "O CNPJ deve estar no formato correto")]
        [JsonPropertyName("CNPJ")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "Campo Nome Fantasia é obrigatório")]
        [JsonPropertyName("NomeFantasia")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "Campo CEP é obrigatório")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "O CEP deve estar no formato correto")]
        [JsonPropertyName("CEP")]
        public string CEP { get; set; }

        public List<FornecedorEmpresa>? FornecedorEmpresas { get; set; }
    }
}
