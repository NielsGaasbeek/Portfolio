using Microsoft.Xna.Framework;

class playingState : GameObjectList
{
    OverworldGrid overworldgrid;
    GameObjectGrid charactergrid;

    public playingState()
    {
        Level level = new Level(1);
        add(level);

        overworldgrid = level.GetLevel;

        charactergrid = level.Getchars;
        charactergrid.CellWidth = 48;
        charactergrid.CellHeight = 48;

        add(overworldgrid);
        add(charactergrid);

        Player player = new Player(Vector2.Zero, "player", 2, "player");
        charactergrid.Add(player, 0, 0);

        Camera camera = new Camera(3, "camera");
        add(camera);
    }

}