
using System.Numerics;
using Raylib_cs;

class Particle : IGameObject {
    public bool excluirObjeto { get; set; }
    public Color cor { get; set; }
    public Vector2 posicaoObjeto { get; set; }

    public Texture2D textura { get; set; }

    float vidaMax;
    float vidaRestante;
    float velocidade;
    Vector2 direcao;

    int idDaCor;

    Random rand = new Random();

    public Particle(Color corDaParticula, Vector2 posicaoBase, int tipoDeParticula = -1) {
        idDaCor = (tipoDeParticula != -1) ? tipoDeParticula : Random.Shared.Next(ParticleManager.listaDeTexturas.Count);
        Load();

        cor = corDaParticula;

        float angulo = (float)(rand.NextDouble() * Math.PI * 2);
        float velocidadeModulos = (float)(rand.NextDouble() * 200 + 80);
        velocidade = velocidadeModulos;
        direcao = new Vector2((float)Math.Cos(angulo), (float)Math.Sin(angulo));

        vidaMax = (float)rand.NextDouble() * 0.4f + .3f;
        vidaRestante = vidaMax;

        posicaoObjeto = posicaoBase;
    }


    public void Load() {
        if (ParticleManager.listaDeTexturas[idDaCor].Id == 0) {
            ParticleManager.CarregarTexturas();
        }
        Texture2D texturaDaParticula = ParticleManager.listaDeTexturas[idDaCor];
        textura = texturaDaParticula;
    }

    public void Update(float dt) {
        if (vidaRestante > 0) {
            vidaRestante -= dt;
            posicaoObjeto += direcao * velocidade * dt;
        } else {
            excluirObjeto = true;
        }

    }

    public void Draw() {
        if (textura.Id == 0) {
            Load();
        }

        Color corDaParticula = cor;
        float proporcaoDeVidarestante = (vidaRestante >= 0) ? ((vidaRestante/vidaMax > 0.7f) ? (vidaRestante/vidaMax + 0.1f) : (vidaRestante/vidaMax) ) : 0.05f;
        corDaParticula.A = (byte)(proporcaoDeVidarestante * 255);
        Vector2 vetorDaTextura = new Vector2((posicaoObjeto.X - (textura.Width/2)), (posicaoObjeto.Y - (textura.Height/2)));
        Raylib.DrawTextureV(textura, vetorDaTextura, corDaParticula);
        // Console.WriteLine("x - " + vetorDaTextura.X + " | y - " + vetorDaTextura.Y);
        // Raylib.DrawCircleV(posicaoObjeto, 15f, cor);
    }

}




