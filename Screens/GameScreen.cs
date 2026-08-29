using System.Numerics;
using Raylib_cs;

class GameScreen : IScreen {
    static List<IGameObject> objetosDoJogo = new();
    float timer = 0f;
    public static int offsetY = 40;
    public static int offsetX = 120;


    public GameScreen () {
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
    }


    // ***  UPDATE   ***
    public void Update(float deltaTime) {
        if (Raylib.IsMouseButtonPressed(MouseButton.Left)) {
            CriarNovaBolinha();
        }

        // timer += deltaTime;
        // if (timer >= 0.2f) {
        //     timer =- 0.2f;
        //     CriarNovaBolinha();
        // }
        // LoopArquivo();

        foreach (IGameObject objeto in objetosDoJogo) {
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
    }





    static void CriarNovaBolinha() {
        Nota novaBolinha = new Nota();
        objetosDoJogo.Add(novaBolinha);
    }

    //     static void LoopArquivo() {
    //     // "notas" consiste em uma lista de lista de floats, onde cada lista de lista representa um conjunto de "nota"
    //     // e cada lista de float (ou seja, cada nota), tem 3 números.
    //     // o primeiro significa o tempo até ela ser reproduzida, sendo que o tempo reinicia a cada nota. ou seja: 0.1 0.1 0.1 acontecem com 0.1 de espaçamento entre as notas
    //     // o segundo número significa "up" (1) ou "down" (2), representando em qual dos alvos a nota vai mirar
    //     // o terceiro numero significa a quantidade de vezes que essa nota se repete (3 notas de 0.2 segundos seguindas fica [0.2, 1, 3])
    //     if(notas[0][0] == -1 || notas[0][1] == 0) {
    //         // ?
    //     } else {
    //         if (timer >= notas[0][0]) {
    //             if (notas[0][2] == 0) {
    //                 notas.RemoveAt(0);
    //             } else {
    //                 CriarNovaBolinha();
    //                 notas[0][2]--;
    //                 timer -= notas[0][0];
    //             }
                
    //         }            
    //     }

    // }
}