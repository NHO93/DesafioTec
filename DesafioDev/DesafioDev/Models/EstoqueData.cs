using System.Text.Json.Serialization;

namespace DesafioDev.Models
{
    public class Produto
    {
        [JsonPropertyName("codigoProduto")]
        public int CodigoProduto { get; set; }
        
        [JsonPropertyName("descricaoProduto")]
        public string DescricaoProduto { get; set; } = string.Empty;
        
        [JsonPropertyName("estoque")]
        public int Estoque { get; set; }
    }

    public class EstoqueData
    {
        [JsonPropertyName("estoque")]
        public List<Produto> Estoque { get; set; } = new List<Produto>();
    }

    public class Movimentacao
    {
        public int Id { get; set; }
        public int CodigoProduto { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public DateTime DataMovimentacao { get; set; }
        public string Tipo { get; set; } = string.Empty; // "ENTRADA" ou "SAIDA"
    }
}