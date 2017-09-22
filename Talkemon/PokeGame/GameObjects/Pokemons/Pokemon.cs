using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Pokemon
{
    public IList<Move> moveList;
    public string name, type;
    public int hp, atk, def, spatk, spdef, spd, lvl, maxHP;
    public string[] abilities;
    string[] info;

    public Pokemon(string name)
    {
        moveList = new List<Move>();
        this.name = name;

        lvl = 10;
        GetInfo();
    }

    private void GetInfo()
    {
        StreamReader reader = new StreamReader("Content/Pokemons/" + name + ".txt");
        string[] temp = reader.ReadLine().Split(':');

        info = temp[0].Split(';');
        type = info[1];
        hp = int.Parse(info[2]);
        maxHP = hp;
        atk = int.Parse(info[3]);
        def = int.Parse(info[4]);
        spatk = int.Parse(info[5]);
        spdef = int.Parse(info[6]);
        spd = int.Parse(info[7]);


        abilities = temp[1].Split(';');
        for (int i = 0; i < abilities.Length; i++)
        {
            Move move = new Move(abilities[i]);
            moveList.Add(move);
            temp = abilities[i].Split(',');
            abilities[i] = temp[0];
        }

    }

    public int DoMove(Move move, Pokemon dfPkmn, double effect)
    {
        double dmg = ((((2 * lvl) + 10) / 250) + (this.atk / dfPkmn.def) * move.str + 2) * effect;

        return (int)dmg;
    }

    public bool IsMove(string move)
    {
        for (int i = 0; i < abilities.Length; i++)
            if (move == abilities[i])
                return true;
        return false;
    }

    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }

    public int MaxHP
    {
        get { return maxHP; }
        set { maxHP = value; }
    }
}
