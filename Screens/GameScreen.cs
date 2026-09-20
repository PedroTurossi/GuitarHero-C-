using System.Numerics;
using Raylib_cs;

class GameScreen : IScreen {
    private readonly GameplaySession session;
    private readonly NoteSpawner noteSpawner;
    private readonly HitJudge hitJudge = new();

    public GameScreen(string levelASerJogado) {
        ParticleManager.CarregarTexturas();

        Song song = new SongLoader().Load(levelASerJogado);
        session = new GameplaySession(song);
        noteSpawner = new NoteSpawner(song);

        CreateTargets(new GameSettings(Program.larguraTela, Program.alturaTela, Program.localDosArquivos));

        string musicPath = Path.Combine(Program.localDosArquivos, song.AudioFile);
        AudioManager.DefinirMusica(musicPath);
    }

    public void Update(float deltaTime, GameContext context) {
        switch (session.State) {
            case GameState.Jogando:
                UpdatePlaying(deltaTime, context);
                break;

            case GameState.Pausado:
                if (context.Input.PausePressed) {
                    session.State = GameState.Jogando;
                    AudioManager.DespausarMusica();
                }
                break;
        }
    }

    private void UpdatePlaying(float deltaTime, GameContext context) {
        if (context.Input.DebugSpawnPressed) {
            int randomLane = Random.Shared.Next(GameSettings.LaneCount);
            session.AddNote(new Nota(randomLane, context.Settings, session));
        }

        session.AdvanceTime(deltaTime);
        AudioManager.UpdateMusica();

        if (context.Input.PausePressed) {
            session.State = GameState.Pausado;
            AudioManager.PausarMusica();
            return;
        }

        session.ResetTargetsInputState();
        noteSpawner.Update(session, context);
        hitJudge.Update(session, context);

        // foreach (IGameObject objeto in session.Objects) {
        //     objeto.Update(deltaTime);
        // }

        foreach (Alvo target in session.Targets) {
            target.Update(deltaTime);
        }

        UpdateNotes(deltaTime, context);
        ParticleManager.UpdateParticles(deltaTime);
        WordsManager.UpdatePalavras(deltaTime);
    }

    private void UpdatePauseMenu(float deltaTime, GameContext context) {
        
    }

    private void UpdateNotes(float deltaTime, GameContext context) {
        foreach (List<Nota> notes in session.NotesByLane) {
            foreach (Nota note in notes) {
                note.Update(deltaTime);

                if (note.Missed && !note.MissRegistered) {
                    hitJudge.RegisterMiss(note, session, context.Settings);
                    note.MissRegistered = true;
                }
            }

            notes.RemoveAll(note => note.excluirObjeto);
        }
    }

    public void Draw(GameContext context) {
        Raylib.ClearBackground(Color.DarkGray);

        foreach (IGameObject objeto in session.Objects) {
            objeto.Draw();
        }
        session.Objects.RemoveAll(objeto => objeto.excluirObjeto);

        foreach (Alvo target in session.Targets) {
            target.Draw();
        }

        ParticleManager.DrawParticles();
        WordsManager.DesenharPalavras();

        foreach (List<Nota> notes in session.NotesByLane) {
            foreach (Nota note in notes) {
                note.Draw();
            }
        }

        DrawScore();

        if (session.State == GameState.Pausado) {
            DrawPauseMenu(context);
        }
    }

    private void CreateTargets(GameSettings settings) {
        for (int lane = 0; lane < GameSettings.LaneCount; lane++) {
            Vector2 position = session.GetTargetPosition(lane, settings);
            session.Targets.Add(new Alvo(lane, position, LaneColor.GetColor(lane)));
        }
    }

    private void DrawScore() {
        Raylib.DrawText($"Score: {session.Score}", 16, 16, 20, Color.White);
    }

    private void DrawPauseMenu(GameContext context) {
        Color overlay = Color.Black;
        overlay.A = 80;
        Raylib.DrawRectangleV(Vector2.Zero, new Vector2(context.ScreenWidth, context.ScreenHeight), overlay);
        Raylib.DrawRectangleV(Vector2.Zero, new Vector2(context.ScreenWidth/2.5f, context.ScreenHeight), overlay);
        Raylib.DrawText("PAUSADO", context.ScreenWidth / 5 - 58, context.ScreenHeight / 2 - 12, 24, Color.White);
    }

    public void Unload() {
        Alvo.Unload();
        Nota.Unload();
        ParticleManager.DescarregarTexturas();
        AudioManager.UnloadMusica();
    }
}
