using System.Numerics;
using Raylib_cs;

class GameScreen : IScreen {
    public static List<IGameObject> objetosDoJogo = new();

    List<List<IGameObject>> listaDeNotasDoJogo = new();
    public static List<IGameObject> listaNotasVerdes = new();
    public static List<IGameObject> listaNotasVermelhas = new();
    public static List<IGameObject> listaNotasAmarelas = new();
    public static List<IGameObject> listaNotasAzuis = new();
    public static List<IGameObject> listaNotasLaranjas = new();

    public static float timer = 0f;
    public static int offsetY = 80;
    public static int offsetX = 120;
    static LeitorDeMusicas leitorDeMusica;


    public GameScreen (string levelASerJogado) {
        ParticleManager.CarregarTexturas();

        GameManager.IniciarJogo();

        Alvo alvoVerde = new Alvo(0); 
        objetosDoJogo.Add(alvoVerde);
        
        Alvo alvoVermelho = new Alvo(1); 
        objetosDoJogo.Add(alvoVermelho);
        
        Alvo alvoAmarelo = new Alvo(2); 
        objetosDoJogo.Add(alvoAmarelo);
        
        Alvo alvoAzul = new Alvo(3); 
        objetosDoJogo.Add(alvoAzul);
        
        Alvo alvoLaranja = new Alvo(4); 
        objetosDoJogo.Add(alvoLaranja);

        leitorDeMusica = new LeitorDeMusicas(levelASerJogado);
        string musicaASerJogada =  (Program.localDosArquivos + "/" + leitorDeMusica.ObterNomeDoArquivoDaMusica());


        AudioManager.DefinirMusica(musicaASerJogada);


        // listaDeNotasDoJogo.Add(listaNotasVerdes);
        // listaDeNotasDoJogo.Add(listaNotasVermelhas);
        // listaDeNotasDoJogo.Add(listaNotasAmarelas);
        // listaDeNotasDoJogo.Add(listaNotasAzuis);
        // listaDeNotasDoJogo.Add(listaNotasLaranjas);
    }


    // ***  UPDATE   ***
    public void Update(float deltaTime) {
        if (Raylib.IsMouseButtonPressed(MouseButton.Left) || Raylib.IsKeyPressed(KeyboardKey.Space)) {
            CriarNovaBolinha();
        }

    // dps ajustar o timer ou o arquivo pra não precisa multiplicar por 1000
        timer += deltaTime * 1000;
        // if (timer >= 0.2f) {
        //     timer =- 0.2f;
        //     CriarNovaBolinha();
        // }
        //  O que antes era o "LoopArquivo();" agora ta dentro de leitordemusica, e ta acessando o timer...

        leitorDeMusica.UpdateMusica();

        foreach (IGameObject objeto in objetosDoJogo) {
            // Console.WriteLine(objeto.GetHashCode());
            objeto.Update(deltaTime);
        }

        // Eu ainda vou achar uma forma mais eficiente de fazer isso, mil desculpas eu do futuro. mas eu AINDA vou fazer
        // foreach (List<IGameObject> listaNotas in listaDeNotasDoJogo) {
        //     foreach (IGameObject notas in listaNotas) {
        //         notas.Update(deltaTime);
        //     }
        // }    

        ParticleManager.UpdateParticles(deltaTime);
        WordsManager.UpdatePalavras(deltaTime);

        foreach (IGameObject objeto in listaNotasVerdes) {
            objeto.Update(deltaTime);
        }
    
        foreach (IGameObject objeto in listaNotasVermelhas) {
            objeto.Update(deltaTime);
        }
    
        foreach (IGameObject objeto in listaNotasAmarelas) {
            objeto.Update(deltaTime);
        }
    
        foreach (IGameObject objeto in listaNotasAzuis) {
            objeto.Update(deltaTime);
        }
    
        foreach (IGameObject objeto in listaNotasLaranjas  ) {
            objeto.Update(deltaTime);
        }
    
    }


    // --=< DRAW >=-- 
    public void Draw() {
        Raylib.ClearBackground(Color.DarkGray);

        foreach (IGameObject objeto in objetosDoJogo) {
            objeto.Draw();
        }
        objetosDoJogo.RemoveAll(objeto => objeto.excluirObjeto);

        // foreach (List<IGameObject> listaNotas in listaDeNotasDoJogo) {
        //     foreach (IGameObject notas in listaNotas) {
        //         notas.Draw();
        //     }
        //     listaNotas.RemoveAll(notas => notas.excluirObjeto);
        // }        

        ParticleManager.DrawParticles();
        WordsManager.DesenharPalavras();


        foreach (IGameObject objeto in listaNotasVerdes) {
            objeto.Draw();
        }
        listaNotasVerdes.RemoveAll(objeto => objeto.excluirObjeto);

        foreach (IGameObject objeto in listaNotasVermelhas) {
            objeto.Draw();
        }
        listaNotasVermelhas.RemoveAll(objeto => objeto.excluirObjeto);

        foreach (IGameObject objeto in listaNotasAmarelas) {
            objeto.Draw();
        }
        listaNotasAmarelas.RemoveAll(objeto => objeto.excluirObjeto);

        foreach (IGameObject objeto in listaNotasAzuis) {
            objeto.Draw();
        }
        listaNotasAzuis.RemoveAll(objeto => objeto.excluirObjeto);

        foreach (IGameObject objeto in listaNotasAzuis) {
            objeto.Draw();
        }
        listaNotasAzuis.RemoveAll(objeto => objeto.excluirObjeto);

        foreach (IGameObject objeto in listaNotasLaranjas) {
            objeto.Draw();
        }
        listaNotasLaranjas.RemoveAll(objeto => objeto.excluirObjeto);
    }





    static void CriarNovaBolinha() {
        // alterar número dentro de "nota" para colocar de uma trilha específica (Verde(0), Vermelho, Amarelo, Azul e Laranja(4))
        Nota novaBolinha = new Nota();
        // objetosDoJogo.Add(novaBolinha);
        // listaNotasVerdes.Add(novaBolinha);
    }

    static void CriarNovaLinha() {}

    public void Unload() {
        Alvo.Unload();
        Nota.Unload();
        ParticleManager.DescarregarTexturas();
    }
}