# Arquitetura do Projeto

Este documento explica a organização atual do jogo depois da refatoração. A ideia principal é separar o que é ambiente do jogo, estado da partida, dados da música, regras de gameplay e objetos visuais.

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

Quando uma música é selecionada:

```text
LevelSelectScreen

  cria GameScreen(caminhoDoJson)

GameScreen

  SongLoader carrega o JSON

  GameplaySession guarda a partida atual

  NoteSpawner cria notas no tempo certo

  HitJudge julga acertos e misses

  Nota e Alvo apenas atualizam/desenham seu próprio estado visual
```

## GameContext

Arquivo: `Systems/GameContext.cs`

O `GameContext` representa o ambiente geral do jogo. Ele não é a partida atual. Ele guarda coisas que existem independentemente da música que está sendo jogada.

Responsabilidades:

* guardar `GameSettings`;
* guardar o `InputState`;
* atualizar o input uma vez por frame;
* oferecer acesso fácil à largura, altura e pasta de assets.

Exemplo de uso:

```csharp
context.UpdateInput();

if (context.Input.PausePressed) {

    // pausa ou despausa

}
```

Antes, esse tipo de informação ficava espalhado em `Program` ou era acessado diretamente via `Raylib`. Agora, o restante do jogo pergunta ao contexto.

## GameSettings

Arquivo: `Systems/GameSettings.cs`

O `GameSettings` guarda configurações fixas ou quase fixas do jogo.

Responsabilidades:

* largura e altura da tela;
* caminho dos assets;
* quantidade de lanes;
* offsets dos alvos;
* velocidade das notas;
* tolerâncias de hit;
* teclas de cada lane.

Isso evita números mágicos espalhados pelo projeto. Se quiser trocar a tecla verde de `A` para outra, basta alterar em um único lugar.

## InputState

Arquivo: `Systems/InputState.cs`

O `InputState` traduz o Raylib para a linguagem do jogo.

Em vez de várias classes perguntarem:

```csharp
Raylib.IsKeyDown(KeyboardKey.A)
```

elas perguntam:

```csharp
context.Input.LaneDown[0]
```

Responsabilidades:

* ler teclado e mouse;
* guardar se a pausa foi pressionada;
* guardar se o spawn de debug foi pressionado;
* guardar quais lanes estão pressionadas.

## GameplaySession

Arquivo: `Gameplay/GameplaySession.cs`

O `GameplaySession` representa a partida atual. Diferentemente do `GameContext`, ele muda quando você entra em outra música.

Responsabilidades:

* guardar a `Song` atual;
* guardar o estado da partida: jogando, pausado ou finalizado;
* guardar o timer da música;
* guardar o score;
* guardar objetos gerais;
* guardar alvos;
* guardar notas separadas por lane;
* calcular as posições dos alvos;
* calcular quanto tempo antes a nota precisa nascer para chegar ao alvo.

Regra prática:

```text
GameContext = ambiente do programa

GameplaySession = estado da partida atual
```

## Song e ChartNote

Arquivo: `Gameplay/Song.cs`

`Song` é o modelo de dados da música carregada do JSON. Ela não toca áudio, não cria nota visual e não julga hit.

Responsabilidades:

* representar o nome da música;
* representar o nome do arquivo de áudio;
* guardar as notas cruas vindas do JSON;
* converter notas cruas para `ChartNote`.

`ChartNote` representa uma nota do mapa:

```text
tempo em milissegundos

lane
```

## SongLoader

Arquivo: `Gameplay/SongLoader.cs`

O `SongLoader` apenas lê o arquivo JSON e devolve uma `Song`.

Ele não deve:

* criar `Nota`;
* mexer no timer;
* acessar `GameScreen`;
* tocar música.

Essa separação é importante porque carregar dados e executar o gameplay são responsabilidades diferentes.

## NoteSpawner

Arquivo: `Gameplay/NoteSpawner.cs`

O `NoteSpawner` transforma os dados da música em objetos do jogo.

Responsabilidades:

* olhar o tempo atual da `GameplaySession`;
* verificar quais `ChartNote` já devem aparecer;
* criar `Nota`;
* adicionar a nota à lane correta da sessão.

Antes, essa responsabilidade ficava misturada no antigo `LeitorDeMusicas`. Agora, o leitor lê os dados, e o spawner executa o spawn.

## HitJudge

Arquivo: `Gameplay/HitJudge.cs`

O `HitJudge` contém a regra de acerto.

Responsabilidades:

* verificar quais lanes estão pressionadas;
* encontrar a nota mais próxima do alvo;
* comparar a distância com as tolerâncias;
* decidir entre `Great`, `Good`, `Bad` ou nada;
* registrar `Miss`;
* adicionar pontuação à `GameplaySession`;
* criar texto flutuante de julgamento;
* chamar partículas quando o acerto merece efeito.

Essa era a lógica mais importante que saiu de `Alvo`. Agora, `Alvo` não precisa saber a lista de notas nem a regra de pontuação.

## GameScreen

Arquivo: `Screens/GameScreen.cs`

`GameScreen` coordena a partida, mas tenta não conter as regras internas.

Responsabilidades:

* carregar a música escolhida;
* criar `GameplaySession`;
* criar `NoteSpawner`;
* criar alvos;
* tocar música;
* pausar/despausar;
* chamar o update dos sistemas;
* chamar o draw dos objetos;
* descarregar recursos da tela.

Ela é a "maestrina" da partida. Ela sabe a ordem das coisas, mas delega o trabalho especializado.

## Nota

Arquivo: `GameObjects/Nota.cs`

`Nota` agora representa uma nota visual.

Responsabilidades:

* saber em qual lane está;
* saber sua posição;
* mover-se para baixo;
* marcar que virou `Missed` quando passa da tela;
* desenhar a textura.

Ela não deve:

* se adicionar em uma lista global;
* acessar `GameScreen`;
* registrar pontuação;
* decidir se foi `Great`, `Good` ou `Bad`.

Isso deixa a classe mais reutilizável. Uma nota é apenas uma nota.

## Alvo

Arquivo: `GameObjects/Alvo.cs`

`Alvo` agora representa o alvo visual de uma lane.

Responsabilidades:

* guardar lane, cor e posição;
* saber se está pressionado;
* desenhar uma animação simples quando pressionado.

Ele não deve:

* buscar notas;
* ler o teclado diretamente;
* julgar colisões;
* registrar score.

Essa lógica agora fica no `HitJudge`.

## LaneColor

Arquivo: `Gameplay/LaneColor.cs`

Classe utilitária para mapear uma lane para uma cor.

Isso evita repetir `switch` de cores em `Nota`, `Alvo` e outros lugares.

## LeitorDeMusicas

Arquivo: `FileSystem/LeitorDeMusicas.cs`

Foi mantido como ponte para nomes antigos, mas a leitura real agora está em `SongLoader`.

Com o tempo, ele pode ser removido se não for mais utilizado.

## Arquitetura de menus

Menus são componentes reutilizáveis, e não telas completas. Uma tela como `LevelSelectScreen` ou `GameScreen` possui um `Menu` e decide quando ele deve ser atualizado e desenhado.

### MenuButton

Arquivo: `GameObjects/Menus/MenuButton.cs`

`MenuButton` representa uma opção individual do menu. Ele guarda o texto, a área clicável e uma ação executada quando a opção é confirmada.

Responsabilidades:

* guardar o texto da opção;
* guardar os limites do botão;
* verificar se o mouse está sobre o botão;
* desenhar o estado normal ou selecionado;
* executar a ação associada.

O botão não decide como o teclado navega entre as opções. Essa responsabilidade pertence ao `Menu`.

### Menu

Arquivo: `GameObjects/Menus/Menu.cs`

`Menu` organiza uma lista vertical de `MenuButton`. Ele centraliza o comportamento comum dos menus do jogo.

Responsabilidades:

* adicionar opções com `AddButton(texto, acao)`;
* calcular automaticamente a posição dos botões;
* selecionar uma opção com o hover do mouse;
* navegar com as setas ou `W`/`S`;
* confirmar com clique, `Enter` ou `Space`;
* executar uma ação opcional de voltar com `BackRequested`;
* desenhar todos os botões.

O `Menu` recebe `GameContext` no método `Update`. Assim, ele utiliza o `InputState` sem acessar `Raylib` ou `Program` diretamente.

Para criar um menu novo, a tela precisa apenas instanciar um `Menu`, adicionar os botões e chamar `Update` e `Draw` no momento adequado.

### Menu de pausa

O menu de pausa pertence ao `GameScreen`, mas não substitui a tela. Quando `GameplaySession.State` é `Pausado`, a partida deixa de ser atualizada, a música é pausada e o `Menu` é desenhado sobre o gameplay.

As opções atuais são:

* `Continuar`: retoma a sessão e a música;
* `Reiniciar`: cria novamente o `GameScreen` usando o mesmo mapa;
* `Voltar`: retorna para `LevelSelectScreen`;
* `Sair`: marca `GameContext.ExitRequested`, e o loop principal encerra a janela com segurança.

Essa diferença é importante: `ScreenManager.ChangeScreen` deve ser usado para trocar de tela, enquanto o menu de pausa deve ser usado como um componente sobreposto quando a partida precisa continuar existindo.

### Fluxo de input dos menus

`InputState` traduz as entradas do Raylib para o jogo:

* `MenuUpPressed`: seta para cima ou `W`;
* `MenuDownPressed`: seta para baixo ou `S`;
* `MenuConfirmPressed`: `Enter` ou `Space`;
* `PausePressed`: `Escape`, usado para pausar e para voltar/fechar o menu de pausa.

Com isso, telas diferentes compartilham a mesma navegação, e novas telas podem reutilizar `Menu` sem duplicar regras de mouse e teclado.
