class ScreenManager {
    private static IScreen? telaAtual;
    private static GameContext? context;

    public static void Inicializar(GameContext gameContext) {
        context = gameContext;
    }

    public static void ChangeScreen(IScreen novaTela) {
        telaAtual?.Unload();
        telaAtual = novaTela;
    }

    public static void Update(float deltaTime) {
        if (telaAtual == null || context == null) {
            return;
        }

        telaAtual.Update(deltaTime, context);
    }

    public static void Draw() {
        if (telaAtual == null || context == null) {
            return;
        }

        telaAtual.Draw(context);
    }

    public static void UnloadTextures() {
        telaAtual?.Unload();
    }
}
