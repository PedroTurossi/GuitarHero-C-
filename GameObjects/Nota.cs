using Raylib_cs;
using System.Numerics;

class Nota : IGameObject {
    public bool excluirObjeto { get; set; } = false;
    public Color cor { get; set; }
    public Vector2 posicaoObjeto { get; set; } = Vector2.Zero;

    static float velocidadeDeMovimento = 200f;
    static float toleranciaDeColisao = velocidadeDeMovimento/120;
    Vector2 posicaoDoAlvo = Vector2.Zero;

    public static int numeroDoAlvo;
    

    // teste para teleguiado
    

    public Nota (int numeroDoAlvo = -1) {
        //  vo larga aleatorio pra teste mesmo xd
        if (numeroDoAlvo == -1) {
            Random rand = new Random();
            numeroDoAlvo = rand.Next(0,5);
        }
        

        float posicaoXDoAlvo = GameScreen.offsetX + ((numeroDoAlvo==0) ? 0 : ( ((Program.larguraTela - (GameScreen.offsetX*2)) / (4)) * numeroDoAlvo ));
        float posicaoYDoAlvo = Program.alturaTela - GameScreen.offsetY;

        posicaoDoAlvo = new Vector2((int)posicaoXDoAlvo, (int)posicaoYDoAlvo);



        posicaoObjeto = new Vector2(posicaoXDoAlvo, -GameScreen.offsetY);


    }

    // ajustar posicao do alvo
    public void Update(float dt) {
        if (Vector2.Distance(posicaoObjeto, posicaoDoAlvo) > toleranciaDeColisao) {
            Vector2 direcao = Vector2.Normalize(posicaoDoAlvo - posicaoObjeto);
            Vector2 movimento = direcao * velocidadeDeMovimento * dt;
            posicaoObjeto += movimento;
        } else {
            excluirObjeto = true;
        }
    }

    public void Draw() {
        // fazer alguma animação bacana depois
        Raylib.DrawCircle((int)posicaoObjeto.X, (int)posicaoObjeto.Y, 4f, Color.RayWhite);
    }
}

