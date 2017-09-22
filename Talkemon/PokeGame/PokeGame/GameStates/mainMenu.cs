using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

class mainMenu : GameObjectList
{
    protected Button startButton, helpButton, settingsButton;

    public mainMenu()
    {
        SpriteGameObject background = new SpriteGameObject("Backgrounds/mainMenuBackground.png", 0, "background");
        add(background);

        startButton = new Button("Buttons/playButton.png", 1, "Playbutton");
        startButton.Position = new Vector2(startButton.Width / 2, 2 * (GameEnvironment.Screen.Y / 3) - startButton.Height);
        add(startButton);

        helpButton = new Button("Buttons/helpButton.png", 1, "helpButton");
        helpButton.Position = new Vector2(helpButton.Width * 2, 2 * (GameEnvironment.Screen.Y / 3) - helpButton.Height);
        add(helpButton);

        settingsButton = new Button("Buttons/settingsButton.png", 1, "settingsButton");
        settingsButton.Position = new Vector2(GameEnvironment.Screen.X - 125, 25);
        add(settingsButton);

        GameEnvironment.AssetManager.PlayMusic("Audio/Music/OpeningTheme", true);

    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);

        if (startButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("playingState");
        }
        if (helpButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("HelpScreen");
        }
        if (settingsButton.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("Settings");
        }

    }


}