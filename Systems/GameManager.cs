using System.Numerics;
using Raylib_cs;

class GameManager {

    private static int scoreBoard = 0;

    public static void IniciarJogo() {
        scoreBoard = 0;
        // Adicionar depois um carregadore de texturas?
    }

    public static void RegistrarPontuacao(string pontuacaoString, Vector2 posicaoParaEscrever) {
        string texto = "";
        Color corDaPontuacao = Color.White;
        int pontuacao = 0;

        switch(pontuacaoString){
            case "Great":
                texto = "+Great+";
                pontuacao = 7;
                break;

            case "Good":
                texto = "Good+";
                pontuacao = 4;
                break;

            case "Bad":
                texto = "Bad-";
                pontuacao = 0;
                corDaPontuacao = Color.LightGray;
                break;

            case "Miss":
                pontuacao = -2;
                texto = "-Miss-";
                corDaPontuacao = Color.Gray;
                break;
        
        }
 
        AdicionarPontuacao(pontuacao);
        EscreverPontuacao(texto, posicaoParaEscrever, corDaPontuacao);
    }


    public static void AdicionarPontuacao(int pontuacao) {
        scoreBoard += pontuacao;
    }

    public static void EscreverPontuacao(string pontuacaoString, Vector2 posicaoParaEscrever, Color corDaPontuacao) {
        WordsManager.AdicionarPalavra(posicaoParaEscrever, 14, pontuacaoString, corDaPontuacao, true);
    }

}