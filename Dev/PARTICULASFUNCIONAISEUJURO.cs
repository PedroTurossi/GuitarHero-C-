// using System;
// using System.Collections.Generic;
// using System.Numerics;
// using Raylib_cs;

// class ParticulasTelaTeste
// {
//     // 1. Define as propriedades de uma única partícula
//     struct Particula
//     {
//         public Vector2 Posicao;
//         public Vector2 Velocidade;
//         public Color Cor;
//         public float Tamanho;
//         public float VidaMax;    // Tempo total de vida em segundos
//         public float VidaRestante; // Tempo que ainda resta
//         public bool Ativa => VidaRestante > 0;
//     }

//     static void Main()
//     {
//         const int larguraTela = 800;
//         const int alturaTela = 600;

//         Raylib.InitWindow(larguraTela, alturaTela, "Sistema de Partículas Simples - Raylib");
//         Raylib.SetTargetFPS(60);

//         List<Particula> listaParticulas = new List<Particula>();
//         Random random = new Random();

//         while (!Raylib.WindowShouldClose())
//         {
//             float dt = Raylib.GetFrameTime(); // Tempo decorrido desde o último frame (Delta Time)
//             Vector2 posicaoMouse = Raylib.GetMousePosition();

//             // -------------------------------------------------------------
//             // 2. EMISSÃO: Cria novas partículas a cada frame se o mouse mover/clicar
//             // -------------------------------------------------------------
//             if (Raylib.IsMouseButtonDown(MouseButton.Left) || Raylib.GetMouseDelta().Length() > 0)
//             {
//                 // Cria 3 partículas por frame para um efeito mais denso
//                 for (int i = 0; i < 3; i++)
//                 {
//                     // Velocidade aleatória para espalhar as partículas
//                     float angulo = (float)(random.NextDouble() * Math.PI * 2);
//                     float velocidadeModulos = (float)(random.NextDouble() * 100 + 50); // Velocidade entre 50 e 150

//                     Particula p = new Particula
//                     {
//                         Posicao = posicaoMouse,
//                         Velocidade = new Vector2((float)Math.Cos(angulo) * velocidadeModulos, (float)Math.Sin(angulo) * velocidadeModulos),
//                         // Escolhe uma cor quente aleatória (Fogo/Faísca)
//                         Cor = random.Next(0, 2) == 0 ? Color.Orange : Color.Gold,
//                         Tamanho = (float)(random.NextDouble() * 8 + 4),
//                         VidaMax = (float)(random.NextDouble() * 0.8 + 0.2), // Vive entre 0.2 e 1.0 segundos
//                     };
//                     p.VidaRestante = p.VidaMax;

//                     listaParticulas.Add(p);
//                 }
//             }

//             // -------------------------------------------------------------
//             // 3. ATUALIZAÇÃO: Move as partículas e reduz o tempo de vida
//             // -------------------------------------------------------------
//             for (int i = listaParticulas.Count - 1; i >= 0; i--)
//             {
//                 Particula p = listaParticulas[i];
//                 p.VidaRestante -= dt;

//                 if (!p.Ativa)
//                 {
//                     listaParticulas.RemoveAt(i); // Remove se "morreu"
//                     continue;
//                 }

//                 // Aplica movimento com base na velocidade e no delta time
//                 p.Posicao += p.Velocidade * dt;

//                 // Opcional: Adiciona um efeito de gravidade empurrando para baixo
//                 p.Velocidade.Y += 50 * dt; 

//                 // Opcional: Desbota a transparência (Alpha) conforme a partícula morre
//                 float proporcaoVida = p.VidaRestante / p.VidaMax; // Vai de 1.0 até 0.0
//                 p.Cor.A = (byte)(proporcaoVida * 255);

//                 listaParticulas[i] = p; // Salva as alterações de volta na lista
//             }

//             // -------------------------------------------------------------
//             // 4. RENDERIZAÇÃO: Desenha as partículas ativas
//             // -------------------------------------------------------------
//             Raylib.BeginDrawing();
//             Raylib.ClearBackground(Color.Black);

//             foreach (var p in listaParticulas)
//             {
//                 // Desenha cada partícula como um círculo transparente
//                 Raylib.DrawCircleV(p.Posicao, p.Tamanho, p.Cor);
//             }

//             Raylib.DrawText("Mova ou clique com o mouse para gerar partículas", 10, 10, 20, Color.RayWhite);
//             Raylib.DrawText($"Partículas ativas: {listaParticulas.Count}", 10, 40, 18, Color.Green);

//             Raylib.EndDrawing();
//         }

//         Raylib.CloseWindow();
//     }
// }
