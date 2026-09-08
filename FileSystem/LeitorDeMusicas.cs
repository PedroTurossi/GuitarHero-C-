using System.Drawing;
using System.IO;
using System.Text.Json;



public class Musica {
    public string nomeMusica {get; set;}
    public string nomeDoArquivoDaMusica {get; set;}
    public List<List<float>> notas {get; set;} = new();
}

//  ------------------------------ //

class LeitorDeMusicas {
    public bool temporaria = true;
    static float tempoExtraDeDescida;

    public static Musica musica;
    public LeitorDeMusicas(String caminhoDoArquivo) {
        string jsonString = File.ReadAllText(caminhoDoArquivo);
        Musica arq = JsonSerializer.Deserialize<Musica>(jsonString);
        arq.notas.Add([-1f, 0f, 0f]);
        musica = arq;
        // depois o arquivo vai ter que sobreescrever a velocidadeDeMovimento de Nota ...

        tempoExtraDeDescida = ((Program.alturaTela)/(Nota.velocidadeDeMovimento)*1000);
    }

    public static void UpdateLoopMusica() {
        // "notas" consiste em uma lista de lista de floats, onde cada lista de lista representa um conjunto de "nota"
        // e cada lista de float (ou seja, cada nota), tem 3 números. o primeiro significa o tempo até ela ser reproduzida, sendo
        // que o tempo reinicia a cada nota. ou seja: 0.1 0.1 0.1 acontecem com 0.1 de espaçamento entre as notas
        // o segundo número significa "up" (1) ou "down" (2), representando em qual dos alvos a nota vai mirar
        // 
        if(musica.notas[0][0] == -1 || musica.notas[0][1] == 0) {
            // arquivo acabou
        } else {
            if (GameScreen.timer >= musica.notas[0][0]) {
                    Nota novaNota = new Nota((int)musica.notas[0][2]);
                    musica.notas.RemoveAt(0);   
            }            
        }

    }
    public void UpdateMusica() {
        if(musica.notas.Count > 0 && musica.notas[0][0] != -1){
            // Console.WriteLine(GameScreen.timer);
            // Console.WriteLine(GameScreen.timer + " - " + (GameScreen.timer >= musica.notas[0][0] - tempoExtraDeDescida));
            if (GameScreen.timer >= musica.notas[0][0] - tempoExtraDeDescida) {
                    Nota novaNota = new Nota((int)musica.notas[0][1]);
                    musica.notas.RemoveAt(0);   
            }            
        } else {
            // jogo acabou
        }

    }

    public string ObterNomeDoArquivoDaMusica() {
        return musica.nomeDoArquivoDaMusica;
    }
}