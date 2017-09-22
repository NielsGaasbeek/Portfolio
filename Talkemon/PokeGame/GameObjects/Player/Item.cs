using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Item
{
    string name, type;
    int effect;

    public Item(string stats)
    {
        GetStats(stats);

    }

    private void GetStats(string stats)
    {
        string[] allStats = stats.Split(',');
        name = allStats[0];
        type = allStats[1];
        effect = int.Parse(allStats[2]);
    }

    public void UseItem(Pokemon target)
    {
        if (type == "hp")
        {
            target.HP += effect;
            return;
        }

        else if (type == "ball")
        {
            return;
        }

        else
        {
            return;
        }
    }
}
