namespace DesafioTarget.Estoque;

public class Produto
{
    public int CodigoProduto { get; init; }
    public string DescricaoProduto { get; init; } = string.Empty;
    public int Estoque { get; set; }
}

public record ArquivoEstoque(List<Produto> Estoque);
