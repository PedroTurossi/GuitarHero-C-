class ScreenManager {
    private IScreen telaAtual;

    public ScreenManager(IScreen telaInicial) {
        telaAtual = telaInicial;
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
}