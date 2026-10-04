namespace DesafioTarget.Compartilhado;

public static class Leitor
{
    public static string Texto(string mensagem)
    {
        Console.Write(mensagem);
        return Console.ReadLine()?.Trim() ?? throw new EndOfStreamException();
    }

    public static int Inteiro(string mensagem)
    {
        while (true)
        {
            if (int.TryParse(Texto(mensagem), out var numero))
                return numero;

            Console.WriteLine("Digite um número inteiro válido.");
        }
    }

    public static void Pausar()
    {
        Console.Write("\nPressione ENTER para voltar ao menu...");
        Console.ReadLine();
    }
}
