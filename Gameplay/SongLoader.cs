using System.Text.Json;

class SongLoader {
    public Song Load(string path) {
        string jsonString = File.ReadAllText(path);
        Song? song = JsonSerializer.Deserialize<Song>(jsonString);

        if (song == null) {
            throw new InvalidOperationException($"Nao foi possivel carregar a musica: {path}");
        }

        return song;
    }
}
