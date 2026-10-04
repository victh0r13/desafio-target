using DesafioTarget.Compartilhado;

namespace DesafioTarget.Estoque;

public class EstoqueTela(ControleEstoque estoque)
{
    public void Exibir()
    {
        while (true)
        {
            Console.WriteLine("\n=== Controle de estoque ===");
            Console.WriteLine("1 - Listar produtos");
            Console.WriteLine("2 - Lançar entrada");
            Console.WriteLine("3 - Lançar saída");
            Console.WriteLine("4 - Ver movimentações");
            Console.WriteLine("0 - Voltar");

            switch (Leitor.Texto("Opção: "))
            {
                case "1": ListarProdutos(); break;
                case "2": Lancar(TipoMovimentacao.Entrada); break;
                case "3": Lancar(TipoMovimentacao.Saida); break;
                case "4": ListarMovimentacoes(); break;
                case "0": return;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }
    }

    private void ListarProdutos()
    {
        Console.WriteLine($"\n{"Código",-8}{"Produto",-30}{"Estoque",8}");
        foreach (var produto in estoque.Produtos)
            Console.WriteLine($"{produto.CodigoProduto,-8}{produto.DescricaoProduto,-30}{produto.Estoque,8}");
    }

    private void Lancar(TipoMovimentacao tipo)
    {
        ListarProdutos();

        var codigo = Leitor.Inteiro("\nCódigo do produto: ");
        var quantidade = Leitor.Inteiro("Quantidade: ");
        var descricao = Leitor.Texto("Descrição da movimentação: ");

        try
        {
            var mov = estoque.Movimentar(codigo, tipo, quantidade, descricao);
            var produto = estoque.BuscarProduto(mov.CodigoProduto);

            Console.WriteLine($"\nMovimentação #{mov.Id} registrada ({mov.Descricao}).");
            Console.WriteLine($"{produto.DescricaoProduto}: {mov.EstoqueAnterior} -> {mov.EstoqueFinal}");
            Console.WriteLine($"Estoque final: {mov.EstoqueFinal}");
        }
        catch (MovimentacaoInvalidaException ex)
        {
            Console.WriteLine($"\nNão foi possível lançar: {ex.Message}");
        }
    }

    private void ListarMovimentacoes()
    {
        if (estoque.Movimentacoes.Count == 0)
        {
            Console.WriteLine("\nNenhuma movimentação lançada ainda.");
            return;
        }

        Console.WriteLine();
        foreach (var mov in estoque.Movimentacoes)
        {
            var produto = estoque.BuscarProduto(mov.CodigoProduto);
            var sinal = mov.Tipo == TipoMovimentacao.Entrada ? "+" : "-";

            Console.WriteLine(
                $"#{mov.Id,-4}{mov.Data:dd/MM/yyyy HH:mm}  {produto.DescricaoProduto,-28}" +
                $"{sinal + mov.Quantidade,-7}{mov.Descricao,-30}saldo: {mov.EstoqueFinal}");
        }
    }
}
