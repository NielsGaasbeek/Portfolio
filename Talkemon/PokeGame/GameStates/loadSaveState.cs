using Microsoft.Xna.Framework;

class loadSaveState : GameObjectList
{
    protected Button newSaveButton, testOverWorld, testBattle;

    public loadSaveState()
    {
        SpriteGameObject background = new SpriteGameObject("Backgrounds/settingsBackground.png", 0, "background");
        add(background);

        newSaveButton = new Button("Buttons/newSaveButton", 1);
        newSaveButton.Position = new Vector2((GameEnvironment.Screen.X - newSaveButton.Width) / 2, 2 * (GameEnvironment.Screen.Y / 3));
        add(newSaveButton);

        testOverWorld = new Button("Buttons/testPlay", 1);
        testOverWorld.Position = new Vector2(200, 200);
        add(testOverWorld);

        testBattle = new Button("Buttons/testBattle", 1);
        testBattle.Position = new Vector2(GameEnvironment.Screen.X - testBattle.Width - 200, 200);
        add(testBattle);

    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (testOverWorld.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("playingState");
            GameEnvironment.AssetManager.PlayMusic("Audio/Music/overworldTheme", true);
            GameEnvironment.AssetManager.PlaySound("Audio/Effects/click");
        }
        if (testBattle.Pressed)
        {
            GameEnvironment.GameStateManager.SwitchTo("battleState");
            GameEnvironment.AssetManager.PlayMusic("Audio/Music/BattleTheme", true);
            GameEnvironment.AssetManager.PlaySound("Audio/Effects/click");
        }
    }

}