using DesafioTarget.Comissoes;
using DesafioTarget.Compartilhado;

namespace DesafioTarget.Tests;

public class ComissaoTests
{
    [Theory]
    [InlineData(0.01, 0)]
    [InlineData(99.99, 0)]
    [InlineData(100, 0.01)]
    [InlineData(499.99, 0.01)]
    [InlineData(500, 0.05)]
    [InlineData(2100.40, 0.05)]
    public void Percentual_respeita_as_faixas(decimal valor, decimal esperado)
    {
        Assert.Equal(esperado, CalculadoraComissao.Percentual(valor));
    }

    [Fact]
    public void Comissao_e_calculada_venda_a_venda()
    {
        var vendas = new List<Venda>
        {
            new("Fulano", 90m),
            new("Fulano", 200m),
            new("Fulano", 1000m)
        };

        var resumo = Assert.Single(CalculadoraComissao.CalcularPorVendedor(vendas));

        Assert.Equal(3, resumo.QuantidadeVendas);
        Assert.Equal(1290m, resumo.TotalVendido);
        Assert.Equal(52m, resumo.Comissao);
    }

    [Fact]
    public void Calcula_a_comissao_de_cada_vendedor_do_arquivo()
    {
        var vendas = ArquivoJson.Ler<ArquivoVendas>("vendas.json").Vendas;

        var comissoes = CalculadoraComissao.CalcularPorVendedor(vendas)
            .ToDictionary(r => r.Vendedor, r => r.Comissao);

        Assert.Equal(495.68m, comissoes["João Silva"]);
        Assert.Equal(465.95m, comissoes["Maria Souza"]);
        Assert.Equal(379.37m, comissoes["Carlos Oliveira"]);
        Assert.Equal(404.98m, comissoes["Ana Lima"]);
    }
}
