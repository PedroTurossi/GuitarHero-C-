using Raylib_cs;
using System.Numerics;
using System;

class Program {
    // alvo
    // static List<List<float>> notas;
    public const int larguraTela = 800;
    public const int alturaTela = 400;
    public static Vector2 posicaoDoMouse;
    public static string localDosArquivos = "Files/";

    static void Main() {
        // CONFIG

            // fazer um "nível" onde as notas vem do meio, do centro da tela
            // e a partir das letras exibidas na tela, o jogador precisa "marcar" no teclado onde elas aparecem.

            // vou anotar aqui pra quando eu tiver net: seria legal fazer um menu, daí seleciona as músicas e tal.
            // deixar as coisas bem enfeitadinhas
            // fazer interface com pontuação, efeitos e palvras (tipo em fnf)
            // fazer sons de acertos, erros, partículas...


            Raylib.InitWindow(larguraTela, alturaTela, "Jogasso");
            Raylib.SetTargetFPS(120);        

            AudioManager.InicializarAudioManager();


        // CONFIG DE TELAS - Alterar aqui a tela inicial para Debug
        ScreenManager.ChangeScreen(new LevelSelectScreen());

        while(!Raylib.WindowShouldClose()) {
            
            // ***  UPDATE  ***
            float deltaTime = Raylib.GetFrameTime();
            posicaoDoMouse = Raylib.GetMousePosition(); // <-- depois posso colocar em outro lugar, mas é para não precisar puxar ele em várias instâncias
            ScreenManager.Update(deltaTime);

            
            AudioManager.UpdateMusica();

            // --=< DRAW >=--
            Raylib.BeginDrawing();
            ScreenManager.Draw();
            Raylib.EndDrawing();
        }
        // fazer alguma coisa pra descarregar as texturas
        ScreenManager.UnloadTextures();
        AudioManager.UpdateMusica();

        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();            
    }
}