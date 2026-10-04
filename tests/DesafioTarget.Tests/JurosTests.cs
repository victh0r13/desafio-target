using DesafioTarget.Juros;

namespace DesafioTarget.Tests;

public class JurosTests
{
    private static readonly DateOnly Hoje = new(2026, 10, 4);

    [Fact]
    public void Calcula_juros_de_dez_dias_de_atraso()
    {
        var resultado = CalculadoraJuros.Calcular(1000m, new DateOnly(2026, 9, 24), Hoje);

        Assert.Equal(10, resultado.DiasAtraso);
        Assert.Equal(250m, resultado.Juros);
        Assert.Equal(1250m, resultado.ValorAtualizado);
    }

    [Theory]
    [InlineData(2026, 10, 4)]
    [InlineData(2026, 10, 10)]
    public void Nao_ha_juros_sem_atraso(int ano, int mes, int dia)
    {
        var resultado = CalculadoraJuros.Calcular(500m, new DateOnly(ano, mes, dia), Hoje);

        Assert.Equal(0, resultado.DiasAtraso);
        Assert.Equal(0m, resultado.Juros);
        Assert.Equal(500m, resultado.ValorAtualizado);
    }

    [Fact]
    public void Arredonda_os_juros_para_centavos()
    {
        var resultado = CalculadoraJuros.Calcular(123.45m, new DateOnly(2026, 10, 1), Hoje);

        Assert.Equal(9.26m, resultado.Juros);
    }

    [Fact]
    public void Nao_aceita_valor_menor_ou_igual_a_zero()
    {
        Assert.Throws<ArgumentException>(() => CalculadoraJuros.Calcular(0m, Hoje, Hoje));
    }

    [Theory]
    [InlineData("1.234,56", 1234.56)]
    [InlineData("R$ 99,90", 99.90)]
    [InlineData("1500.75", 1500.75)]
    [InlineData("200", 200)]
    public void Converte_valores_digitados(string texto, decimal esperado)
    {
        Assert.True(ConversorEntrada.TentarConverterValor(texto, out var valor));
        Assert.Equal(esperado, valor);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("")]
    public void Rejeita_valores_invalidos(string texto)
    {
        Assert.False(ConversorEntrada.TentarConverterValor(texto, out _));
    }

    [Fact]
    public void Converte_data_no_formato_brasileiro()
    {
        Assert.True(ConversorEntrada.TentarConverterData("05/03/2026", out var data));
        Assert.Equal(new DateOnly(2026, 3, 5), data);
        Assert.False(ConversorEntrada.TentarConverterData("2026-03-05", out _));
    }
}
