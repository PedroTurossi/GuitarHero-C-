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

Os proximos passos naturais sao:

- transformar `AudioManager`, `ParticleManager` e `WordsManager` em objetos de contexto ou sistemas de sessao;
- mover assets para uma estrutura mais clara, como `Files/Images`, `Files/Songs` e `Files/Charts`;
- remover `GameManager` se ele nao for mais usado.

Nao precisa fazer tudo de uma vez. A direcao importante e: estado de partida fica em `GameplaySession`; ambiente geral fica em `GameContext`; regras de gameplay ficam em sistemas como `HitJudge` e `NoteSpawner`.

## Arquitetura de menus

Menus sao componentes reutilizaveis, e nao telas completas. Uma tela como `LevelSelectScreen` ou `GameScreen` possui um `Menu` e decide quando ele deve ser atualizado e desenhado.

### MenuButton

Arquivo: `GameObjects/Menus/MenuButton.cs`

`MenuButton` representa uma opcao individual do menu. Ele guarda o texto, a area clicavel e uma acao executada quando a opcao e confirmada.

Responsabilidades:

- guardar o texto da opcao;
- guardar os limites do botao;
- verificar se o mouse esta sobre o botao;
- desenhar o estado normal ou selecionado;
- executar a acao associada.

O botao nao decide como o teclado navega entre as opcoes. Essa responsabilidade pertence ao `Menu`.

### Menu

Arquivo: `GameObjects/Menus/Menu.cs`

`Menu` organiza uma lista vertical de `MenuButton`. Ele centraliza o comportamento comum de menus do jogo.

Responsabilidades:

- adicionar opcoes com `AddButton(texto, acao)`;
- calcular automaticamente a posicao dos botoes;
- selecionar uma opcao com hover do mouse;
- navegar com setas ou `W`/`S`;
- confirmar com clique, `Enter` ou `Space`;
- executar uma acao opcional de voltar com `BackRequested`;
- desenhar todos os botoes.

O `Menu` recebe `GameContext` no metodo `Update`. Assim, ele usa o `InputState` sem acessar `Raylib` ou `Program` diretamente. Para criar um menu novo, a tela precisa apenas instanciar um `Menu`, adicionar botoes e chamar `Update` e `Draw` no momento adequado.

### Menu de pausa

O menu de pausa pertence ao `GameScreen`, mas nao substitui a tela. Quando `GameplaySession.State` e `Pausado`, a partida deixa de ser atualizada, a musica e pausada e o `Menu` e desenhado sobre o gameplay.

As opcoes atuais sao:

- `Continuar`: retoma a sessao e a musica;
- `Reiniciar`: cria novamente o `GameScreen` usando o mesmo mapa;
- `Voltar`: retorna para `LevelSelectScreen`;
- `Sair`: marca `GameContext.ExitRequested`, e o loop principal encerra a janela com seguranca.

Essa diferenca e importante: `ScreenManager.ChangeScreen` deve ser usado para trocar de tela, enquanto o menu de pausa deve ser usado como componente sobreposto quando a partida precisa continuar existindo.

### Fluxo de input dos menus

`InputState` traduz as entradas do Raylib para o jogo:

- `MenuUpPressed`: seta para cima ou `W`;
- `MenuDownPressed`: seta para baixo ou `S`;
- `MenuConfirmPressed`: `Enter` ou `Space`;
- `PausePressed`: `Escape`, usado para pausar e para voltar/fechar o menu de pausa.

Com isso, telas diferentes compartilham a mesma navegacao e novas telas podem reutilizar `Menu` sem duplicar regras de mouse e teclado.
