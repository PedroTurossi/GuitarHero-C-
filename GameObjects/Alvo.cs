using System.Numerics;
using Raylib_cs;

class Alvo : IGameObject {
    public bool excluirObjeto { get; set; } = false;
    public Color cor { get; set; }
    public Vector2 posicaoObjeto { get; set; }
    public static Texture2D textura { get; set; }

    public int Lane { get; }

    private bool alvoPressionado;

    public Alvo(int lane, Vector2 position, Color color) {
        Lane = lane;
        posicaoObjeto = position;
        cor = color;
    }

    public void Load() {
        CarregarTextura();
    }

    public static void CarregarTextura() {
        if (textura.Id != 0) {
            return;
        }

        textura = Raylib.LoadTexture("Files\\AlvoBase.png");
    }

    public static void Unload() {
        if (textura.Id != 0) {
            Raylib.UnloadTexture(textura);
            textura = default;
        }
    }

    public void Update(float dt) {
    }

    public void SetPressed(bool isPressed) {
        alvoPressionado = isPressed;
    }

    public void Draw() {
        if (textura.Id == 0) {
            Load();
        }

        if (alvoPressionado) {
            Raylib.DrawEllipseV(posicaoObjeto, (textura.Width - 5) / 2, (textura.Height - 5) / 2, cor);
        }

        Vector2 vetorDaTextura = new Vector2(
            posicaoObjeto.X - textura.Width / 2,
            posicaoObjeto.Y - textura.Height / 2
        );

        Raylib.DrawTextureV(textura, vetorDaTextura, cor);
    }
}
