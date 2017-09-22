using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

partial class Level : GameObjectList
{
    OverworldGrid tiles;
    GameObjectGrid chars;

    public void LoadTiles(string path)
    {
        List<string> textLines = new List<string>();
        StreamReader fileReader = new StreamReader(path);
        string line = fileReader.ReadLine();
        int width = line.Length;
        while (line != null)
        {
            textLines.Add(line);
            line = fileReader.ReadLine();
        }
        tiles = new OverworldGrid(textLines.Count - 1, width, 1, "overworldgrid");

        tiles.CellWidth = 48;
        tiles.CellHeight = 48;
        add(tiles);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < textLines.Count - 1; y++)
            {
                Tile t = LoadTile(textLines[y][x], x, y);
                tiles.Add(t, x, y);
            }
        }
    }

    public void LoadCharacters(string path)
    {
        List<string> textLines = new List<string>();
        StreamReader fileReader = new StreamReader(path);
        string line = fileReader.ReadLine();
        int width = line.Length;
        while (line != null)
        {
            textLines.Add(line);
            line = fileReader.ReadLine();
        }
        chars = new GameObjectGrid(textLines.Count - 1, width, 2, "charactergrid");

        chars.CellWidth = 48;
        chars.CellHeight = 48;
        add(chars);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < textLines.Count - 1; y++)
            {
                Character c = LoadChar(textLines[y][x], x, y);
                if (c != null)
                    chars.Add(c, x, y);
            }
        }
    }

    public OverworldGrid GetLevel
    {
        get { return tiles; }
    }

    public GameObjectGrid Getchars
    {
        get { return chars; }
    }

    private Character LoadChar(char type, int x, int y)
    {
        switch (type)
        {
            case '.':
                return null;
            case 'b':
                return new Character(Vector2.Zero, "fisher");
            default:
                return null;
        }
    }

    private Tile LoadTile(char tileType, int x, int y)
    {
        switch (tileType)
        {
            case '.':
                return new Tile();
            case 'g':
                return LoadBasicTile("Tiles/grass.png", TileType.Normal);
            case 'w':
                return LoadBasicTile("Tiles/water.png", TileType.Water);
            case 'v':
                return LoadBasicTile("Tiles/water2.png", TileType.Water);
            case 'b':
                return LoadBasicTile("Tiles/wall.png", TileType.Identifier);
            case '#':
                return LoadBasicTile("", TileType.Wall);
            case 's':
                return LoadBasicTile("Tiles/stone.png", TileType.Normal);
            case 'c':
                return LoadBasicTile("Tiles/wallcorner.png", TileType.Identifier);
            case 'l':
                return LoadBasicTile("Tiles/wall2.png", TileType.Identifier);
            default:
                return new Tile("");
        }
    }

    private Tile LoadBasicTile(string name, TileType tileType, bool walkable = true)
    {
        Tile t = new Tile("" + name, tileType);
        t.Walkable = walkable;
        return t;
    }
}
