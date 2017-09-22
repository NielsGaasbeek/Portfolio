using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

class BattleInfoBox : GameObjectList
{
    protected int currentHealth, maxHealth, level, exp;
    protected float waarde;
    protected TextGameObject health, pkmnLevel;
    protected SpriteGameObject background, healthBar;
    protected Color healthBarKleur;

    public BattleInfoBox(bool player, string pkmnName, int currentHealth, int maxHealth, int level, int exp = 0)
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
        this.level = level;
        this.exp = exp;
        waarde = 200;
        healthBarKleur = Color.Green;

        //speler heeft een iets grotere sprite dan de npc
        if(player)
            background = new SpriteGameObject("Battle/infoBoxPlayer", 1);
        else
            background = new SpriteGameObject("Battle/infoBoxOpponent", 1);

        //bepaal of het de pokemon van de speler of npc is, en daarop de positie aanpassen
        if (player) 
            background.Position = new Vector2(GameEnvironment.Screen.X - background.Width - 50, GameEnvironment.Screen.Y - background.Height - 50);
        else
            background.Position = new Vector2(50, 50);
        add(background);

        //plaats de naam van de pokemon
        TextGameObject name = new TextGameObject("Fonts/hud", 2);
        name.Text = pkmnName;
        name.Color = Color.Black;
        name.Position = new Vector2(background.Position.X + 5, background.Position.Y + 5);
        add(name);

        //plaats alleen bij de speler de huidige en maximale health van de pokemon.
        health = new TextGameObject("Fonts/hud", 2);
        health.Text = (currentHealth.ToString() + "/" + maxHealth.ToString());
        health.Color = Color.Black;
        health.Position = new Vector2(background.Position.X + (background.Width / 2) + 10, background.Position.Y + (background.Height / 2) + 10);
        if (!player)
            health.Visible = false;
        add(health);

        //health balk toevoegen, positie verschilt omdat de npc een kleinere sprite heeft
        healthBar = new SpriteGameObject("Battle/healthBar.png", 2, "healthBar");
        if (player)
            healthBar.Position = new Vector2(background.Position.X + background.Width - healthBar.Width - 6,
            background.Position.Y + (background.Height / 2) - (healthBar.Height / 2));
        else
            healthBar.Position = new Vector2(background.Position.X + background.Width - healthBar.Width - 6, 
                background.Position.Y + background.Height / 2 + 6);

        //plaats het level van de pokemon
        pkmnLevel = new TextGameObject("Fonts/hud", 2);
        pkmnLevel.Text = "Lv" + level.ToString();
        pkmnLevel.Color = Color.Black;
        pkmnLevel.Position = new Vector2(background.Position.X + background.Width - 90 , name.Position.Y);
        add(pkmnLevel);

        //bij de pokemon van de speler ook een exp balk toevoegen
        if (player)
        {
            SpriteGameObject expBar = new SpriteGameObject("Battle/expBar", 2);
            /*........................
            ..........WIP.............
            ........................*/
        }


    }

    public void updateInfo(int currentHealth, int maxHealth, int level, int exp = 0)
    {
        //update health en healthbar
        health.Text = (currentHealth.ToString() + "/" + maxHealth.ToString());
        waarde = 200 * ((float)currentHealth / (float)maxHealth);

        //update level in geval van level-up
        pkmnLevel.Text = "Lv" + level.ToString();

        //update exp
        this.exp = exp;

    }


    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        base.Draw(gameTime, spriteBatch);

        //bepaal de kleur van de healthbar. <50% oranje, <20% rood
        if (waarde >= 100)
            healthBarKleur = Color.Green;
        if (waarde >= 40 && waarde < 100)
            healthBarKleur = Color.Orange;
        if (waarde < 40)
            healthBarKleur = Color.Red;

        spriteBatch.Draw(healthBar.Sprite.Sprite, healthBar.Position, new Rectangle(0, 0, (int)waarde, healthBar.Height), healthBarKleur);
    }

}