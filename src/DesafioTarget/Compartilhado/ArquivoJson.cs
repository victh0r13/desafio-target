using System.Text.Json;

namespace DesafioTarget.Compartilhado;

public static class ArquivoJson
{
    private static readonly JsonSerializerOptions Opcoes = new() { PropertyNameCaseInsensitive = true };

    public static T Ler<T>(string nomeArquivo)
    {
        var caminho = Path.Combine(AppContext.BaseDirectory, "Dados", nomeArquivo);
        var conteudo = File.ReadAllText(caminho);

        return JsonSerializer.Deserialize<T>(conteudo, Opcoes)
            ?? throw new InvalidDataException($"Não foi possível ler o arquivo {nomeArquivo}.");
    }
}
