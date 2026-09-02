using Raylib_cs;
using System.Numerics;
using System;

class Program {
    // alvo
    // static List<List<float>> notas;
    public const int larguraTela = 800;
    public const int alturaTela = 400;

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

            Raylib.InitAudioDevice();
            // Music musicaBraba = Raylib.LoadMusicStream("Files/RapDoMinecraft.mp3");
            // Raylib.PlayMusicStream(musicaBraba);


        // CONFIG DE TELAS - Alterar aqui a tela inicial para Debug
        ScreenManager screenManager = new ScreenManager();
        screenManager.ChangeScreen(new GameScreen());

        while(!Raylib.WindowShouldClose()) {
            
            // ***  UPDATE  ***
            float deltaTime = Raylib.GetFrameTime();
            screenManager.Update(deltaTime);
            
            // Raylib.UpdateMusicStream(musicaBraba);

            // --=< DRAW >=--
            Raylib.BeginDrawing();
            screenManager.Draw();
            Raylib.EndDrawing();
        }
        // fazer alguma coisa pra descarregar as texturas
        screenManager.UnloadTextures();

        // Raylib.UnloadMusicStream(musicaBraba);
        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();            
    }
}