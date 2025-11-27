using System.Text.Json;
using DesafioDev.Models;

namespace DesafioDev.Services
{
    public class GerenciadorEstoque
    {
        private List<Produto> _produtos;
        private List<Movimentacao> _movimentacoes;
        private int _proximoId;
        
        public GerenciadorEstoque(string jsonEstoque)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var estoqueData = JsonSerializer.Deserialize<EstoqueData>(jsonEstoque, options) 
                           ?? new EstoqueData();
            _produtos = estoqueData.Estoque;
            _movimentacoes = new List<Movimentacao>();
            _proximoId = 1;
        }
        
        public Movimentacao RegistrarMovimentacao(int codigoProduto, string descricao, int quantidade, string tipo)
        {
            var produto = _produtos.FirstOrDefault(p => p.CodigoProduto == codigoProduto);
            if (produto == null)
                throw new ArgumentException($"Produto com código {codigoProduto} não encontrado");
                
            if (tipo != "ENTRADA" && tipo != "SAIDA")
                throw new ArgumentException("Tipo deve ser 'ENTRADA' ou 'SAIDA'");
                
            if (tipo == "SAIDA" && produto.Estoque < quantidade)
                throw new InvalidOperationException($"Estoque insuficiente. Disponível: {produto.Estoque}, Solicitado: {quantidade}");
            
            // Atualiza estoque
            if (tipo == "ENTRADA")
                produto.Estoque += quantidade;
            else
                produto.Estoque -= quantidade;
            
            // Registra movimentação
            var movimentacao = new Movimentacao
            {
                Id = _proximoId++,
                CodigoProduto = codigoProduto,
                Descricao = descricao,
                Quantidade = quantidade,
                DataMovimentacao = DateTime.Now,
                Tipo = tipo
            };
            
            _movimentacoes.Add(movimentacao);
            return movimentacao;
        }
        
        public int ObterEstoqueAtual(int codigoProduto)
        {
            var produto = _produtos.FirstOrDefault(p => p.CodigoProduto == codigoProduto);
            return produto?.Estoque ?? 0;
        }
        
        public List<Movimentacao> ObterHistoricoMovimentacoes()
        {
            return _movimentacoes.OrderByDescending(m => m.DataMovimentacao).ToList();
        }
        
        public List<Produto> ObterSituacaoEstoque()
        {
            return _produtos.OrderBy(p => p.CodigoProduto).ToList();
        }
        
        public Produto? ObterProduto(int codigoProduto)
        {
            return _produtos.FirstOrDefault(p => p.CodigoProduto == codigoProduto);
        }
    }
}