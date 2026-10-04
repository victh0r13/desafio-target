using DesafioTarget.Compartilhado;

namespace DesafioTarget.Comissoes;

public static class CalculadoraComissao
{
    public static decimal Percentual(decimal valorVenda) => valorVenda switch
    {
        < 100m => 0m,
        < 500m => 0.01m,
        _ => 0.05m
    };

    public static decimal ComissaoDaVenda(decimal valorVenda) => valorVenda * Percentual(valorVenda);

    public static List<ResumoVendedor> CalcularPorVendedor(IEnumerable<Venda> vendas) =>
        vendas
            .GroupBy(v => v.Vendedor)
            .Select(grupo => new ResumoVendedor(
                grupo.Key,
                grupo.Count(),
                Moeda.Arredondar(grupo.Sum(v => v.Valor)),
                Moeda.Arredondar(grupo.Sum(v => ComissaoDaVenda(v.Valor)))))
            .ToList();
}
