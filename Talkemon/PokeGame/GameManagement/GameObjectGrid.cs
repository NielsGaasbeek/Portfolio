using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameObjectGrid : GameObject
{
    protected GameObject[,] grid;
    protected int cellWidth, cellHeight;

    public GameObjectGrid(int rows, int columns, int layer = 0, string id = "")
        : base(layer, id)
    {
        grid = new GameObject[columns, rows];
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                grid[x, y] = null;
            }
        }
    }

    public void Add(GameObject obj, int x, int y)
    {
        grid[x, y] = obj;
        obj.Parent = this;
        obj.Position = new Vector2(x * cellWidth, y * cellHeight);
    }

    // De grid kan nu ook zoeken naar objecten. Bijna letterlijk een kopie van de GameObjectList.Find .
    public GameObject Find(string id)
    {
        foreach (GameObject obj in grid)
        {
            if (obj != null)
            {
                if (obj.Id == id)
                {
                    return obj;
                }
                if (obj is GameObjectList)
                {
                    GameObjectList objList = obj as GameObjectList;
                    GameObject subObj = objList.Find(id);
                    if (subObj != null)
                    {
                        return subObj;
                    }
                }
                if (obj is GameObjectGrid)
                {
                    GameObjectGrid objGrid = obj as GameObjectGrid;
                    GameObject subObj = objGrid.Find(id);
                    if (subObj != null)
                    {
                        return subObj;
                    }
                }
            }
        }
        return null;
    }

    public GameObject Get(int x, int y)
    {
        if (x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1))
        {
            return grid[x, y];
        }
        else
        {
            return null;
        }
    }

    public GameObject[,] Objects
    {
        get
        {
            return grid;
        }
    }

    public Vector2 GetAnchorPosition(GameObject s)
    {
        for (int x = 0; x < Columns; x++)
        {
            for (int y = 0; y < Rows; y++)
            {
                if (grid[x, y] == s)
                {
                    return new Vector2(x * cellWidth, y * cellHeight);
                }
            }
        }
        return Vector2.Zero;
    }


    // Een functie om objecten te bewegen binnen de grid. 
    // movex en movey zijn altijd -1, 0 of 1.
    public void Move(GameObject s, int movex, int movey)
    {
        // Als de x,y overeen komt met het gegeven object...
        for (int x = 0; x < Columns; x++)
        {
            for (int y = 0; y < Rows; y++)
            {
                if (grid[x, y] == s)
                {
                    // Dan wordt de target positie berekent.
                    int xnew = x + movex;
                    int ynew = y + movey;
                    // Als de target positie buiten de grid is, gat de beweging niet door.
                    // Dit zou in principe niet moeten kunnen. 
                    if (xnew < 0 || xnew >= Columns || ynew < 0 || ynew >= Rows)
                    {
                        return;
                    }
                    // De plek waar het object vandaan komt wordt leeggemaakt.
                    grid[x, y] = null;

                    // Het object wordt naar de target positie gezet. 
                    // Er wordt altijd een kopie gemaakt als een object beweegt, de oude versie gaat dood.
                    // Best zielig eigenlijk.
                    grid[xnew, ynew] = s;
                    return;
                }
            }
        }
    }

    public int Rows
    {
        get { return grid.GetLength(1); }
    }

    public int Columns
    {
        get { return grid.GetLength(0); }
    }

    public int CellWidth
    {
        get { return cellWidth; }
        set { cellWidth = value; }
    }

    public int CellHeight
    {
        get { return cellHeight; }
        set { cellHeight = value; }
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        foreach (GameObject obj in grid)
        {
            if (obj != null)
                obj.HandleInput(inputHelper);
        }
    }

    public override void Update(GameTime gameTime)
    {
        foreach (GameObject obj in grid)
        {
            if (obj != null)
                obj.Update(gameTime);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (GameObject obj in grid)
        {
            if (obj != null)
                obj.Draw(gameTime, spriteBatch);
        }
    }

    public override void Reset()
    {
        base.Reset();
        foreach (GameObject obj in grid)
        {
            obj.Reset();
        }
    }
}
