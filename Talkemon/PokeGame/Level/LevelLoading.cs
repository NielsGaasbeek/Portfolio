using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class PlayingState : IGameLoopObject
{
    protected List<Level> levels;
    protected int currentLevelIndex;
    protected ContentManager content;

    public PlayingState(ContentManager content)
    {
        this.content = content;
        currentLevelIndex = -1;
        levels = new List<Level>();
        LoadLevels();
    }

    public Level CurrentLevel
    {
        get { return levels[currentLevelIndex]; }
    }

    public int CurrentLevelIndex
    {
        get { return currentLevelIndex; }
        set
        {
            if (value >= 0 && value < levels.Count)
            {
                currentLevelIndex = value;
                CurrentLevel.Reset();
            }
        }
    }

    public List<Level> Levels
    {
        get { return levels; }
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {
        CurrentLevel.HandleInput(inputHelper);
    }

    public virtual void Update(GameTime gameTime)
    {
        CurrentLevel.Update(gameTime);
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        CurrentLevel.Draw(gameTime, spriteBatch);
    }

    public virtual void Reset()
    {
        CurrentLevel.Reset();
    }

    public void NextLevel()
    {
        CurrentLevel.Reset();
    }

    public void LoadLevels()
    {
        for (int currLevel = 1; currLevel <= 10; currLevel++)
        {
            levels.Add(new Level(currLevel));
        }
    }

    public void LoadLevelsStatus(string path)
    {
        List<string> textlines = new List<string>();
        StreamReader fileReader = new StreamReader(path);
        fileReader.Close();
    }
}
