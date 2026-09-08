using System.Drawing;
using System.IO;
using System.Text.Json;


public class Arquivo {
    public List<List<float>> notas {get; set;} = new();
}

class LeitorDeArquivos {
    public Arquivo arquivo;

    string[] arquivosDoDiretorio;

    public LeitorDeArquivos() {
        // string jsonString = File.ReadAllText(caminhoDoArquivo);
        // Arquivo arq = JsonSerializer.Deserialize<Arquivo>(jsonString);
        // arq.notas.Add([-1f, 0f, 0f]);
        // arquivo = arq;
    }

    public static string[] LerDiretorio(string diretorio) {
        string[] arquivos = Directory.GetFiles(diretorio, "*.json");
        return arquivos;
    }
}

// public class LeitorDeConfigs {
//     public int larguraDaTela;
//     public int alturaDaTela;

//     // public ? corPrincipal;
//     // public ? corSecundaria;



// }