using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class Player : Character
{
    // Een timer om het lopen bij te houden. 
    protected double timer = 0;
    protected bool walking;
    Inventory inventory;

    public Player(Vector2 startposition, string assetname, int layer = 0, string id = "")
        : base(startposition, assetname, layer, id)
    {
        inventory = new Inventory();
        
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        // Als de timer boven de standaardwaarde is, mag de speler bewegen. De standaard is lager als de shift knop is ingedrukt. 
        if (timer > 500 || (timer > 250 && inputHelper.IsKeyDown(Keys.LeftShift)))
        {
            // Als een richting is gedrukt, wordt de timer gereset en de direction veranderd.
            // Ook wordt de player in de grid bewogen indien mogelijk.
            if (inputHelper.IsKeyDown(Keys.Up) || inputHelper.IsKeyDown(Keys.W))
            {
                timer = 0;
                walking = true;
                facing = Direction.Up;
                Move(facing);
            }
            else if (inputHelper.IsKeyDown(Keys.Down) || inputHelper.IsKeyDown(Keys.S))
            {
                timer = 0;
                walking = true;
                facing = Direction.Down;
                PlayAnimation("walkdown");
                Move(facing);
            }
            else if (inputHelper.IsKeyDown(Keys.Left) || inputHelper.IsKeyDown(Keys.A))
            {
                timer = 0;
                walking = true;
                facing = Direction.Left;
                Move(facing);
            }
            else if (inputHelper.IsKeyDown(Keys.Right) || inputHelper.IsKeyDown(Keys.D))
            {
                timer = 0;
                walking = true;
                facing = Direction.Right;
                Move(facing);
            }

            else
            {
                walking = false;
            }

        }

        // Als er op E wordt gedrukt, komt er interactie als er iets naast de player staat.
        if (inputHelper.KeyPressed(Keys.E))
        {
            Interact(facing);
        }

    }



    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        // De timer gaat omhoog met de actuele tijd.
        timer += gameTime.ElapsedGameTime.TotalMilliseconds;

        // Er wordt gekeken naar de grootte van de grid. 2x gameworld, want hij moet de grid zelf hebben, niet iets in de grid.
        GameObjectGrid charactergrid = GameWorld.GameWorld.Find("charactergrid") as GameObjectGrid;

        // Er wordt een beweging gemaakt tussen de current positie en de target positie. 
        Position = Vector2.Lerp(Position, charactergrid.GetAnchorPosition(this), 0.1f);

        if (!walking)
        {
            if (facing == Direction.Up)
                PlayAnimation("idleup");
            else if (facing == Direction.Down)
                PlayAnimation("idledown");
            else if (facing == Direction.Left)
                PlayAnimation("idleleft");
            else if (facing == Direction.Right)
                PlayAnimation("idleright");
        }

    }

    public void Move(Direction dir)
    {

        // De direction wordt omgezet in nummers.
        int x = Directiontox(dir);
        int y = Directiontoy(dir);

        // Beide grids moeten worden opgehaald.
        GameObjectGrid charactergrid = GameWorld.GameWorld.Find("charactergrid") as GameObjectGrid;
        OverworldGrid overworldgrid = GameWorld.GameWorld.Find("overworldgrid") as OverworldGrid;

        // De eigen positie wordt naar een vector2 omgezet. Deze geeft aan welk blok de player zich bevindt.
        Vector2 gridpos = new Vector2(Position.X / overworldgrid.CellWidth, Position.Y / overworldgrid.CellHeight);

        // Met deze gegevens wordt er gekeken naar welk blok de player zich wil bewegen.
        int gridx = (int)Math.Round(gridpos.X + x);
        int gridy = (int)Math.Round(gridpos.Y + y);

        // Er wordt gekeken of de player niet tegen een muur, de waterkant of een character loopt
        if (overworldgrid.GetTileType(gridx, gridy) != TileType.Wall && overworldgrid.GetTileType(gridx, gridy) != TileType.Water
            && overworldgrid.GetTileType(gridx, gridy) != TileType.Identifier && charactergrid.Get(gridx, gridy) == null)
        {
            // Als dit niet het geval is mag de player bewegen. Dit wordt gedaan in de grid. 
            charactergrid.Move(this, x, y);
        }
    }

    public void Interact(Direction dir)
    {
        // De direction wordt omgezet in nummers.
        int x = Directiontox(dir);
        int y = Directiontoy(dir);

        // Beide grids moeten worden opgehaald.
        GameObjectGrid charactergrid = GameWorld.GameWorld.Find("charactergrid") as GameObjectGrid;
        OverworldGrid overworldgrid = GameWorld.GameWorld.Find("overworldgrid") as OverworldGrid;

        // De eigen positie wordt naar een vector2 omgezet. Deze geeft aan welk blok de player zich bevindt.
        Vector2 gridpos = new Vector2(Position.X / overworldgrid.CellWidth, Position.Y / overworldgrid.CellHeight);

        // Met deze gegevens wordt er gekeken met welk blok de player wil interacten.
        int gridx = (int)Math.Round(gridpos.X + x);
        int gridy = (int)Math.Round(gridpos.Y + y);

        // Als er iets staat op die positie...
        if (charactergrid.Get(gridx, gridy) != null)
        {
            // Als het een Character is...
            Character chara = charactergrid.Get(gridx, gridy) as Character;
            // Wordt het character omgedraait.
            // TO DO: Niet met signs/computers/andere objecten.
            chara.Facing = XytoDirection(-x, -y);
            
            // TO DO: Dialogue / andere interactie.
        }
    }
}
