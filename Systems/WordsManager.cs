using System.Numerics;
using Raylib_cs;

class Palavra : IGameObject {
    public bool excluirObjeto { get; set; }
    public Color cor { get; set; }
    public Vector2 posicaoObjeto { get; set; }



    bool palavraSumindo;
    float tempoDePalavraNaTela = 0.8f;
    float tempoRestanteDePalavraNaTela = 0.8f;

    int tamanhoDaFonte;
    string fonte;
    string texto;

    static Random rand = new Random();

    public Palavra(Vector2 posicaoDaPalavra, int fontSize, string textoASerEscrito, Color corDoTexto, bool palavraTemporaria) {
        int direcao = (rand.Next(0, 1) == 0) ? -1 : 1;
        float distanciaX = (float)(rand.NextDouble() * (Program.larguraTela * 0.03f) );
        int posicaoX = (int)(posicaoDaPalavra.X + direcao * distanciaX);

        direcao = (rand.Next(0, 1) == 0) ? -1 : 1;
        float distanciaY = (float)(rand.NextDouble() * (Program.alturaTela * 0.03f) );
        int posicaoY = (int)(posicaoDaPalavra.Y + direcao * distanciaY);
    
        posicaoObjeto = new Vector2(posicaoX, posicaoY);


        tamanhoDaFonte = fontSize;
        texto = textoASerEscrito;
        palavraSumindo = palavraTemporaria;
        cor = corDoTexto;
    }



    public void Update(float dt) {
        if (palavraSumindo) {
            if (tempoRestanteDePalavraNaTela <= 0) {
                excluirObjeto = true;
            } else {
                if ((int)cor.A > 0) {
                    Color corTemporaria = cor;
                    float proporcao = tempoRestanteDePalavraNaTela/tempoDePalavraNaTela;
                    corTemporaria.A = (byte)(proporcao * 255);
                    cor = corTemporaria;
                }
            }
            tempoRestanteDePalavraNaTela -= dt;
        }


    }

    public void Draw() {
        // Raylib.DrawCircleV(posicaoObjeto, 3f, Color.Red);

        
        int larguraTexto = Raylib.MeasureText(texto, tamanhoDaFonte);
        int xTexto = (int)(posicaoObjeto.X - (larguraTexto) / 2);
        int yTexto = (int)(posicaoObjeto.Y );

        Raylib.DrawText(
            texto,
            xTexto,
            yTexto,
            tamanhoDaFonte,
            cor
        );
    }

    public void Load() {
        throw new NotImplementedException();
    }
}

class WordsManager {

    private static List<IGameObject> listaDePalavras = new List<IGameObject>();


    public static void AdicionarPalavra(Vector2 posicaoDaPalavraAdicionada, int tamanhoDaFonte, string TextoDaPalavra, Color? corDoTexto = null, bool textoTemporario = false) {
        Palavra teste = new Palavra(posicaoDaPalavraAdicionada, tamanhoDaFonte, TextoDaPalavra, corDoTexto ?? Color.White, textoTemporario);
        listaDePalavras.Add(teste);
    }

    public static void UpdatePalavras(float deltaTime) {
        foreach (IGameObject palavra in listaDePalavras) {
            palavra.Update(deltaTime);
        }
    }

    public static void DesenharPalavras() {
        foreach (IGameObject palavra in listaDePalavras) {
            palavra.Draw();
        }
        listaDePalavras.RemoveAll(palavra => palavra.excluirObjeto);
    }

}