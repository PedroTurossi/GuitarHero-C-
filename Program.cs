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

        // Carrega os recursos compartilhados antes do loop. Assim, o primeiro
        // frame da fase não precisa fazer upload de texturas para a GPU.
        Alvo.CarregarTextura();
        Nota.CarregarTextura();
        ParticleManager.CarregarTexturas();

        ScreenManager.Inicializar(context);
        ScreenManager.ChangeScreen(() => new MainMenuScreen(context));

        while(!Raylib.WindowShouldClose() && !context.ExitRequested) {
            float deltaTime = Raylib.GetFrameTime();
            context.UpdateInput();
            ScreenManager.Update(deltaTime);

            Raylib.BeginDrawing();
            ScreenManager.Draw();
            Raylib.EndDrawing();
        }

        ScreenManager.UnloadTextures();

        // A tela atual pode ser o menu, que não possui recursos próprios para
        // descarregar os assets compartilhados.
        Alvo.Unload();
        Nota.Unload();
        ParticleManager.DescarregarTexturas();
        AudioManager.UnloadMusica();

        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();            
    }
}
