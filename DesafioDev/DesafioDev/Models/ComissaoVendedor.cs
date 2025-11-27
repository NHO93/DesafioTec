namespace DesafioDev.Models
{
    public class ComissaoVendedor
    {
        public string Vendedor { get; set; } = string.Empty;
        public decimal TotalVendas { get; set; }
        public decimal TotalComissao { get; set; }
        public int QuantidadeVendas { get; set; }
    }
}