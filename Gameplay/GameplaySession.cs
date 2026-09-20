using System.Numerics;

class GameplaySession {
    public Song Song { get; }
    public GameState State { get; set; } = GameState.Jogando;
    public float SongTimerMs { get; private set; }
    public int Score { get; private set; }

    public List<IGameObject> Objects { get; } = new();
    public List<Alvo> Targets { get; } = new();
    public List<Nota>[] NotesByLane { get; } = [
        new(), new(), new(), new(), new()
    ];

    public GameplaySession(Song song) {
        Song = song;
    }

    public void AdvanceTime(float deltaTime) {
        SongTimerMs += deltaTime * 1000f;
    }

    public void AddScore(int score) {
        Score += score;
    }

    public void AddNote(Nota note) {
        NotesByLane[note.Lane].Add(note);
    }

    public Vector2 GetTargetPosition(int lane, GameSettings settings) {
        float availableWidth = settings.ScreenWidth - (settings.TargetOffsetX * 2);
        float laneSpacing = availableWidth / (GameSettings.LaneCount - 1);
        float x = settings.TargetOffsetX + (laneSpacing * lane);
        float y = settings.ScreenHeight - settings.TargetOffsetY;

        return new Vector2((int)x, (int)y);
    }

    public float GetSpawnLeadTimeMs(GameSettings settings) {
        return settings.ScreenHeight / settings.NoteSpeed * 1000f;
    }

    public void ResetTargetsInputState() {
        foreach (Alvo target in Targets) {
            target.SetPressed(false);
        }
    }
}
