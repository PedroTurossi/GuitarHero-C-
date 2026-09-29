
using System.Numerics;
using Raylib_cs;

class ParticleManager {

    public static List<IGameObject> listaDeParticulas = new List<IGameObject>();

    public static List<Texture2D> listaDeTexturas = new List<Texture2D>();

    static int quantidadeMinimaDeParticulas = 10;
    static int quantidadeMaximaDeParticulas = 35;

    public ParticleManager() {

    }

    public static void CarregarTexturas() {
        if (listaDeTexturas.Count > 0) {
            return;
        }

        string localDasTexturas = Program.localDosArquivos + "Particulas";
        string[] arquivosDeTexturaExistentes = LeitorDeArquivos.LerImagensDoDiretorio(localDasTexturas);
        
        foreach(string arquivo in arquivosDeTexturaExistentes) {
            Texture2D textura = Raylib.LoadTexture(arquivo);
            listaDeTexturas.Add(textura);
            Console.WriteLine((textura.Id != 0) ? "### - carregadíssimo :)" : "#!# - Não carregaou");
        }
    }
    
    public static void CarregarParticulasAleatorias(Color corDasParticulas, Vector2 posicaoBase) {
        if (listaDeTexturas.Count == 0) {
            CarregarTexturas();
        }

        if (listaDeTexturas.Count == 0) {
            return;
        }

        for (int i = quantidadeMinimaDeParticulas; i < Random.Shared.Next(quantidadeMaximaDeParticulas); i++) {
            Particle novaParticula = new Particle(corDasParticulas, posicaoBase);
            listaDeParticulas.Add(novaParticula);
        }
    }

    public static void DescarregarTexturas() {
        // As partículas guardam uma cópia do Texture2D. Removê-las evita que
        // uma tela nova tente desenhar uma textura já descarregada.
        listaDeParticulas.Clear();
        foreach(Texture2D textura in listaDeTexturas) {
            if (textura.Id != 0) {
                Raylib.UnloadTexture(textura);
            }
        }
        listaDeTexturas.Clear();
    }

    public static void UpdateParticles(float deltaTime) {
        foreach (IGameObject particula in listaDeParticulas) {
            particula.Update(deltaTime);
        }
    }

    public static void DrawParticles() {
        foreach (IGameObject particula in listaDeParticulas) {
            particula.Draw();
        }
        listaDeParticulas.RemoveAll(particula => particula.excluirObjeto);
    }

} 
