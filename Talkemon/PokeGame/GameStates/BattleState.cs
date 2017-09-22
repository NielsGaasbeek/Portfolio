using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

partial class BattleState : GameObjectList
{
    protected BattleInfoBox speler, npc;
    int playerhealth, playerLevel, exp, playerMaxhealth;
    int npcHealth, npcMaxhealth, npcLevel;
    bool isEnemyTurn;

    float playerTimer = 2000;
    float npcTimer = 4000;

    Pokemon opponentpkmn, currentpkmn, invpkmn;
    Pokemon Bulbasaur, Charmander, Pikachu;
    Effectiveness eff;

    Random r;


    public BattleState()
    {
        InitRecognizer();

        Bulbasaur = new Pokemon("Bulbasaur");
        Charmander = new Pokemon("Charmander");
        Pikachu = new Pokemon("Pikachu");

        r = new Random();

        eff = new Effectiveness();

        randomizer();


        speler = new BattleInfoBox(true, currentpkmn.name, currentpkmn.HP, currentpkmn.MaxHP, currentpkmn.lvl, exp);
        add(speler);


        npc = new BattleInfoBox(false, opponentpkmn.name, opponentpkmn.HP, opponentpkmn.MaxHP, opponentpkmn.lvl);
        add(npc);

    }

    public override void HandleInput(InputHelper inputHelper)
    {
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        playerTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        npcTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        if(playerTimer <= 0)
        {
            //npcHealth -= GameEnvironment.Random.Next(5, 50);
            speler.updateInfo(currentpkmn.HP, currentpkmn.MaxHP, currentpkmn.lvl, exp);
            playerTimer = 4000;
        }

        if(npcTimer <= 0)
        {
            //playerhealth -= GameEnvironment.Random.Next(5, 50);
            npc.updateInfo(opponentpkmn.HP, opponentpkmn.MaxHP, opponentpkmn.lvl);
            npcTimer = 4000;
        }

    }


    public void randomizer()
    {

        int n = r.Next(5);
        switch (n)
        {
            case 0:
                opponentpkmn = Bulbasaur;
                currentpkmn = Charmander;
                invpkmn = Pikachu;
                break;
            case 1:
                opponentpkmn = Bulbasaur;
                currentpkmn = Pikachu;
                invpkmn = Charmander;
                break;
            case 2:
                opponentpkmn = Charmander;
                currentpkmn = Bulbasaur;
                invpkmn = Pikachu;
                break;
            case 3:
                opponentpkmn = Charmander;
                currentpkmn = Pikachu;
                invpkmn = Bulbasaur;
                break;
            case 4:
                opponentpkmn = Pikachu;
                currentpkmn = Charmander;
                invpkmn = Bulbasaur;
                break;
            case 5:
                opponentpkmn = Pikachu;
                currentpkmn = Bulbasaur;
                invpkmn = Charmander;
                break;
        }
    }


    public void EnemyTurn()
    {
        isEnemyTurn = true;
        int i = r.Next(3);

        double effect = eff.calculateEffectiveness(currentpkmn.type, opponentpkmn.type, null);
        int dmg = opponentpkmn.DoMove(opponentpkmn.moveList[i], currentpkmn, effect);

        currentpkmn.HP -= (dmg / 5);
        isEnemyTurn = false;
        return;
    }

    public void Fight(Pokemon atPkmn, Pokemon dfPkmn, string move)
    {
        if (atPkmn.IsMove(move))
        {
            double effect = eff.calculateEffectiveness(atPkmn.type, dfPkmn.type, null);
            int dmg = 0;
    
            for (int i = 0; i < atPkmn.abilities.Length; i++)
            {
                if (move == atPkmn.abilities[i])
                {
                    Move temp =  atPkmn.moveList[i];
                    dmg = atPkmn.DoMove(temp, dfPkmn, effect);
                    break;
            }
        }

        dfPkmn.HP -= (dmg / 5);
        }

        EnemyTurn();

    }

    public void Item()
    {
        currentpkmn.HP = currentpkmn.MaxHP;

        EnemyTurn();
    }

    public void Switch()
    {
        Pokemon temp = currentpkmn;
        currentpkmn = invpkmn;
        invpkmn = temp;

        EnemyTurn();
    }

    public void Run()
    {
        if (currentpkmn.spd > opponentpkmn.spd)
        {
            GameEnvironment.GameStateManager.SwitchTo("MainMenu");
            recognizer.RecognizeAsyncCancel();
        }
    }
}