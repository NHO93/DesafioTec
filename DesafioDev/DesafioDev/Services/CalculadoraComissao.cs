using System.Text.Json;
using DesafioDev.Models;

namespace DesafioDev.Services
{
    public class CalculadoraComissao
    {
        public static List<ComissaoVendedor> CalcularComissoes(string jsonVendas)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var vendasData = JsonSerializer.Deserialize<VendasData>(jsonVendas, options) 
                             ?? new VendasData();
            
            var resultado = new List<ComissaoVendedor>();
            
            var vendasPorVendedor = vendasData.Vendas
                .GroupBy(v => v.Vendedor);
            
            foreach (var grupo in vendasPorVendedor)
            {
                decimal totalComissao = 0;
                decimal totalVendas = 0;
                int quantidadeVendas = 0;
                
                foreach (var venda in grupo)
                {
                    totalVendas += venda.Valor;
                    totalComissao += CalcularComissaoVenda(venda.Valor);
                    quantidadeVendas++;
                }
                
                resultado.Add(new ComissaoVendedor
                {
                    Vendedor = grupo.Key,
                    TotalVendas = totalVendas,
                    TotalComissao = totalComissao,
                    QuantidadeVendas = quantidadeVendas
                });
            }
            
            return resultado.OrderByDescending(c => c.TotalComissao).ToList();
        }
        
        private static decimal CalcularComissaoVenda(decimal valorVenda)
        {
            if (valorVenda < 100.00m)
                return 0;
            else if (valorVenda < 500.00m)
                return valorVenda * 0.01m;
            else
                return valorVenda * 0.05m;
        }
    }
}