using System.Numerics;
using Raylib_cs;

class Alvo : IGameObject {
    public bool excluirObjeto { get; set; } = false;
    public Color cor { get; set; }
    public Vector2 posicaoObjeto { get; set; }
    Texture2D texturaNota;


    float raioAlvo = 8f;

    public Alvo (int id) {
        float posicaoX = GameScreen.offsetX + ((id==0) ? 0 : ( ((Program.larguraTela - (GameScreen.offsetX*2)) / (4)) * id ));
        float posicaoY = Program.alturaTela - GameScreen.offsetY;

        posicaoObjeto = new Vector2((int) posicaoX, (int) posicaoY);
        
        switch(id) {
            case 0:
                cor = Color.Green;
                break;

            case 1:
                cor = Color.Red;
                break;

            case 2:
                cor = Color.Yellow;
                break;

            case 3:
                cor = Color.Blue;
                break;

            case 4:
                cor = Color.Orange;
                break;
        }
        // Console.WriteLine("OIII");
        // Image imagem = Raylib.LoadImage("Files/NotaBase.png");
        // Raylib.ImageColorReplace(ref imagem, Color.White, cor);
        // texturaNota = Raylib.LoadTextureFromImage(imagem);
        // Raylib.UnloadImage(imagem);
    }

    public void Draw() {
        // Raylib.DrawTexture(texturaNota, (int)posicaoObjeto.X, (int)posicaoObjeto.Y, Color.White);
        // Raylib.UnloadTexture(texturaNota);
        Raylib.DrawCircle((int)posicaoObjeto.X, (int)posicaoObjeto.Y, raioAlvo, cor);

    }

    public void Update(float dt) {
    }
}