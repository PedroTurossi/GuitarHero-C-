using System.Numerics;
using Raylib_cs;

class LevelSelectScreen : IScreen {
    private readonly Menu menu;

    public LevelSelectScreen(GameContext context) {
        string[] arquivos = LeitorDeArquivos.LerDiretorio(context.AssetsPath);

        float buttonWidth = context.ScreenWidth * 0.4f;
        float buttonHeight = context.ScreenHeight * 0.08f;
        float positionX = (context.ScreenWidth - buttonWidth) / 2f;
        float positionY = context.ScreenHeight * 0.05f;
        float spacing = context.ScreenHeight * 0.02f;

        menu = new Menu(
            new Vector2(positionX, positionY),
            (int)buttonWidth,
            (int)buttonHeight,
            (int)spacing
        );

        menu.AddSpacing(20);

        foreach (string arquivoAtual in arquivos) {
            string textoDoBotao = Path.GetFileNameWithoutExtension(arquivoAtual);
            menu.AddButton(
                textoDoBotao,
                () => ScreenManager.ChangeScreen(() => new GameScreen(arquivoAtual, context))
            );
        }
        menu.AddSpacing(15);
        menu.AddButton("Voltar", () => ScreenManager.ChangeScreen(() => new MainMenuScreen(context)));


        menu.BackRequested = () => ScreenManager.ChangeScreen(() => new MainMenuScreen(context));
    }

    public void Update(float deltaTime, GameContext context) {
        menu.Update(context);
    }

    public void Draw(GameContext context) {
        Raylib.ClearBackground(Color.DarkGray);
        menu.Draw();
    }

    public void Unload() {
    }
}
