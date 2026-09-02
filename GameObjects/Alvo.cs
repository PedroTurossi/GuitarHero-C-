using System.Numerics;
using Raylib_cs;

class Alvo : IGameObject {
    
    public static float toleranciaHitRuim = 23f;
    public static float toleranciaHitBom = 12f;
    public static float toleranciaHitOtimo = 6f;
    
    public bool excluirObjeto { get; set; } = false;
    public Color cor { get; set; }
    int idDeCor;
    public Vector2 posicaoObjeto { get; set; }
    public static Texture2D textura { get; set; }


    float raioAlvo = 14f;

    public Alvo (int id) {
        float posicaoX = GameScreen.offsetX + ((id==0) ? 0 : ( ((Program.larguraTela - (GameScreen.offsetX*2)) / (4)) * id ));
        float posicaoY = Program.alturaTela - GameScreen.offsetY;

        posicaoObjeto = new Vector2((int) posicaoX, (int) posicaoY);
        idDeCor = id;
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
        // Console.WriteLine("## INICIALIZANDO ALVO - " + id);
        // Image imagem = Raylib.LoadImage("Files/NotaBase.png");
        // Raylib.ImageColorReplace(ref imagem, Color.White, cor);
        // texturaNota = Raylib.LoadTextureFromImage(imagem);
        // Raylib.UnloadImage(imagem);
    }

   
    public void Load() {
        textura = Raylib.LoadTexture("Files\\AlvoBase.png");
    }

    public static void Unload() {
        Raylib.UnloadTexture(textura);
    }

    public void Update(float dt) {
        List<IGameObject> listaDeNotas = new List<IGameObject>();
        KeyboardKey teclaDoAlvo = KeyboardKey.Space;

        switch(idDeCor) {
            case 0:
                listaDeNotas = GameScreen.listaNotasVerdes;
                teclaDoAlvo = KeyboardKey.A;

                break;
            
            case 1:
                listaDeNotas = GameScreen.listaNotasVermelhas;
                teclaDoAlvo = KeyboardKey.S;
                break;

            case 2:
                listaDeNotas = GameScreen.listaNotasAmarelas;
                teclaDoAlvo = KeyboardKey.J;
                break;
            
            case 3:
                listaDeNotas = GameScreen.listaNotasAzuis;
                teclaDoAlvo = KeyboardKey.K;
                break;
            
            case 4:
                listaDeNotas = GameScreen.listaNotasLaranjas;
                teclaDoAlvo = KeyboardKey.L;
                break;
        }
        // Console.WriteLine(idDeCor);

        if(Raylib.IsKeyDown(teclaDoAlvo)) {
            foreach(IGameObject nota in listaDeNotas) {
                if (Vector2.Distance(posicaoObjeto, nota.posicaoObjeto) <= toleranciaHitRuim) {
                    if (Vector2.Distance(posicaoObjeto, nota.posicaoObjeto) <= toleranciaHitBom) {
                        if (Vector2.Distance(posicaoObjeto, nota.posicaoObjeto) <= toleranciaHitOtimo) {
                            Console.WriteLine("++Foda");
                        } else{
                            Console.WriteLine("+bom");
                        }
                    } else {
                        Console.WriteLine("-ruim");
                    }
                    nota.excluirObjeto = true;
                } else {
                }
            }
        }
    }

    public void Draw() {
        // Raylib.DrawTexture(texturaNota, (int)posicaoObjeto.X, (int)posicaoObjeto.Y, Color.White);
        // Raylib.UnloadTexture(texturaNota);
        // Raylib.DrawCircle((int)posicaoObjeto.X, (int)posicaoObjeto.Y, raioAlvo, cor);

        if (textura.Id == 0) {
            Load();
        }

        Vector2 vetorDaTextura = new Vector2((posicaoObjeto.X - (textura.Width/2)), (posicaoObjeto.Y - (textura.Height/2)));
        Raylib.DrawTextureV(textura, vetorDaTextura, cor);
    }

}