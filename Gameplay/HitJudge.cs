using System.Numerics;
using Raylib_cs;

class HitJudge {
    public void Update(GameplaySession session, GameContext context) {
        for (int lane = 0; lane < GameSettings.LaneCount; lane++) {
            bool lanePressed = context.Input.LaneDown[lane];

            if (lane < session.Targets.Count) {
                session.Targets[lane].SetPressed(lanePressed);
            }

            if (!lanePressed) {
                continue;
            }

            TryJudgeLane(lane, session, context);
        }
    }

    private void TryJudgeLane(int lane, GameplaySession session, GameContext context) {
        List<Nota> notes = session.NotesByLane[lane];
        if (notes.Count == 0) {
            return;
        }

        Vector2 targetPosition = session.GetTargetPosition(lane, context.Settings);
        Nota? closestNote = notes
            .Where(note => !note.excluirObjeto)
            .OrderBy(note => Vector2.Distance(targetPosition, note.posicaoObjeto))
            .FirstOrDefault();

        if (closestNote == null) {
            return;
        }

        float distance = Vector2.Distance(targetPosition, closestNote.posicaoObjeto);
        string? judgement = GetJudgement(distance, context.Settings);

        if (judgement == null) {
            // não tem nenhuma nota nessa lane agora ou não tem nenhuma nota sendo atingida pela Lane
            return;
        }

        if (judgement == "Good" || judgement == "Great") {
            ParticleManager.CarregarParticulasAleatorias(closestNote.cor, targetPosition);
        }

        if (judgement == "Great") {
            ParticleManager.CarregarParticulasAleatorias(closestNote.cor, targetPosition);
        }

        closestNote.excluirObjeto = true;
        RegisterJudgement(judgement, targetPosition, session, context.Settings);
    }

    public void RegisterMiss(Nota note, GameplaySession session, GameSettings settings) {
        Vector2 targetPosition = session.GetTargetPosition(note.Lane, settings);
        RegisterJudgement("Miss", targetPosition, session, settings);
    }

    private string? GetJudgement(float distance, GameSettings settings) {
        if (distance > settings.BadHitTolerance) {
            // fora do range de qualquer pontuação possível
            return null;
        }

        if (distance <= settings.GreatHitTolerance) {
            return "Great";
        }

        if (distance <= settings.GoodHitTolerance) {
            return "Good";
        }

        return "Bad";
    }

    private void RegisterJudgement(string judgement, Vector2 targetPosition, GameplaySession session, GameSettings settings) {
        string text = "";
        Color textColor = Color.White;
        int score = 0;

        switch (judgement) {
            case "Great":
                text = "+Great+";
                score = 7;
                break;
            case "Good":
                text = "Good+";
                score = 4;
                break;
            case "Bad":
                text = "Bad-";
                textColor = Color.LightGray;
                break;
            case "Miss":
                text = "-Miss-";
                score = -2;
                textColor = Color.Gray;
                break;
        }

        session.AddScore(score);
        Vector2 textPosition = new Vector2(targetPosition.X, targetPosition.Y + settings.TargetOffsetY / 2);
        WordsManager.AdicionarPalavra(textPosition, 14, text, textColor, true);
    }
}
