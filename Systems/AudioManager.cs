using Raylib_cs;

// obs: talvez eu precise criar depois uma versão de AudioManager e de MusicManager ...
class AudioManager {
    private static Music musicaASerTocada;
    private static bool musicaCarregada;
    public static float Volume { get; private set; } = 1f;

    public static void InicializarAudioManager() {
        Raylib.InitAudioDevice();
        Raylib.SetMasterVolume(Volume);
    }

    public static void DefinirVolume(float volume) {
        Volume = Math.Clamp(volume, 0f, 1f);
        Raylib.SetMasterVolume(Volume);
    }

    public static void DefinirMusica(String stringLocalDaMusica) {
        if (musicaCarregada) {
            Raylib.UnloadMusicStream(musicaASerTocada);
        }
        musicaASerTocada = Raylib.LoadMusicStream(stringLocalDaMusica);
        musicaCarregada = true;
        Raylib.PlayMusicStream(musicaASerTocada);
    }

    public static void UpdateMusica() {
        Raylib.UpdateMusicStream(musicaASerTocada);
    }

    public static void PausarMusica() {
        Raylib.PauseMusicStream(musicaASerTocada);
    }

    public static void DespausarMusica() {
        Raylib.ResumeMusicStream(musicaASerTocada);
    }

    public static void UnloadMusica() {
        if (musicaCarregada) {
            Raylib.UnloadMusicStream(musicaASerTocada);
            musicaASerTocada = default;
            musicaCarregada = false;
        }
    }
}
