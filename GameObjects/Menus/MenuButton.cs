using System.Numerics;
using Raylib_cs;

class MenuButton {
    public string Text { get; }
    public Rectangle Bounds { get; private set; }

    private readonly Action action;

    public MenuButton(string text, Action action) {
        Text = text;
        this.action = action;
    }

    public void SetBounds(Rectangle bounds) {
        Bounds = bounds;
    }

    public bool Contains(Vector2 point) {
        return Raylib.CheckCollisionPointRec(point, Bounds);
    }

    public void Activate() {
        action();
    }

    public void Draw(bool selected) {
        Color background = selected ? Color.LightGray : Color.Gray;
        Raylib.DrawRectangleRec(Bounds, background);

        const int fontSize = 20;
        int textWidth = Raylib.MeasureText(Text, fontSize);
        int textX = (int)(Bounds.X + (Bounds.Width - textWidth) / 2f);
        int textY = (int)(Bounds.Y + (Bounds.Height - fontSize) / 2f);

        Raylib.DrawText(Text, textX, textY, fontSize, Color.White);
    }
}
