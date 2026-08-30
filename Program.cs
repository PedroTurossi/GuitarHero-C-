using Raylib_cs;
using System.Numerics;
using System;

class Program {
    // alvo
    // static List<List<float>> notas;
    public const int larguraTela = 800;
    public const int alturaTela = 400;

    static void Main() {
        // CONFIG DE TELAS - Alterar aqui a tela inicial para Debug
        IScreen telaInicial = new GameScreen();
        ScreenManager screenManager = new ScreenManager(telaInicial);

        // CONFIG

            // fazer um "nível" onde as notas vem do meio, do centro da tela
            // e a partir das letras exibidas na tela, o jogador precisa "marcar" no teclado onde elas aparecem.

            Raylib.InitWindow(larguraTela, alturaTela, "Jogasso");
            Raylib.SetTargetFPS(120);


        while(!Raylib.WindowShouldClose()) {
            
            // ***  UPDATE  ***
            float deltaTime = Raylib.GetFrameTime();
            screenManager.Update(deltaTime);
            

            // --=< DRAW >=--
            Raylib.BeginDrawing();
            screenManager.Draw();
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();            
    }
}