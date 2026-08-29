using System.Drawing;
using System.IO;
using System.Text.Json;


public class Arquivo {
    public List<List<float>> notas {get; set;} = new();
}

class LeitorDeArquivos {
    public Arquivo musica;
    public LeitorDeArquivos(String caminhoDoArquivo) {
        string jsonString = File.ReadAllText(caminhoDoArquivo);
        Arquivo arq = JsonSerializer.Deserialize<Arquivo>(jsonString);
        arq.notas.Add([-1f, 0f, 0f]);
        musica = arq;
    }

}

public class LeitorDeConfigs {
    public int larguraDaTela;
    public int alturaDaTela;

    // public ? corPrincipal;
    // public ? corSecundaria;



}