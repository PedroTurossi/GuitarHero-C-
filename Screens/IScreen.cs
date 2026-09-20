interface IScreen {
    void Update(float deltaTime, GameContext context);
    void Draw(GameContext context);

    void Unload();
}
