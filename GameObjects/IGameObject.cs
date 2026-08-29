using System.Numerics;
using Raylib_cs;

interface IGameObject {

    bool excluirObjeto { get; set; }
    Color cor { get; set; }
    Vector2 posicaoObjeto { get; set; }
    void Update(float dt);
    void Draw();
}