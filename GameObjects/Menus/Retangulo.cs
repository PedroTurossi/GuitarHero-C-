using System.Numerics;
using Raylib_cs;

class Botao : IGameObject {
    public bool excluirObjeto { get; set; } = false;
    public Color cor { get; set; }
    public Vector2 posicaoObjeto { get; set; }
    public Texture2D textura { get; set; }

    public string textoDentroDoRetangulo = "";

    public int larguraRetangulo = 0;
    public int alturaRetangulo = 0;
    public Action? AoClicar;

    private Rectangle retangulo;

    public Botao (Vector2 posicao, int altura, int largura, Color corEscolhida, string texto, Action? funcao) {
        posicaoObjeto = posicao;
        alturaRetangulo = altura;
        larguraRetangulo = largura;
        cor = corEscolhida;
        textoDentroDoRetangulo = texto;
        retangulo = new Rectangle((int)posicaoObjeto.X, (int)posicaoObjeto.Y, larguraRetangulo, alturaRetangulo);
        AoClicar = funcao;
    }

   
    public void Load() {
    }

    public static void Unload() {
    }

    public void Update(float dt) {
        if (Raylib.CheckCollisionPointRec(Program.posicaoDoMouse, retangulo) && Raylib.IsMouseButtonPressed(MouseButton.Left)) {
            AoClicar.Invoke();
        }
    }

    public void Draw() {  
        bool mouseEmCima = Raylib.CheckCollisionPointRec(
            Program.posicaoDoMouse,
            retangulo
        );

        Raylib.DrawRectangleRec(
            retangulo,
            mouseEmCima ? Color.LightGray : cor
        );

        int tamanhoFonte = 20;

        int larguraTexto = Raylib.MeasureText(textoDentroDoRetangulo, tamanhoFonte);
        int xTexto = (int)(retangulo.X + (retangulo.Width - larguraTexto) / 2);
        int yTexto = (int)(retangulo.Y + (retangulo.Height - tamanhoFonte) / 2);

        Raylib.DrawText(
            textoDentroDoRetangulo,
            xTexto,
            yTexto,
            tamanhoFonte,
            Color.White
        );
    }

}