using System.Numerics;
using Raylib_cs;

class SettingsScreen : IScreen {
    private readonly Menu menu;
    private readonly Rectangle volumeSlider;
    private bool arrastandoVolume;

    public SettingsScreen(GameContext context) {
        int buttonWidth = (int)(context.ScreenWidth * 0.4f);
        int buttonHeight = (int)(context.ScreenHeight * 0.08f);
        int positionX = (context.ScreenWidth - buttonWidth) / 2;
        int positionY = (int)(context.ScreenHeight * 0.62f);

        volumeSlider = new Rectangle(
            context.ScreenWidth * 0.3f,
            context.ScreenHeight * 0.48f,
            context.ScreenWidth * 0.4f,
            12
        );

        menu = new Menu(
            new Vector2(positionX, positionY),
            buttonWidth,
            buttonHeight,
            (int)(context.ScreenHeight * 0.02f)
        );

        menu.AddButton(
            "Voltar",
            () => ScreenManager.ChangeScreen(() => new MainMenuScreen(context))
        );
        menu.BackRequested = () => ScreenManager.ChangeScreen(() => new MainMenuScreen(context));
    }

    public void Update(float deltaTime, GameContext context) {
        menu.Update(context);

        Rectangle sliderHitbox = new(
            volumeSlider.X - 12,
            volumeSlider.Y - 18,
            volumeSlider.Width + 24,
            volumeSlider.Height + 36
        );

        if (context.Input.MouseLeftPressed
            && Raylib.CheckCollisionPointRec(context.Input.MousePosition, sliderHitbox)) {
            arrastandoVolume = true;
        }

        if (!Raylib.IsMouseButtonDown(MouseButton.Left)) {
            arrastandoVolume = false;
        }

        if (arrastandoVolume) {
            float volume = (context.Input.MousePosition.X - volumeSlider.X) / volumeSlider.Width;
            AudioManager.DefinirVolume(volume);
        }
    }

    public void Draw(GameContext context) {
        Raylib.ClearBackground(Color.DarkGray);

        const string title = "CONFIGURACOES";
        const int titleFontSize = 32;
        int titleWidth = Raylib.MeasureText(title, titleFontSize);
        int titleX = (context.ScreenWidth - titleWidth) / 2;

        Raylib.DrawText(title, titleX, 100, titleFontSize, Color.White);
        Raylib.DrawText(
            $"Volume: {(int)(AudioManager.Volume * 100)}%",
            (int)volumeSlider.X,
            (int)volumeSlider.Y - 38,
            18,
            Color.White
        );

        Raylib.DrawRectangleRec(volumeSlider, Color.Gray);

        Rectangle preenchimento = new(
            volumeSlider.X,
            volumeSlider.Y,
            volumeSlider.Width * AudioManager.Volume,
            volumeSlider.Height
        );
        Raylib.DrawRectangleRec(preenchimento, Color.RayWhite);

        float ponteiroX = volumeSlider.X + volumeSlider.Width * AudioManager.Volume;
        Raylib.DrawCircle(
            (int)ponteiroX,
            (int)(volumeSlider.Y + volumeSlider.Height / 2),
            10,
            Color.White
        );

        menu.Draw();
    }

    public void Unload() {
    }
}
