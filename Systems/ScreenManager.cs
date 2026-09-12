class ScreenManager {
    private static IScreen telaAtual;

    public ScreenManager() {
        // telaAtual --> Tela inicial, quando houver um Menu
    }

    public static void ChangeScreen(IScreen novaTela) {
        telaAtual = novaTela;
    }

    public static void Update(float deltaTime) {
        telaAtual.Update(deltaTime);
    }

    public static void Draw() {
        telaAtual.Draw();
    }

    public static void UnloadTextures() {
        telaAtual.Unload();
    }
}