namespace DesafioTarget.Comissoes;

public record Venda(string Vendedor, decimal Valor);

public record ArquivoVendas(List<Venda> Vendas);
