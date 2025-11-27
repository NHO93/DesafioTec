using DesafioDev.Services;
using DesafioDev.Data;
using DesafioDev.Models;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== DESAFIO DEV - SISTEMA COMPLETO ===\n");

        // 1. CÁLCULO DE COMISSÕES
        Console.WriteLine("1. CÁLCULO DE COMISSÕES DE VENDEDORES");
        Console.WriteLine("=====================================\n");
        
        var comissoes = CalculadoraComissao.CalcularComissoes(DadosExemplo.JsonVendas);
        
        foreach (var comissao in comissoes)
        {
            Console.WriteLine($"Vendedor: {comissao.Vendedor}");
            Console.WriteLine($"Quantidade de Vendas: {comissao.QuantidadeVendas}");
            Console.WriteLine($"Total em Vendas: R$ {comissao.TotalVendas:F2}");
            Console.WriteLine($"Comissão Total: R$ {comissao.TotalComissao:F2}");
            Console.WriteLine($"Percentual de Comissão: {(comissao.TotalComissao / comissao.TotalVendas * 100):F2}%");
            Console.WriteLine("---");
        }

        // 2. CONTROLE DE ESTOQUE
        Console.WriteLine("\n2. SISTEMA DE CONTROLE DE ESTOQUE");
        Console.WriteLine("=================================\n");
        
        var gerenciadorEstoque = new GerenciadorEstoque(DadosExemplo.JsonEstoque);
        
        // Exibir estoque inicial
        Console.WriteLine("Estoque Inicial:");
        foreach (var produto in gerenciadorEstoque.ObterSituacaoEstoque())
        {
            Console.WriteLine($"  {produto.CodigoProduto}: {produto.DescricaoProduto} - {produto.Estoque} unidades");
        }

        // Realizar movimentações
        Console.WriteLine("\nRealizando movimentações...");
        
        try
        {
            // Entrada de estoque
            var mov1 = gerenciadorEstoque.RegistrarMovimentacao(101, "Compra de fornecedor", 50, "ENTRADA");
            Console.WriteLine($"✓ Entrada registrada: {mov1.Descricao} - {mov1.Quantidade} unidades");
            Console.WriteLine($"  Estoque atual de Caneta Azul: {gerenciadorEstoque.ObterEstoqueAtual(101)} unidades");

            // Saída de estoque
            var mov2 = gerenciadorEstoque.RegistrarMovimentacao(101, "Venda para cliente", 30, "SAIDA");
            Console.WriteLine($"✓ Saída registrada: {mov2.Descricao} - {mov2.Quantidade} unidades");
            Console.WriteLine($"  Estoque atual de Caneta Azul: {gerenciadorEstoque.ObterEstoqueAtual(101)} unidades");

            // Mais movimentações
            gerenciadorEstoque.RegistrarMovimentacao(102, "Reposição de estoque", 25, "ENTRADA");
            gerenciadorEstoque.RegistrarMovimentacao(103, "Venda atacado", 50, "SAIDA");
            gerenciadorEstoque.RegistrarMovimentacao(105, "Promoção especial", 20, "SAIDA");

            Console.WriteLine("\nEstoque Final:");
            foreach (var produto in gerenciadorEstoque.ObterSituacaoEstoque())
            {
                Console.WriteLine($"  {produto.CodigoProduto}: {produto.DescricaoProduto} - {produto.Estoque} unidades");
            }

            // Histórico de movimentações
            Console.WriteLine("\nHistórico de Movimentações:");
            foreach (var mov in gerenciadorEstoque.ObterHistoricoMovimentacoes().Take(5))
            {
                Console.WriteLine($"  #{mov.Id} - {mov.DataMovimentacao:dd/MM/yyyy HH:mm} - {mov.Tipo} - {mov.Quantidade} unidades - {mov.Descricao}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }

        // 3. CÁLCULO DE JUROS
        Console.WriteLine("\n3. CÁLCULO DE JUROS POR ATRASO");
        Console.WriteLine("==============================\n");
        
        // Exemplo 1
        decimal valor1 = 1000.00m;
        DateTime vencimento1 = new DateTime(2024, 1, 1);
        
        var resultado1 = CalculadoraJuros.CalcularJuros(valor1, vencimento1);
        
        Console.WriteLine($"Exemplo 1:");
        Console.WriteLine($"  Valor original: R$ {valor1:F2}");
        Console.WriteLine($"  Data vencimento: {vencimento1:dd/MM/yyyy}");
        Console.WriteLine($"  Dias em atraso: {resultado1.diasAtraso}");
        Console.WriteLine($"  Juros ({resultado1.diasAtraso} dias × 2.5%): R$ {resultado1.juros:F2}");
        Console.WriteLine($"  Valor total: R$ {resultado1.valorTotal:F2}");

        // Exemplo 2
        decimal valor2 = 500.00m;
        DateTime vencimento2 = DateTime.Today.AddDays(-10); // 10 dias atrás
        
        var resultado2 = CalculadoraJuros.CalcularJuros(valor2, vencimento2);
        
        Console.WriteLine($"\nExemplo 2:");
        Console.WriteLine($"  Valor original: R$ {valor2:F2}");
        Console.WriteLine($"  Data vencimento: {vencimento2:dd/MM/yyyy}");
        Console.WriteLine($"  Dias em atraso: {resultado2.diasAtraso}");
        Console.WriteLine($"  Juros ({resultado2.diasAtraso} dias × 2.5%): R$ {resultado2.juros:F2}");
        Console.WriteLine($"  Valor total: R$ {resultado2.valorTotal:F2}");

        // Exemplo 3 - Sem atraso
        decimal valor3 = 750.00m;
        DateTime vencimento3 = DateTime.Today.AddDays(5); // 5 dias no futuro
        
        var resultado3 = CalculadoraJuros.CalcularJuros(valor3, vencimento3);
        
        Console.WriteLine($"\nExemplo 3 (Sem atraso):");
        Console.WriteLine($"  Valor original: R$ {valor3:F2}");
        Console.WriteLine($"  Data vencimento: {vencimento3:dd/MM/yyyy}");
        Console.WriteLine($"  Dias em atraso: {resultado3.diasAtraso}");
        Console.WriteLine($"  Juros: R$ {resultado3.juros:F2}");
        Console.WriteLine($"  Valor total: R$ {resultado3.valorTotal:F2}");

        Console.WriteLine("\n=== PROGRAMA FINALIZADO ===");
    }
}