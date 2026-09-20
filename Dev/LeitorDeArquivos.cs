using System.Drawing;
using System.IO;
using System.Text.Json;


public class Arquivo {
    public List<List<float>> notas {get; set;} = new();
}

class LeitorDeArquivos {
    public static string[] LerDiretorio(string diretorio) {
        string[] arquivos = Directory.GetFiles(diretorio, "*.json");
        return arquivos;
    }

    public static string[] LerImagensDoDiretorio(string diretorio) {
        string[] arquivos = Directory.GetFiles(diretorio, "*.png");
        return arquivos;
    }
}

// public class LeitorDeConfigs {
//     public int larguraDaTela;
//     public int alturaDaTela;

//     // public ? corPrincipal;
//     // public ? corSecundaria;



// }
