using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpriteSheet
{
    protected Texture2D sprite;
    protected int sheetIndex;
    protected int sheetColumns;
    protected int sheetRows;
    protected bool mirror;
    protected bool[] collisionMask;

    public SpriteSheet(string assetname, int sheetIndex = 0)
    {
        sprite = GameEnvironment.AssetManager.GetSprite(assetname);

        Color[] colorData = new Color[sprite.Width * sprite.Height];
        collisionMask = new bool[sprite.Width * sprite.Height];
        sprite.GetData(colorData);
        for (int i = 0; i < colorData.Length; i++)
            collisionMask[i] = colorData[i].A != 0;
        
        this.sheetIndex = sheetIndex;
        this.sheetColumns = 1;
        this.sheetRows = 1;

        string[] assetSplit = assetname.Split('@');
        if (assetSplit.Length <= 1)
            return;

        string sheetNrData = assetSplit[assetSplit.Length - 1];
        string[] colrow = sheetNrData.Split('x');
        this.sheetColumns = int.Parse(colrow[0]);
        if (colrow.Length == 2)
            this.sheetRows = int.Parse(colrow[1]);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 origin)
    {
        int columnIndex = sheetIndex % sheetColumns;
        int rowIndex = sheetIndex / sheetColumns;
        Rectangle spritePart = new Rectangle(columnIndex * this.Width, rowIndex * this.Height, this.Width, this.Height);
        SpriteEffects spriteEffects = SpriteEffects.None;
        if (mirror)
        {
            spriteEffects = SpriteEffects.FlipHorizontally;
        }
        spriteBatch.Draw(sprite, position, spritePart, Color.White, 0.0f, origin, 1.0f, spriteEffects, 0.0f);
    }

    public bool isTranslucent(int x, int y)
    {
        int column_index = sheetIndex % sheetColumns;
        int row_index = sheetIndex / sheetColumns % sheetRows;

        return collisionMask[column_index * Width + x + (row_index * Height + y) * sprite.Width];
    }

    public Texture2D Sprite
    {
        get { return sprite; }
    }

    public Vector2 Center
    {
        get { return new Vector2(Width, Height) / 2; }
    }

    public int Width
    {
        get { return sprite.Width / sheetColumns; }
    }

    public int Height
    {
        get { return sprite.Height / sheetRows; }
    }

    public int NumberOfSheetElements
    {
        get { return this.sheetColumns * this.sheetRows; }
    }

    public int SheetIndex
    {
        get { return this.SheetIndex; }
        set
        {
            if (value < NumberOfSheetElements && value >= 0)
                this.sheetIndex = value;
        }
    }

    public bool Mirror
    {
        get { return mirror; }
        set { mirror = value; }
    }

}