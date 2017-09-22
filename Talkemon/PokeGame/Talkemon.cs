using Microsoft.Xna.Framework;

class Talkemon : GameEnvironment
{
    static void Main()
    {
        Talkemon game = new Talkemon();
        game.Run();
    }

    public Talkemon()
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
        GameStateManager.AddGameState("LoadSaveMenu", new loadSaveState());
        gameStateManager.AddGameState("playingState", new playingState());
        GameStateManager.AddGameState("battleState", new BattleState());
        gameStateManager.SwitchTo("MainMenu");
    }

}
