using System.Numerics;
using Raylib_cs;

class MainMenuScreen : IScreen {
    private readonly Menu menu;

    public MainMenuScreen(GameContext context) {
        int buttonWidth = (int)(context.ScreenWidth * 0.4f);
        int buttonHeight = (int)(context.ScreenHeight * 0.08f);
        int positionX = (context.ScreenWidth - buttonWidth) / 2;
        int positionY = (int)(context.ScreenHeight * 0.36f);
        int spacing = (int)(context.ScreenHeight * 0.02f);

        menu = new Menu(
            new Vector2(positionX, positionY),
            buttonWidth,
            buttonHeight,
            spacing
        );

        menu.AddButton(
            "Jogar",
            () => ScreenManager.ChangeScreen(() => new LevelSelectScreen(context))
        );

        menu.AddButton(
            "Configurações",
            () => ScreenManager.ChangeScreen(() => new SettingsScreen(context))
        );

        menu.AddSpacing(15);
        menu.AddButton("Sair", context.RequestExit);
    }

    public void Update(float deltaTime, GameContext context) {
        menu.Update(context);
    }

    public void Draw(GameContext context) {
        Raylib.ClearBackground(Color.DarkGray);

        const int titleFontSize = 42;
        const string title = "GUITAR HERO - C# clone";
        int titleWidth = Raylib.MeasureText(title, titleFontSize);
        int titleX = (context.ScreenWidth - titleWidth) / 2;

        Raylib.DrawText(title, titleX, 70, titleFontSize, Color.White);
        menu.Draw();
    }

    public void Unload() {
    }
}
