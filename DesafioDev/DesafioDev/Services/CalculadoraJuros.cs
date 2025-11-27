namespace DesafioDev.Services
{
    public class CalculadoraJuros
    {
        public static (decimal juros, int diasAtraso, decimal valorTotal) CalcularJuros(
            decimal valorOriginal, 
            DateTime dataVencimento, 
            decimal taxaDiariaPercentual = 2.5m)
        {
            DateTime dataAtual = DateTime.Today;
            
            if (dataAtual <= dataVencimento)
                return (0, 0, valorOriginal);
                
            int diasAtraso = (dataAtual - dataVencimento).Days;
            decimal taxaDiariaDecimal = taxaDiariaPercentual / 100m;
            
            decimal juros = valorOriginal * taxaDiariaDecimal * diasAtraso;
            decimal valorTotal = valorOriginal + juros;
            
            return (juros, diasAtraso, valorTotal);
        }
    }
}