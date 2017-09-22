using Microsoft.Xna.Framework;

class Slider : GameObjectList
{
    protected int leftMargin, rightMargin;
    protected bool dragging;
    protected SpriteGameObject back, front;

    public Slider(string sliderBack, string sliderFront, int layer = 0, string id ="") : base(layer, id)
    {
        back = new SpriteGameObject(sliderBack, 0);
        this.add(back);

        front = new SpriteGameObject(sliderFront, 1);
        front.Position = new Vector2(leftMargin, 11);
        this.add(front);

        leftMargin = 10;
        rightMargin = 10;
        dragging = false;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if(!inputHelper.MouseLeftButtonDown())
        {
            dragging = false;
            return;
        }
        if(back.BoundingBox.Contains(inputHelper.MousePosition) || dragging)
        {
            float newXPos = MathHelper.Clamp(inputHelper.MousePosition.X - back.GlobalPosition.X - front.Width / 2,
                back.Position.X + leftMargin, back.Position.X + back.Width - front.Width - rightMargin);
            front.Position = new Vector2(newXPos, front.Position.Y);
            dragging = true;
        }
    }

    public float Value //waarde tussen 0 en 1 met 0 = helemaal uit, 1 = helmaal aan.
    {
        get { return (front.Position.X - back.Position.X - leftMargin) / (back.Width - leftMargin - rightMargin - front.Width); }
        set
        {
            float newXPos = value * (back.Width - leftMargin - rightMargin - front.Width) + back.Position.X + leftMargin;
            front.Position = new Vector2(newXPos, front.Position.Y);
        }
    }

}