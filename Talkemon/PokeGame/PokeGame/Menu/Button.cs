using System;
using Microsoft.Xna.Framework;

public class Button : SpriteGameObject
{
    protected bool pressed;

    public Button(string imageAsset, int layer = 0, string id = "") : base(imageAsset, layer, id)
    {
        pressed = false;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        Rectangle rect = new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, sprite.Width, sprite.Height);

        pressed = inputHelper.MouseLeftButtonPressed() && rect.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y);
    }

    public bool Pressed
    {
        get { return pressed; }
    }
}