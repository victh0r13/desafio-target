using DesafioTarget.Compartilhado;
using DesafioTarget.Estoque;

namespace DesafioTarget.Tests;

public class EstoqueTests
{
    private readonly ControleEstoque _estoque =
        new(ArquivoJson.Ler<ArquivoEstoque>("estoque.json").Estoque);

    [Fact]
    public void Entrada_soma_no_estoque()
    {
        var mov = _estoque.Movimentar(101, TipoMovimentacao.Entrada, 50, "Compra de fornecedor");

        Assert.Equal(150, mov.EstoqueAnterior);
        Assert.Equal(200, mov.EstoqueFinal);
        Assert.Equal(200, _estoque.BuscarProduto(101).Estoque);
    }

    [Fact]
    public void Saida_subtrai_do_estoque()
    {
        var mov = _estoque.Movimentar(102, TipoMovimentacao.Saida, 25, "Venda");

        Assert.Equal(50, mov.EstoqueFinal);
    }

    [Fact]
    public void Cada_movimentacao_recebe_um_id_unico()
    {
        var ids = Enumerable.Range(0, 5)
            .Select(_ => _estoque.Movimentar(103, TipoMovimentacao.Entrada, 1, "Ajuste").Id)
            .ToList();

        Assert.Equal(5, ids.Distinct().Count());
    }

    [Fact]
    public void Nao_permite_saida_maior_que_o_estoque()
    {
        Assert.Throws<MovimentacaoInvalidaException>(
            () => _estoque.Movimentar(105, TipoMovimentacao.Saida, 91, "Venda"));

        Assert.Equal(90, _estoque.BuscarProduto(105).Estoque);
        Assert.Empty(_estoque.Movimentacoes);
    }

    [Fact]
    public void Nao_permite_produto_inexistente()
    {
        Assert.Throws<MovimentacaoInvalidaException>(
            () => _estoque.Movimentar(999, TipoMovimentacao.Entrada, 1, "Compra"));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-3)]
    public void Nao_permite_quantidade_menor_ou_igual_a_zero(int quantidade)
    {
        Assert.Throws<MovimentacaoInvalidaException>(
            () => _estoque.Movimentar(104, TipoMovimentacao.Entrada, quantidade, "Compra"));
    }

    [Fact]
    public void Descricao_e_obrigatoria()
    {
        Assert.Throws<MovimentacaoInvalidaException>(
            () => _estoque.Movimentar(104, TipoMovimentacao.Entrada, 1, "   "));
    }
}
