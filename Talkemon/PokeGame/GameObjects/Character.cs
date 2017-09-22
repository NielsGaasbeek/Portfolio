using Microsoft.Xna.Framework;

// Elk character kan 4 kanten op kijken. 
enum Direction
{
    Up,
    Down,
    Left,
    Right
}

class Character : AnimatedGameObject
{
    // Elk character moet bijhouden welke kant hij/zij/gender opkijkt
    protected Direction facing;

    // Bij het aanmaken wordt een positie meegegeven. Dit is eigenlijk alleen nodig bij de eerste frame.
    public Character(Vector2 startposition, string assetname, int layer = 0, string id = "")
        : base(layer, id)
    {
        Position = startposition;

        // De standaard kant die een character opkijkt is naar beneden.
        facing = Direction.Down;

        //voorlopig heeft alleen de speler animaties
        if(assetname == "player")
        {
            LoadAnimation("Characters/" + assetname + "/walk_down@3", "walkdown", true, 0.5f);
        }


        // Elk character moet een plaatje hebben voor elke vier de richtingen. 
        LoadAnimation("Characters/" + assetname + "/idle_up", "idleup", true);
        LoadAnimation("Characters/" + assetname + "/idle_down", "idledown", true);
        LoadAnimation("Characters/" + assetname + "/idle_left", "idleleft", true);
        LoadAnimation("Characters/" + assetname + "/idle_right", "idleright", true);
        PlayAnimation("idledown");
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        // De juiste animatie wordt gespeeld. 
        if (facing == Direction.Up)
            PlayAnimation("idleup");/*
        else if (facing == Direction.Down)
            PlayAnimation("idledown");*/
        else if (facing == Direction.Left)
            PlayAnimation("idleleft");
        else if (facing == Direction.Right)
            PlayAnimation("idleright");

    }

    // Zo kan de direction van buiten de klasse worden aangepast en afgelezen.
    public Direction Facing
    {
        get { return facing; }
        set { facing = value; }
    }

    // Om directions naar ints om te zetten. Je kan maar een waarde returnen, zonder ingewikkelde code. Vandaar 2 methoden.
    // Voornamelijk nodig in Player. Zou kunnen gebruikt worden in belangrijke NPC's.
    public int Directiontox(Direction dir)
    {
        int x = 0;
        if (dir == Direction.Left)
            x = -1;
        else if (dir == Direction.Right)
            x = 1;
        return x;
    }

    public int Directiontoy(Direction dir)
    {
        int y = 0;
        if (dir == Direction.Up)
            y = -1;
        else if (dir == Direction.Down)
            y = 1;
        return y;
    }

    // Om weer terug te rekenen naar een direction.
    public Direction XytoDirection(int x, int y)
    {
        Direction dir = Direction.Down;
        if (x == 0 && y == -1)
            dir = Direction.Up;
        else if (x == 0 && y == 1)
            dir = Direction.Down;
        else if (x == -1 && y == 0)
            dir = Direction.Left;
        else if (x == 1 && y == 0)
            dir = Direction.Right;
        return dir;
    }
}
