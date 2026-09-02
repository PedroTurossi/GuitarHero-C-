using System.Numerics;
using Raylib_cs;

interface IGameObject {

    bool excluirObjeto { get; set; }
    Color cor { get; set; }
    Vector2 posicaoObjeto { get; set; }
    void Load();
    // void Unload();  <------- Eu descartei isso pra que sejam estáticos de cada objetos e todas as texturas deles possam ser descarregadas a partir da classe.
    // isso pode gerar uma confusão no código futuramente, mas deixa mais leve o processamento se as texturas de cada classe forem descarregadas
    void Update(float dt);
    void Draw();
}