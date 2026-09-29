using Raylib_cs;
using System.Numerics;

class InputState {
    public Vector2 MousePosition { get; private set; }
    public bool MouseLeftPressed { get; private set; }
    public bool PausePressed { get; private set; }
    public bool DebugSpawnPressed { get; private set; }
    public bool MenuUpPressed { get; private set; }
    public bool MenuDownPressed { get; private set; }
    public bool MenuConfirmPressed { get; private set; }

    public bool[] LaneDown { get; } = new bool[GameSettings.LaneCount];

    public void Update(GameSettings settings) {
        MousePosition = Raylib.GetMousePosition();
        MouseLeftPressed = Raylib.IsMouseButtonPressed(MouseButton.Left);
        PausePressed = Raylib.IsKeyPressed(KeyboardKey.Escape);
        DebugSpawnPressed = Raylib.IsKeyPressed(KeyboardKey.Space);
        MenuUpPressed = Raylib.IsKeyPressed(KeyboardKey.Up) || Raylib.IsKeyPressed(KeyboardKey.W);
        MenuDownPressed = Raylib.IsKeyPressed(KeyboardKey.Down) || Raylib.IsKeyPressed(KeyboardKey.S);
        MenuConfirmPressed = Raylib.IsKeyPressed(KeyboardKey.Enter) || Raylib.IsKeyPressed(KeyboardKey.Space);

        for (int i = 0; i < GameSettings.LaneCount; i++) {
            LaneDown[i] = Raylib.IsKeyDown(settings.LaneKeys[i]);
        }
    }
}
