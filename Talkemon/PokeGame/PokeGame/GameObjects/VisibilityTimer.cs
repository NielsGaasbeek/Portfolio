using System;
using Microsoft.Xna.Framework;

class VisibilityTimer : GameObject
{
    protected GameObject target;
    protected float timeleft;
    protected float totalTime;

    public VisibilityTimer(GameObject target, int layer = 0, string id = "") : base(layer, id)
    {
        totalTime = 5;
        timeleft = 0;
        this.target = target;
    }

    public override void Update(GameTime gameTime)
    {
        timeleft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (timeleft <= 0)
            target.Visible = false;

    }

    public void startVisible()
    {
        timeleft = totalTime;
        target.Visible = true;
    }

    public override void Reset()
    {
        base.Reset();
        timeleft = 0;
    }

}