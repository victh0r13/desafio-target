namespace DesafioTarget.Estoque;

public enum TipoMovimentacao
{
    Entrada,
    Saida
}

public record Movimentacao(
    int Id,
    DateTime Data,
    int CodigoProduto,
    TipoMovimentacao Tipo,
    int Quantidade,
    string Descricao,
    int EstoqueAnterior,
    int EstoqueFinal);
