class NoteSpawner {
    private readonly List<ChartNote> notes;
    private int nextNoteIndex;

    public NoteSpawner(Song song) {
        notes = song.Notes;
    }

    public void Update(GameplaySession session, GameContext context) {
        float spawnLeadTime = session.GetSpawnLeadTimeMs(context.Settings);

        while (nextNoteIndex < notes.Count && session.SongTimerMs >= notes[nextNoteIndex].TimeMs - spawnLeadTime) {
            ChartNote chartNote = notes[nextNoteIndex];
            session.AddNote(new Nota(chartNote.Lane, context.Settings, session));
            nextNoteIndex++;
        }
    }
}
