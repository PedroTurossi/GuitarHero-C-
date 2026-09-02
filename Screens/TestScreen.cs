using Raylib_cs;

class TestScreen : IScreen {

    static Texture2D textura;
    public TestScreen() {
        
        textura = Raylib.LoadTexture("Files\\NotaBase.png");
        Console.WriteLine("#### carregando boceta - " + textura.Id);
    }

    public void Draw() {
        Raylib.ClearBackground(Color.Gray);
        Raylib.DrawTexture(textura, 100, 50, Color.Red);
    }

    public void Unload() {
        throw new NotImplementedException();
    }

    public void Update(float deltaTime) {
        
    }
}