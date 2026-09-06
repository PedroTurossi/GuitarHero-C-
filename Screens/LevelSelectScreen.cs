using System.Numerics;
using Raylib_cs;

class LevelSelectScreen : IScreen {

    List<IGameObject> listaDeObjetos = new List<IGameObject>();


    public LevelSelectScreen() {
        // agora só falta obter uma lista com todos os arquivos de músicas
        // e distribuir esses objetos automaticamente dentro de um espaço da
        // tela de maneira responsiva fazendo com que entremos nas telas de jogos
        // de acordo com os diferentes arquivos


        listaDeObjetos.Add(new Retangulo(new Vector2(20, 20), 50, 200, Color.Gray, "teste1"));
        listaDeObjetos.Add(new Retangulo(new Vector2(250, 20), 50, 180, Color.White, "teste2"));

    }
    
    public void Update(float deltaTime) {
        foreach(IGameObject objeto in listaDeObjetos) {
            objeto.Update(deltaTime);
        }
    }


    public void Draw() {
        foreach(IGameObject objeto in listaDeObjetos) {
            objeto.Draw();
        }
    }


    public void Unload() {
        // throw new NotImplementedException();
    }

}