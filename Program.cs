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



            Raylib.InitWindow(larguraTela, alturaTela, "Jogasso");
            Raylib.SetTargetFPS(60);


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