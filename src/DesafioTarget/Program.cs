using System.Text;
using DesafioTarget.Comissoes;
using DesafioTarget.Compartilhado;
using DesafioTarget.Estoque;
using DesafioTarget.Juros;

Console.OutputEncoding = Encoding.UTF8;

var estoque = new ControleEstoque(ArquivoJson.Ler<ArquivoEstoque>("estoque.json").Estoque);
var estoqueTela = new EstoqueTela(estoque);

try
{
    while (true)
    {
        Console.WriteLine("\n===== Desafio Target Sistemas =====");
        Console.WriteLine("1 - Comissão dos vendedores");
        Console.WriteLine("2 - Movimentação de estoque");
        Console.WriteLine("3 - Cálculo de juros");
        Console.WriteLine("0 - Sair");

        switch (Leitor.Texto("Opção: "))
        {
            case "1":
                ComissaoTela.Exibir();
                Leitor.Pausar();
                break;
            case "2":
                estoqueTela.Exibir();
                break;
            case "3":
                JurosTela.Exibir();
                Leitor.Pausar();
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }
}
catch (EndOfStreamException)
{
    Console.WriteLine();
}
