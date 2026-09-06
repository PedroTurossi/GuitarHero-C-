using System.Numerics;
using Raylib_cs;

class Retangulo : IGameObject {
    public bool excluirObjeto { get; set; } = false;
    public Color cor { get; set; }
    public Vector2 posicaoObjeto { get; set; }
    public Texture2D textura { get; set; }

    public string textoDentroDoRetangulo = "";

    public int larguraRetangulo = 0;
    public int alturaRetangulo = 0;

    private Rectangle retangulo;

    public Retangulo (Vector2 posicao, int altura, int largura, Color corEscolhida, string texto) {
        posicaoObjeto = posicao;
        alturaRetangulo = altura;
        larguraRetangulo = largura;
        cor = corEscolhida;
        textoDentroDoRetangulo = texto;
        retangulo = new Rectangle((int)posicaoObjeto.X, (int)posicaoObjeto.Y, larguraRetangulo, alturaRetangulo);
    }

   
    public void Load() {
    }

    public static void Unload() {
    }

    public void Update(float dt) {
        if (Raylib.CheckCollisionPointRec(Program.posicaoDoMouse, retangulo) && Raylib.IsMouseButtonPressed(MouseButton.Left)) {
            // vai ser enviado pra tal lugar dependendo doq for criado nesse retangulo bla bla bla
        }
    }

    public void Draw() {
        bool mouseEmCima = Raylib.CheckCollisionPointRec(Program.posicaoDoMouse, retangulo);        
        Raylib.DrawRectangleRec(retangulo, mouseEmCima ? Color.LightGray : cor);
        Raylib.DrawText(textoDentroDoRetangulo, (int)retangulo.X + (larguraRetangulo/2), (int)retangulo.Y + (alturaRetangulo/2), 20, Color.White);
    }

}