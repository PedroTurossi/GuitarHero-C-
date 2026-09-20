using Raylib_cs;

class GameSettings {
    public const int LaneCount = 5;

    public int ScreenWidth { get; }
    public int ScreenHeight { get; }
    public string AssetsPath { get; }

    public int TargetOffsetX { get; } = 120;
    public int TargetOffsetY { get; } = 80;
    public float NoteSpeed { get; } = 500f;

    public float BadHitTolerance { get; } = 23f;
    public float GoodHitTolerance { get; } = 18f;
    public float GreatHitTolerance { get; } = 10f;

    public KeyboardKey[] LaneKeys { get; } = [
        KeyboardKey.A,
        KeyboardKey.S,
        KeyboardKey.J,
        KeyboardKey.K,
        KeyboardKey.L
    ];

    public GameSettings(int screenWidth, int screenHeight, string assetsPath) {
        ScreenWidth = screenWidth;
        ScreenHeight = screenHeight;
        AssetsPath = assetsPath;
    }
}
