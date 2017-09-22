using Microsoft.Xna.Framework;

class settingsMenu : GameObjectList
{
    protected Button backButton;
    protected Slider musicVolume;
    protected TextGameObject volumeValue;
    protected float volume;

    public settingsMenu()
    {
        SpriteGameObject background = new SpriteGameObject("Backgrounds/settingsBackground.png", 0);
        add(background);

        backButton = new Button("Buttons/backButton.png", 1, "backButton");
        backButton.Position = new Vector2(25, 25);
        add(backButton);

        TextGameObject musicText = new TextGameObject("Fonts/Hud", 1);
        musicText.Text = "Music Volume";
        musicText.Color = Color.Black;
        musicText.Position = new Vector2(275, GameEnvironment.Screen.Y / 4);
        add(musicText);

        musicVolume = new Slider("Buttons/sliderBack.png", "Buttons/sliderButton.png", 1, "MusicVolSlider");
        musicVolume.Position = new Vector2(musicText.Position.X + 200, musicText.Position.Y - 20);
        add(musicVolume);

        volumeValue = new TextGameObject("Fonts/Hud", 1);
        volumeValue.Text = "100%";
        volumeValue.Color = Color.Black;
        volumeValue.Position = new Vector2(musicVolume.Position.X + 525, musicText.Position.Y);
        add(volumeValue);

        musicVolume.Value = GameEnvironment.AssetManager.Volume;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (backButton.Pressed)
        {
            GameEnvironment.GameStateManager.returnToPrevious();
        }
    }

    public override void Update(GameTime gameTime)
    {
        GameEnvironment.AssetManager.Volume = musicVolume.Value;
        volume = (int)(musicVolume.Value * 100);
        volumeValue.Text = volume.ToString() + "%";
    }

}