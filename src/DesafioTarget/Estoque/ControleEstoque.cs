namespace DesafioTarget.Estoque;

public class ControleEstoque
{
    private readonly Dictionary<int, Produto> _produtos;
    private readonly List<Movimentacao> _movimentacoes = [];
    private int _ultimoId;

    public ControleEstoque(IEnumerable<Produto> produtos)
    {
        _produtos = produtos.ToDictionary(p => p.CodigoProduto);
    }

    public IReadOnlyCollection<Produto> Produtos => _produtos.Values;

    public IReadOnlyList<Movimentacao> Movimentacoes => _movimentacoes;

    public Produto BuscarProduto(int codigo) =>
        _produtos.TryGetValue(codigo, out var produto)
            ? produto
            : throw new MovimentacaoInvalidaException($"Produto {codigo} não encontrado.");

    public Movimentacao Movimentar(int codigoProduto, TipoMovimentacao tipo, int quantidade, string descricao)
    {
        var produto = BuscarProduto(codigoProduto);

        if (quantidade <= 0)
            throw new MovimentacaoInvalidaException("A quantidade deve ser maior que zero.");

        if (string.IsNullOrWhiteSpace(descricao))
            throw new MovimentacaoInvalidaException("Informe uma descrição para a movimentação.");

        if (tipo == TipoMovimentacao.Saida && quantidade > produto.Estoque)
            throw new MovimentacaoInvalidaException(
                $"Estoque insuficiente de {produto.DescricaoProduto}: disponível {produto.Estoque}, solicitado {quantidade}.");

        var estoqueAnterior = produto.Estoque;
        produto.Estoque += tipo == TipoMovimentacao.Entrada ? quantidade : -quantidade;

        var movimentacao = new Movimentacao(
            ++_ultimoId,
            DateTime.Now,
            produto.CodigoProduto,
            tipo,
            quantidade,
            descricao.Trim(),
            estoqueAnterior,
            produto.Estoque);

        _movimentacoes.Add(movimentacao);
        return movimentacao;
    }
}
