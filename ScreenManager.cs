class ScreenManager {
    private IScreen telaAtual;

    public ScreenManager() {
        // telaAtual --> Tela inicial, quando houver um Menu
    }

    public void ChangeScreen(IScreen novaTela) {
        telaAtual = novaTela;
    }

    public void Update(float deltaTime) {
        telaAtual.Update(deltaTime);
    }

    public void Draw() {
        telaAtual.Draw();
    }

    public void UnloadTextures() {
        telaAtual.Unload();
    }
}