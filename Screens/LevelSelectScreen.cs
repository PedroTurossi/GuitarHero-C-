using System.Numerics;
using Raylib_cs;

class LevelSelectScreen : IScreen {

    List<IGameObject> listaDeObjetos = new List<IGameObject>();


    public LevelSelectScreen() {
        // agora só falta obter uma lista com todos os arquivos de músicas
        // e distribuir esses objetos automaticamente dentro de um espaço da
        // tela de maneira responsiva fazendo com que entremos nas telas de jogos
        // de acordo com os diferentes arquivos


        string[] arquivos = LeitorDeArquivos.LerDiretorio(Program.localDosArquivos);

        float larguraBotao = Program.larguraTela * 0.4f;
        float alturaBotao = Program.alturaTela * 0.08f;

        float posicaoX = (Program.larguraTela - larguraBotao) / 2f;
        float espacamento = Program.alturaTela * 0.02f;
        
        for (int i = 0; i < arquivos.Length; i++) {
            float posicaoY = (Program.alturaTela * 0.05f) + (i * (alturaBotao + espacamento));
            
            string arquivoAtual = arquivos[i];
            string textoDoBotao = Path.GetFileNameWithoutExtension(arquivoAtual);

            listaDeObjetos.Add(
                new Botao(
                    new Vector2(posicaoX, posicaoY),
                    (int)alturaBotao,
                    (int)larguraBotao,
                    Color.Gray,
                    textoDoBotao,
                    () => ScreenManager.ChangeScreen(new GameScreen(arquivoAtual))
                )
            );
        }

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