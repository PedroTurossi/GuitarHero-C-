using System.Numerics;
using Raylib_cs;

class TestScreen : IScreen {

    // List<IGameObject> listaDeObjetos = new List<IGameObject>();

    Botao botaoDePlay;
    Rectangle exitButton;
    bool teste = false;

    public TestScreen() {   
        int larguraDoBotao = (int)(Program.larguraTela * 0.08f);
        int alturaDoBotao = (int)(Program.alturaTela * 0.04f);
        Vector2 posicaoBotaoDePlay = new Vector2(Program.larguraTela/2.5f - larguraDoBotao/2, Program.alturaTela/2  - 80 - alturaDoBotao);     
        botaoDePlay = new Botao(
                posicaoBotaoDePlay,
                alturaDoBotao,
                larguraDoBotao,
                Color.Red,
                "Jogar", 
                () => Console.Write("jogar xd"));

        Console.WriteLine("#### carregando botao");
    }

    public void Draw(GameContext context) {
        Raylib.ClearBackground(Color.SkyBlue);
        Raylib.DrawText("Jogasso XD", 310, 100, 40, Color.DarkGray);


        // camada 1 - cinza em toda a tela
        Color overlay = Color.Black;
        overlay.A = 80;
        Raylib.DrawRectangleV(Vector2.Zero, new Vector2(context.ScreenWidth, context.ScreenHeight), overlay);

        // camada 2 - cinza no canto esquerdo
        Vector2 vetorDoRetanguloEsquerdo = new Vector2(context.ScreenWidth/2.5f, context.ScreenHeight);
        Raylib.DrawRectangleV(Vector2.Zero, vetorDoRetanguloEsquerdo, overlay);
        Raylib.DrawText("PAUSADO", context.ScreenWidth / 5 - 58, context.ScreenHeight / 2 - 12, 24, Color.White);

        botaoDePlay.Draw();

    }

    public void Unload() {
    }

    public void Update(float deltaTime, GameContext context) {
        
        // if (Raylib.CheckCollisionPointRec(context.Input.MousePosition, playButton) && context.Input.MouseLeftPressed) {
        //     WordsManager.AdicionarPalavra(context.Input.MousePosition, 20, "xd");
        // }
        // WordsManager.UpdatePalavras(deltaTime);
    }
}
