using DesafioTarget.Compartilhado;

namespace DesafioTarget.Juros;

public record ResultadoJuros(
    decimal ValorOriginal,
    DateOnly Vencimento,
    DateOnly DataCalculo,
    int DiasAtraso,
    decimal Juros,
    decimal ValorAtualizado);

public static class CalculadoraJuros
{
    public const decimal TaxaDiaria = 0.025m;

    public static int DiasEmAtraso(DateOnly vencimento, DateOnly hoje) =>
        Math.Max(hoje.DayNumber - vencimento.DayNumber, 0);

    public static ResultadoJuros Calcular(decimal valor, DateOnly vencimento) =>
        Calcular(valor, vencimento, DateOnly.FromDateTime(DateTime.Today));

    public static ResultadoJuros Calcular(decimal valor, DateOnly vencimento, DateOnly hoje)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor precisa ser maior que zero.", nameof(valor));

        var dias = DiasEmAtraso(vencimento, hoje);
        var valorOriginal = Moeda.Arredondar(valor);
        var juros = Moeda.Arredondar(valor * TaxaDiaria * dias);

        return new ResultadoJuros(valorOriginal, vencimento, hoje, dias, juros, valorOriginal + juros);
    }
}
