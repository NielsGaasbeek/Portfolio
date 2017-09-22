partial class Level : GameObjectList
{
    public Level(int levelIndex)
    {
        LoadTiles("Content/Files/Tiles/" + levelIndex + ".txt");
        LoadCharacters("Content/Files/Characters/" + levelIndex + ".txt");
    }
}