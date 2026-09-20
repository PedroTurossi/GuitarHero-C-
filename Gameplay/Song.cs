using System.Text.Json.Serialization;

class Song {
    [JsonPropertyName("nomeMusica")]
    public string Name { get; set; } = "";

    [JsonPropertyName("nomeDaMusica")]
    public string AlternateName {
        set => Name = value;
    }

    [JsonPropertyName("nomeDoArquivoDaMusica")]
    public string AudioFile { get; set; } = "";

    [JsonPropertyName("notas")]
    public List<List<float>> RawNotes { get; set; } = new();

    [JsonIgnore]
    public List<ChartNote> Notes => RawNotes
        .Where(note => note.Count >= 2)
        .Select(note => new ChartNote(note[0], (int)note[1]))
        .Where(note => note.Lane >= 0 && note.Lane < GameSettings.LaneCount)
        .OrderBy(note => note.TimeMs)
        .ToList();
}

readonly record struct ChartNote(float TimeMs, int Lane);
