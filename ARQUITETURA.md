# Arquitetura do Projeto

Este documento explica a organizacao atual do jogo depois da refatoracao. A ideia principal e separar o que e ambiente do jogo, estado da partida, dados da musica, regras de gameplay e objetos visuais.

## Fluxo Geral

O jogo executa nesta ordem:

```text
Program
  cria GameContext
  inicializa janela/audio
  inicializa ScreenManager
  troca para LevelSelectScreen

Loop principal
  context.UpdateInput()
  ScreenManager.Update(deltaTime)
  ScreenManager.Draw()
```

Quando uma musica e selecionada:

```text
LevelSelectScreen
  cria GameScreen(caminhoDoJson)

GameScreen
  SongLoader carrega o JSON
  GameplaySession guarda a partida atual
  NoteSpawner cria notas no tempo certo
  HitJudge julga acertos e misses
  Nota e Alvo apenas atualizam/desenham seu proprio estado visual
```

## GameContext

Arquivo: `Systems/GameContext.cs`

O `GameContext` representa o ambiente geral do jogo. Ele nao e a partida atual. Ele guarda coisas que existem independentemente da musica que esta sendo jogada.

Responsabilidades:

- guardar `GameSettings`;
- guardar o `InputState`;
- atualizar o input uma vez por frame;
- oferecer acesso facil a largura, altura e pasta de assets.

Exemplo de uso:

```csharp
context.UpdateInput();

if (context.Input.PausePressed) {
    // pausa ou despausa
}
```

Antes, esse tipo de informacao ficava espalhado em `Program` ou era acessado diretamente via `Raylib`. Agora o resto do jogo pergunta ao contexto.

## GameSettings

Arquivo: `Systems/GameSettings.cs`

O `GameSettings` guarda configuracoes fixas ou quase fixas do jogo.

Responsabilidades:

- largura e altura da tela;
- caminho dos assets;
- quantidade de lanes;
- offsets dos alvos;
- velocidade das notas;
- tolerancias de hit;
- teclas de cada lane.

Isso evita numeros magicos espalhados pelo projeto. Se quiser trocar a tecla verde de `A` para outra, muda em um lugar so.

## InputState

Arquivo: `Systems/InputState.cs`

O `InputState` traduz Raylib para a linguagem do jogo.

Em vez de varias classes perguntarem:

```csharp
Raylib.IsKeyDown(KeyboardKey.A)
```

elas perguntam:

```csharp
context.Input.LaneDown[0]
```

Responsabilidades:

- ler teclado e mouse;
- guardar se pause foi pressionado;
- guardar se spawn de debug foi pressionado;
- guardar quais lanes estao pressionadas.

## GameplaySession

Arquivo: `Gameplay/GameplaySession.cs`

O `GameplaySession` representa a partida atual. Diferente do `GameContext`, ele muda quando voce entra em outra musica.

Responsabilidades:

- guardar a `Song` atual;
- guardar o estado da partida: jogando, pausado ou finalizado;
- guardar o timer da musica;
- guardar o score;
- guardar objetos gerais;
- guardar alvos;
- guardar notas separadas por lane;
- calcular posicoes dos alvos;
- calcular quanto tempo antes a nota precisa nascer para chegar no alvo.

Regra pratica:

```text
GameContext = ambiente do programa
GameplaySession = estado da partida atual
```

## Song e ChartNote

Arquivo: `Gameplay/Song.cs`

`Song` e o modelo de dados da musica carregada do JSON. Ela nao toca audio, nao cria nota visual e nao julga hit.

Responsabilidades:

- representar nome da musica;
- representar nome do arquivo de audio;
- guardar notas cruas vindas do JSON;
- converter notas cruas para `ChartNote`.

`ChartNote` representa uma nota do mapa:

```text
tempo em milissegundos
lane
```

## SongLoader

Arquivo: `Gameplay/SongLoader.cs`

O `SongLoader` apenas le o arquivo JSON e devolve uma `Song`.

Ele nao deve:

- criar `Nota`;
- mexer no timer;
- acessar `GameScreen`;
- tocar musica.

Essa separacao e importante porque carregar dados e executar gameplay sao responsabilidades diferentes.

## NoteSpawner

Arquivo: `Gameplay/NoteSpawner.cs`

O `NoteSpawner` transforma dados da musica em objetos do jogo.

Responsabilidades:

- olhar o tempo atual da `GameplaySession`;
- verificar quais `ChartNote` ja devem aparecer;
- criar `Nota`;
- adicionar a nota na lane correta da sessao.

Antes, essa responsabilidade ficava misturada no antigo `LeitorDeMusicas`. Agora o leitor le dados, e o spawner executa o spawn.

## HitJudge

Arquivo: `Gameplay/HitJudge.cs`

O `HitJudge` contem a regra de acerto.

Responsabilidades:

- verificar quais lanes estao pressionadas;
- encontrar a nota mais proxima do alvo;
- comparar distancia com tolerancias;
- decidir entre `Great`, `Good`, `Bad` ou nada;
- registrar `Miss`;
- adicionar pontuacao na `GameplaySession`;
- criar texto flutuante de julgamento;
- chamar particulas quando o acerto merece efeito.

Essa era a logica mais importante que saiu de `Alvo`. Agora `Alvo` nao precisa saber lista de notas nem regra de pontuacao.

## GameScreen

Arquivo: `Screens/GameScreen.cs`

`GameScreen` coordena a partida, mas tenta nao conter as regras internas.

Responsabilidades:

- carregar a musica escolhida;
- criar `GameplaySession`;
- criar `NoteSpawner`;
- criar alvos;
- tocar musica;
- pausar/despausar;
- chamar update dos sistemas;
- chamar draw dos objetos;
- descarregar recursos da tela.

Ela e a "maestro" da partida. Ela sabe a ordem das coisas, mas delega trabalho especializado.

## Nota

Arquivo: `GameObjects/Nota.cs`

`Nota` agora representa uma nota visual.

Responsabilidades:

- saber em qual lane esta;
- saber sua posicao;
- mover para baixo;
- marcar que virou `Missed` quando passa da tela;
- desenhar a textura.

Ela nao deve:

- se adicionar em lista global;
- acessar `GameScreen`;
- registrar pontuacao;
- decidir se foi `Great`, `Good` ou `Bad`.

Isso deixa a classe mais reutilizavel. Uma nota e apenas uma nota.

## Alvo

Arquivo: `GameObjects/Alvo.cs`

`Alvo` agora representa o alvo visual de uma lane.

Responsabilidades:

- guardar lane, cor e posicao;
- saber se esta pressionado;
- desenhar animacao simples quando pressionado.

Ele nao deve:

- buscar notas;
- ler teclado diretamente;
- julgar colisao;
- registrar score.

Essa logica agora fica no `HitJudge`.

## LaneColor

Arquivo: `Gameplay/LaneColor.cs`

Classe utilitaria para mapear lane para cor.

Isso evita repetir `switch` de cores em `Nota`, `Alvo` e outros lugares.

## LeitorDeMusicas

Arquivo: `FileSystem/LeitorDeMusicas.cs`

Foi mantido como ponte para nomes antigos, mas a leitura real agora esta em `SongLoader`.

Com o tempo, ele pode ser removido se nao for mais usado.

## O Que Melhorou

- `Nota` nao depende mais de `GameScreen`.
- `Alvo` nao contem mais regra de hit.
- O spawn de notas saiu do leitor de arquivos.
- O score agora pertence a partida atual.
- As listas de notas estao agrupadas por lane em `GameplaySession`.
- `GameContext` centraliza configuracao e input.
- O projeto compila sem avisos.

## Proximo Passo Recomendado

O proximo passo natural seria remover os ultimos acessos globais antigos:

- trocar `Program.posicaoDoMouse` por `context.Input.MousePosition` dentro de `Botao`;
- transformar `AudioManager`, `ParticleManager` e `WordsManager` em objetos de contexto ou sistemas de sessao;
- mover assets para uma estrutura mais clara, como `Files/Images`, `Files/Songs` e `Files/Charts`;
- remover `GameManager` se ele nao for mais usado.

Nao precisa fazer tudo de uma vez. A direcao importante e: estado de partida fica em `GameplaySession`; ambiente geral fica em `GameContext`; regras de gameplay ficam em sistemas como `HitJudge` e `NoteSpawner`.
