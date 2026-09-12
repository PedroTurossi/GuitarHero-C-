using System.Numerics;
using Raylib_cs;

class TestScreen : IScreen {

    // static Texture2D textura;

    Rectangle playButton = new Rectangle(300, 220, 200, 50);
    Rectangle exitButton = new Rectangle(300, 300, 200, 50);

    public TestScreen() {        
        // textura = Raylib.LoadTexture("Files\\NotaBase.png");
        // Console.WriteLine("#### carregando textura - " + textura.Id);
    }

    public void Draw() {
        Raylib.ClearBackground(Color.Black);
        Raylib.DrawText("Jogasso XD", 310, 100, 40, Color.DarkGray);

        bool hoverPlay = Raylib.CheckCollisionPointRec(Program.posicaoDoMouse, playButton);
        Raylib.DrawRectangleRec(playButton, hoverPlay ? Color.LightGray : Color.Gray);
        Raylib.DrawText("JOGAR", (int)playButton.X + 60, (int)playButton.Y + 15, 20, Color.White);

        WordsManager.DesenharPalavras();

    }

    public void Unload() {
    }

    public void Update(float deltaTime) {
        if (Raylib.CheckCollisionPointRec(Program.posicaoDoMouse, playButton) && Raylib.IsMouseButtonPressed(MouseButton.Left)) {
            WordsManager.AdicionarPalavra(Program.posicaoDoMouse, 20, "xd");
        }
        WordsManager.UpdatePalavras(deltaTime);
    }
}