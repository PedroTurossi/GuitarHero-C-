class GameContext {
    public GameSettings Settings { get; }
    public InputState Input { get; } = new();

    public int ScreenWidth => Settings.ScreenWidth;
    public int ScreenHeight => Settings.ScreenHeight;
    public string AssetsPath => Settings.AssetsPath;

    public GameContext(GameSettings settings) {
        Settings = settings;
    }

    public void UpdateInput() {
        Input.Update(Settings);
    }
}
