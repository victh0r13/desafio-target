using DesafioTarget.Compartilhado;

namespace DesafioTarget.Comissoes;

public static class ComissaoTela
{
    public static void Exibir()
    {
        var vendas = ArquivoJson.Ler<ArquivoVendas>("vendas.json").Vendas;
        var resumo = CalculadoraComissao.CalcularPorVendedor(vendas);

        Console.WriteLine("\n=== Comissão dos vendedores ===\n");
        Console.WriteLine($"{"Vendedor",-20}{"Vendas",8}{"Total vendido",20}{"Comissão",16}");
        Console.WriteLine(new string('-', 64));

        foreach (var item in resumo)
        {
            Console.WriteLine(
                $"{item.Vendedor,-20}{item.QuantidadeVendas,8}" +
                $"{Moeda.Formatar(item.TotalVendido),20}{Moeda.Formatar(item.Comissao),16}");
        }

        Console.WriteLine(new string('-', 64));
        Console.WriteLine($"{"Total de comissões",-48}{Moeda.Formatar(resumo.Sum(r => r.Comissao)),16}");
    }
}
