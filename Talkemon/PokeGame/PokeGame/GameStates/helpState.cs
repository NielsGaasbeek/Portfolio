using Microsoft.Xna.Framework;

class helpMenu : GameObjectList
{
    protected Button backButton;

    public helpMenu()
    {
        SpriteGameObject background = new SpriteGameObject("Backgrounds/helpBackground", 0);
        add(background);

        backButton = new Button("Buttons/backButton.png", 1, "backButton");
        backButton.Position = new Vector2(25, 25);
        add(backButton);

    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (backButton.Pressed)
        {
            GameEnvironment.GameStateManager.returnToPrevious();
        }
    }

}