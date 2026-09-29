class ScreenManager {
    private static IScreen? telaAtual;
    private static GameContext? context;

    public static void Inicializar(GameContext gameContext) {
        context = gameContext;
    }

    // A fábrica é importante: a tela anterior precisa ser descarregada antes
    // de a nova tela carregar recursos estáticos compartilhados.
    public static void ChangeScreen(Func<IScreen> criarTela) {
        telaAtual?.Unload();
        telaAtual = criarTela();
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
