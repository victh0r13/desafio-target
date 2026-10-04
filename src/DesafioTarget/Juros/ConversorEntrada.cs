using System.Globalization;
using DesafioTarget.Compartilhado;

namespace DesafioTarget.Juros;

public static class ConversorEntrada
{
    public static bool TentarConverterValor(string texto, out decimal valor)
    {
        var limpo = texto.Replace("R$", "").Replace(" ", "").Trim();

        var cultura = limpo.Contains(',') ? Moeda.Cultura : CultureInfo.InvariantCulture;
        return decimal.TryParse(limpo, NumberStyles.Number, cultura, out valor);
    }

    public static bool TentarConverterData(string texto, out DateOnly data) =>
        DateOnly.TryParseExact(texto.Trim(), "dd/MM/yyyy", Moeda.Cultura, DateTimeStyles.None, out data);
}
