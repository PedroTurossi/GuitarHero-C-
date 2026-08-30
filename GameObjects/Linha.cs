using System.Numerics;
using Raylib_cs;

class Linha : IGameObject {
    public bool excluirObjeto { get; set; }
    public Color cor { get; set; } = Color.DarkPurple;
    public Vector2 posicaoObjeto { get; set; }

    public Vector2[] pontosDaLinha = new Vector2[2];
    
    public void SetPosicao (Vector2 posicaoRecebida, Color corRecebida) {
        pontosDaLinha[0] = new Vector2(posicaoRecebida.X-5f, posicaoRecebida.Y);
        pontosDaLinha[1] = new Vector2(posicaoRecebida.X+5f, posicaoRecebida.Y);
        cor = corRecebida;
    }
    
    public void Draw() {
        Raylib.DrawLine((int)pontosDaLinha[0].X, (int)pontosDaLinha[0].Y, (int)pontosDaLinha[1].X, (int)pontosDaLinha[1].Y, cor);
    }

    public void Update(float dt) {
    }

}