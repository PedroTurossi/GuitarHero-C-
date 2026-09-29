using System.Numerics;
using Raylib_cs;

class Menu {
    private readonly List<MenuButton> buttons = new();
    private readonly List<int> extraSpacingBeforeButton = new();
    private readonly Vector2 position;
    private readonly int buttonWidth;
    private readonly int buttonHeight;
    private readonly int spacing;
    private int selectedIndex;
    private int pendingSpacing;

    public Action? BackRequested { get; set; }

    public Menu(Vector2 position, int buttonWidth, int buttonHeight, int spacing) {
        this.position = position;
        this.buttonWidth = buttonWidth;
        this.buttonHeight = buttonHeight;
        this.spacing = spacing;
    }

    public MenuButton AddButton(string text, Action action) {
        MenuButton button = new(text, action);
        buttons.Add(button);
        extraSpacingBeforeButton.Add(pendingSpacing);
        UpdateButtonBounds();
        return button;
    }

    // Adiciona espaço apenas antes do próximo botão. Isso permite separar
    // visualmente grupos de opções sem criar um botão falso e sem afetar a
    // navegação do menu.
    public void AddSpacing(int pixels) {
        pendingSpacing += Math.Max(0, pixels);
        UpdateButtonBounds();
    }

    public void Update(GameContext context) {
        if (buttons.Count == 0) {
            return;
        }

        int hoveredIndex = FindHoveredButton(context.Input.MousePosition);
        if (hoveredIndex >= 0) {
            selectedIndex = hoveredIndex;
        }

        if (context.Input.MenuUpPressed) {
            selectedIndex = (selectedIndex - 1 + buttons.Count) % buttons.Count;
        } else if (context.Input.MenuDownPressed) {
            selectedIndex = (selectedIndex + 1) % buttons.Count;
        }

        if (context.Input.MouseLeftPressed && hoveredIndex >= 0) {
            buttons[hoveredIndex].Activate();
        } else if (context.Input.MenuConfirmPressed) {
            buttons[selectedIndex].Activate();
        } else if (context.Input.PausePressed) {
            BackRequested?.Invoke();
        }
    }

    public void Draw() {
        for (int i = 0; i < buttons.Count; i++) {
            buttons[i].Draw(i == selectedIndex);
        }
    }

    private int FindHoveredButton(Vector2 mousePosition) {
        for (int i = 0; i < buttons.Count; i++) {
            if (buttons[i].Contains(mousePosition)) {
                return i;
            }
        }

        return -1;
    }

    private void UpdateButtonBounds() {
        for (int i = 0; i < buttons.Count; i++) {
            float y = position.Y
                + i * (buttonHeight + spacing)
                + extraSpacingBeforeButton[i];
            buttons[i].SetBounds(new Rectangle(position.X, y, buttonWidth, buttonHeight));
        }
    }
}
