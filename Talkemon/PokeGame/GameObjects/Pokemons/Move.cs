using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Move
{
    string moveName, moveType;
    public int acc, str;
    
    public Move(string moveStats)
    {
        string[] temp = moveStats.Split(',');
        moveName = temp[0];
        //moveType = temp[1];
        str = int.Parse(temp[1]);
        acc = int.Parse(temp[2]);
    }
}
