using Raylib_cs;

// obs: talvez eu precise criar depois uma versão de AudioManager e de MusicManager ...
class AudioManager {
    private static Music musicaASerTocada;
    public static void InicializarAudioManager() {
        Raylib.InitAudioDevice();
    }

    public static void DefinirMusica(String stringLocalDaMusica) {
        musicaASerTocada = Raylib.LoadMusicStream(stringLocalDaMusica);
        Raylib.PlayMusicStream(musicaASerTocada);
    }

    public static void UpdateMusica() {
        Raylib.UpdateMusicStream(musicaASerTocada);
    }

    public static void UnloadMusica() {
        Raylib.UnloadMusicStream(musicaASerTocada);
    }
}