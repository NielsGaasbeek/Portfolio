using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

enum TileType
{
    Background,
    Normal,
    Wall,
    Water,
    Door,
    Identifier
}

class Tile : SpriteGameObject
{
    protected TileType type;
    protected bool walkable = false;

    public Tile(string assetname = "", TileType tp = TileType.Background, int layer = 0, string id = "")
        : base(assetname, layer, id)
    {
        type = tp;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (type == TileType.Background || type == TileType.Wall)
        {
            return;
        }
        base.Draw(gameTime, spriteBatch);
    }

    public TileType TileType
    {
        get { return type; }
    }

    public bool Walkable
    {
        get { return walkable; }
        set { walkable = value; }
    }
}
