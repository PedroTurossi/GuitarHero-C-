using System.Numerics;
using Raylib_cs;

class TestScreen : IScreen {
    private readonly Menu menu;

    public TestScreen(GameContext context) {
        int buttonWidth = (int)(context.ScreenWidth * 0.08f);
        int buttonHeight = (int)(context.ScreenHeight * 0.04f);
        Vector2 buttonPosition = new(
            context.ScreenWidth / 2.5f - buttonWidth / 2f,
            context.ScreenHeight / 2f - 80 - buttonHeight
        );

        menu = new Menu(buttonPosition, buttonWidth, buttonHeight, 8);
        menu.AddButton("Jogar", () => Console.Write("jogar xd"));
    }

    public void Draw(GameContext context) {
        Raylib.ClearBackground(Color.SkyBlue);
        Raylib.DrawText("Jogasso XD", 310, 100, 40, Color.DarkGray);

        Color overlay = Color.Black;
        overlay.A = 80;
        Raylib.DrawRectangleV(
            Vector2.Zero,
            new Vector2(context.ScreenWidth, context.ScreenHeight),
            overlay
        );
        menu.Draw();
    }

    public void Unload() {
    }

    public void Update(float deltaTime, GameContext context) {
        menu.Update(context);
    }
}
