using Microsoft.Xna.Framework;

class Camera : GameObject
{
    public Camera(int layer = 0, string id = "")
        : base(layer, id)
    {
    }

    // De Camera kijkt waar de speler is, en veranderd de positie.
    public void ChangePosition()
    {
        GameObjectGrid charactergrid = GameWorld.Find("charactergrid") as GameObjectGrid;
        Player player = charactergrid.Find("player") as Player;

        // Note: momenteel is het scherm niet goed ingesteld. 

        // De helft van het scherm is het maximale wat de camera mag bewegen. 
        float screenx = GameEnvironment.Screen.X / 2;
        float screeny = GameEnvironment.Screen.Y / 2;

        // De positie van de camera wordt berekend.
        position.X = player.Position.X - screenx;
        position.Y = player.Position.Y - screeny;

    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        // Elke tik van de update, update de camera de positie. 
        ChangePosition();
    }
}
