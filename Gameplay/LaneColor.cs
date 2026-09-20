using Raylib_cs;

static class LaneColor {
    public static Color GetColor(int lane) {
        return lane switch {
            0 => Color.Green,
            1 => Color.Red,
            2 => Color.Yellow,
            3 => Color.Blue,
            4 => Color.Orange,
            _ => Color.White
        };
    }
}
