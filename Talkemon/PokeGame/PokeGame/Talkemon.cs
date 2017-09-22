using Microsoft.Xna.Framework;
using System;

class PokeGame : GameEnvironment
{
    static void Main()
    {
        PokeGame game = new PokeGame();
        game.Run();
    }

    public PokeGame()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        screen = new Point(1440, 825);
        windowSize = new Point(1024, 586);
        Fullscreen = false;

        // Load all gamestates here. Check TickTick for example.
        gameStateManager.AddGameState("MainMenu", new mainMenu());
        gameStateManager.AddGameState("Settings", new settingsMenu());
        gameStateManager.AddGameState("HelpScreen", new helpMenu());
        gameStateManager.AddGameState("playingState", new playingState());
        gameStateManager.SwitchTo("MainMenu");
	}

}
