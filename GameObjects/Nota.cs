using Raylib_cs;
using System.Numerics;

class Nota : IGameObject {
    public bool excluirObjeto { get; set; } = false;
    public Color cor { get; set; }
    public static Texture2D textura { get; set; }
    public Vector2 posicaoObjeto { get; set; } = Vector2.Zero;

    public static float velocidadeDeMovimento = 400f;
    // static float toleranciaDeColisao = velocidadeDeMovimento/120;
    // static float toleranciaHitRuim = velocidadeDeMovimento/22;
    // static float toleranciaHitBom = velocidadeDeMovimento/45;
    // static float toleranciaHitOtimo = velocidadeDeMovimento/105;

    Vector2 posicaoDoAlvo = Vector2.Zero;

    public int numeroDoAlvo;

    public float timer;
    

    // teste para teleguiado
    

    public Nota (int numeroDoAlvo = -1) {
        timer = 0f;
        //  vo larga aleatorio pra teste mesmo xd
        if (numeroDoAlvo == -1) {
            Random rand = new Random();
            numeroDoAlvo = rand.Next(0,5);
        }
        


        float posicaoXDoAlvo = GameScreen.offsetX + ((numeroDoAlvo==0) ? 0 : ( ((Program.larguraTela - (GameScreen.offsetX*2)) / (4)) * numeroDoAlvo ));
        float posicaoYDoAlvo = Program.alturaTela - GameScreen.offsetY;

        posicaoDoAlvo = new Vector2((int)posicaoXDoAlvo, (int)posicaoYDoAlvo);

        posicaoObjeto = new Vector2(posicaoXDoAlvo, -GameScreen.offsetY);


        switch(numeroDoAlvo) {
            case 0:
                GameScreen.listaNotasVerdes.Add(this);
                break;
            
            case 1:
                GameScreen.listaNotasVermelhas.Add(this);
                break;

            case 2:
                GameScreen.listaNotasAmarelas.Add(this);
                break;
            
            case 3:
                GameScreen.listaNotasAzuis.Add(this);
                break;
            
            case 4:
                GameScreen.listaNotasLaranjas.Add(this);
                break;
        }

        // DEBUG VISUAL - COLISÃO 
        // Vector2 posicaoOtima = new Vector2(posicaoXDoAlvo, posicaoYDoAlvo - Alvo.toleranciaHitOtimo);
        // Linha linhaOtima = new Linha();
        // linhaOtima.SetPosicao(posicaoOtima, Color.White);
        // GameScreen.objetosDoJogo.Add(linhaOtima);
        
        // Vector2 posicaoOtima2 = new Vector2(posicaoXDoAlvo, posicaoYDoAlvo + Alvo.toleranciaHitOtimo);
        // Linha linhaOtima2 = new Linha();
        // linhaOtima2.SetPosicao(posicaoOtima2, Color.White);
        // GameScreen.objetosDoJogo.Add(linhaOtima2);

        

        // Vector2 posicaoBoa = new Vector2(posicaoXDoAlvo, posicaoYDoAlvo - Alvo.toleranciaHitBom);
        // Linha linhaBoa = new Linha();
        // linhaBoa.SetPosicao(posicaoBoa, Color.DarkGreen);
        // GameScreen.objetosDoJogo.Add(linhaBoa);
        
        // Vector2 posicaoBoa2 = new Vector2(posicaoXDoAlvo, posicaoYDoAlvo + Alvo.toleranciaHitBom);
        // Linha linhaBoa2 = new Linha();
        // linhaBoa2.SetPosicao(posicaoBoa2, Color.DarkGreen);
        // GameScreen.objetosDoJogo.Add(linhaBoa2);
        

        // Vector2 posicaoRuim = new Vector2(posicaoXDoAlvo, posicaoYDoAlvo - Alvo.toleranciaHitRuim);
        // Linha linhaRuim = new Linha();
        // linhaRuim.SetPosicao(posicaoRuim, Color.White);
        // GameScreen.objetosDoJogo.Add(linhaRuim);

        // Vector2 posicaoRuim2 = new Vector2(posicaoXDoAlvo, posicaoYDoAlvo + Alvo.toleranciaHitRuim);
        // Linha linhaRuim2 = new Linha();
        // linhaRuim2.SetPosicao(posicaoRuim2, Color.White);
        // GameScreen.objetosDoJogo.Add(linhaRuim2);

    }

    public void Load() {
        textura = Raylib.LoadTexture("Files\\NotaBase.png");
    }

    public static void Unload() {
        Raylib.UnloadTexture(textura);
    }

    public void Update(float dt) {
        Vector2 direcao = new Vector2(0,1);
        Vector2 movimento = direcao * velocidadeDeMovimento * dt;
        posicaoObjeto += movimento;
        timer += dt;
        

        if (posicaoObjeto.Y > Program.alturaTela + 20f) {
            excluirObjeto = true;
            Console.WriteLine("--Errou");
        }
    }

    public void Draw() {
        // Console.WriteLine(textura.Id);
        if (textura.Id == 0) {
            Load();
        }

        Vector2 vetorDaTextura = new Vector2((posicaoObjeto.X - (textura.Width/2)), (posicaoObjeto.Y - (textura.Height/2)));
        Raylib.DrawTextureV(textura, vetorDaTextura, Color.White);

        // fazer alguma animação bacana depois
        // Raylib.DrawCircle((int)posicaoObjeto.X, (int)posicaoObjeto.Y, 10f, Color.RayWhite);
        // Raylib.DrawCircle((int)posicaoObjeto.X, (int)posicaoObjeto.Y, 1f, Color.Red);
    }
}

