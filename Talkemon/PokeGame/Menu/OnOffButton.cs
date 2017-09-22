using Microsoft.Xna.Framework;

class OnOffButton : SpriteGameObject
{
    public OnOffButton(string imageAsset, int layer = 0, string id = "") : base(imageAsset, layer, id, 0)
    {
        
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        if (inputHelper.MouseLeftButtonPressed() && BoundingBox.Contains((int)inputHelper.MousePosition.X, (int)inputHelper.MousePosition.Y))
            sprite.SheetIndex = 1 - sprite.SheetIndex;
    }

    public bool On
    {
        get { return sprite.SheetIndex == 1; }
        set
        {
            if (value)
                sprite.SheetIndex = 1;
            else
                sprite.SheetIndex = 0;
        }

    }

}