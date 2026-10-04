using DesafioTarget.Compartilhado;

namespace DesafioTarget.Juros;

public static class JurosTela
{
    public static void Exibir()
    {
        Console.WriteLine("\n=== Cálculo de juros (2,5% ao dia) ===\n");

        decimal valor;
        while (!ConversorEntrada.TentarConverterValor(Leitor.Texto("Valor: "), out valor) || valor <= 0)
            Console.WriteLine("Valor inválido. Exemplo: 1.500,00");

        DateOnly vencimento;
        while (!ConversorEntrada.TentarConverterData(Leitor.Texto("Data de vencimento (dd/mm/aaaa): "), out vencimento))
            Console.WriteLine("Data inválida. Use o formato dd/mm/aaaa.");

        var resultado = CalculadoraJuros.Calcular(valor, vencimento);

        Console.WriteLine();
        Console.WriteLine($"Valor original:   {Moeda.Formatar(resultado.ValorOriginal)}");
        Console.WriteLine($"Vencimento:       {resultado.Vencimento:dd/MM/yyyy}");
        Console.WriteLine($"Data do cálculo:  {resultado.DataCalculo:dd/MM/yyyy}");
        Console.WriteLine($"Dias em atraso:   {resultado.DiasAtraso}");
        Console.WriteLine($"Juros:            {Moeda.Formatar(resultado.Juros)}");
        Console.WriteLine($"Valor atualizado: {Moeda.Formatar(resultado.ValorAtualizado)}");

        if (resultado.DiasAtraso == 0)
            Console.WriteLine("\nO título não está vencido, então não há juros.");
    }
}
