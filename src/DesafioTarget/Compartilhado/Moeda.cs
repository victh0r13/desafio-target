using System.Globalization;

namespace DesafioTarget.Compartilhado;

public static class Moeda
{
    public static readonly CultureInfo Cultura = new("pt-BR");

    public static decimal Arredondar(decimal valor) =>
        Math.Round(valor, 2, MidpointRounding.AwayFromZero);

    public static string Formatar(decimal valor) =>
        Arredondar(valor).ToString("C", Cultura);
}
