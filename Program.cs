using Raylib_cs;
using System.Numerics;
using System;

class Program {
    public const int larguraTela = 800;
    public const int alturaTela = 400;
    public static string localDosArquivos = "Files/";

    static void Main() {
        GameContext context = new GameContext(new GameSettings(larguraTela, alturaTela, localDosArquivos));

        Raylib.InitWindow(larguraTela, alturaTela, "Jogasso");
        Raylib.SetTargetFPS(120);        
        Raylib.SetExitKey(KeyboardKey.Null); 

        AudioManager.InicializarAudioManager();

        ScreenManager.Inicializar(context);
        ScreenManager.ChangeScreen(() => new LevelSelectScreen(context));

        while(!Raylib.WindowShouldClose() && !context.ExitRequested) {
            float deltaTime = Raylib.GetFrameTime();
            context.UpdateInput();
            ScreenManager.Update(deltaTime);

            Raylib.BeginDrawing();
            ScreenManager.Draw();
            Raylib.EndDrawing();
        }

        ScreenManager.UnloadTextures();

        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();            
    }
}
