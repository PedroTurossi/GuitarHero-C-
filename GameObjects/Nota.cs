using Raylib_cs;
using System.Numerics;

class Nota : IGameObject {
    public bool excluirObjeto { get; set; } = false;
    public Color cor { get; set; }
    public static Texture2D textura { get; set; }
    public Vector2 posicaoObjeto { get; set; } = Vector2.Zero;

    public int Lane { get; }
    public bool Missed { get; private set; }
    public bool MissRegistered { get; set; }

    private readonly float velocidadeDeMovimento;
    private readonly int screenHeight;
    private readonly int targetOffsetY;

    public Nota(int lane, GameSettings settings, GameplaySession session) {
        Lane = lane;
        velocidadeDeMovimento = settings.NoteSpeed;
        screenHeight = settings.ScreenHeight;
        targetOffsetY = settings.TargetOffsetY;

        Vector2 targetPosition = session.GetTargetPosition(lane, settings);
        posicaoObjeto = new Vector2(targetPosition.X, -settings.TargetOffsetY);
        cor = LaneColor.GetColor(lane);
    }

    public void Load() {
        textura = Raylib.LoadTexture("Files\\NotaBase.png");
    }

    public static void Unload() {
        if (textura.Id != 0) {
            Raylib.UnloadTexture(textura);
            textura = default;
        }
    }

    public void Update(float dt) {
        Vector2 direcao = new Vector2(0, 1);
        Vector2 movimento = direcao * velocidadeDeMovimento * dt;
        posicaoObjeto += movimento;

        if (posicaoObjeto.Y > screenHeight + targetOffsetY) {
            Missed = true;
            excluirObjeto = true;
        }
    }

    public void Draw() {
        if (textura.Id == 0) {
            Load();
        }

        Vector2 vetorDaTextura = new Vector2(
            posicaoObjeto.X - textura.Width / 2,
            posicaoObjeto.Y - textura.Height / 2
        );

        Raylib.DrawTextureV(textura, vetorDaTextura, Color.White);
    }
}
