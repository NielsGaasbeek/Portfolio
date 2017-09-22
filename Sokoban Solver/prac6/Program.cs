using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


class Prac6
{
    static void Main(string[] args)
    {
        Prac6 p = new Prac6();
        p.SokoBan();
    }
    StreamReader r;

    uint end, beginState;
    string outputMode;
    uint[,] room;

    Queue<uint> Q;

    Dictionary<uint, uint> prevStates;

    public void SokoBan()
    {
        
        string[] input = Console.ReadLine().Split(' ');
        
        /*
        r = new StreamReader("/.../Uni/Datastructuren/Practica/prac6/prac6/prac6/input.txt");
        string[] input = r.ReadLine().Split(' ');
        */

        room = new uint[uint.Parse(input[0]), uint.Parse(input[1])];
        outputMode = input[2];

        Q = new Queue<uint>();
        prevStates = new Dictionary<uint, uint>();

        for (uint y = 0; y < uint.Parse(input[1]); y++)
        {
            char[] mapRow = Console.ReadLine().ToCharArray();

            for (uint x = 0; x < mapRow.Length; x++)
            {
                switch (mapRow[x])
                {
                    case '+':
                        beginState += (x << 24) + (y << 16); break;
                    case '!':
                        beginState += (x << 8) + (y); break;
                    case '?':
                        end = (x << 8) + (y); break;
                    case '.':
                        room[x, y] = 0; break;
                    default:
                        room[x, y] = 1; break;
                }
            }
        }

        Search();
    }

    IList<uint> CalcStates(uint state)
    {
        IList<uint> result = new List<uint>();
        // enqueue four different states for all the possible movements
        uint xkoen = (state >> 24) ;
        uint ykoen = ((state >> 16) % 256) ;
        uint xfridge = ((state >> 8) % 256) ;
        uint yfridge = (state % 256) ;

        uint newXKoen, newYKoen, newXFridge, newYFridge;

        //up
        if (room[xkoen, ykoen - 1] == 0)
        {
            newXFridge = xfridge;
            newYFridge = yfridge;
            newXKoen = xkoen;
            newYKoen = ykoen;

            if (yfridge + 1 == ykoen && xfridge == xkoen)
            {
                if (room[xfridge, yfridge - 1] == 0)
                {
                    newYFridge = yfridge - 1;
                }
            }
            newYKoen = ykoen - 1;

            if (!(newXKoen == newXFridge && newYKoen == newYFridge))
            {
                uint res = uintState(newXKoen, newYKoen, newXFridge, newYFridge);
                if (!prevStates.ContainsKey(res))
                {
                    result.Add(res);
                    prevStates.Add(res, state);
                }
            }
        }

        //right
        if (room[xkoen + 1, ykoen] == 0)
        {
            newXFridge = xfridge;
            newYFridge = yfridge;
            newXKoen = xkoen;
            newYKoen = ykoen;

            if (xfridge - 1 == xkoen && yfridge == ykoen)
            {
                if (room[xfridge + 1, yfridge] == 0)
                {
                    newXFridge = xfridge + 1;
                }
            }
            newXKoen = xkoen + 1;

            if (!(newXKoen == newXFridge && newYKoen == newYFridge))
            {
                uint res = uintState(newXKoen, newYKoen, newXFridge, newYFridge);
                if (!prevStates.ContainsKey(res))
                {
                    result.Add(res);
                    prevStates.Add(res, state);
                }
            }
        }

        //down
        if (room[xkoen, ykoen + 1] == 0)
        {
            newXFridge = xfridge;
            newYFridge = yfridge;
            newXKoen = xkoen;
            newYKoen = ykoen;

            if (yfridge - 1 == ykoen && xfridge == xkoen)
            {
                if (room[xfridge, yfridge + 1] == 0)
                {
                    newYFridge = yfridge + 1;
                }
            }

            newYKoen = ykoen + 1;

            if (!(newXKoen == newXFridge && newYKoen == newYFridge))
            {
                uint res = uintState(newXKoen, newYKoen, newXFridge, newYFridge);
                if (!prevStates.ContainsKey(res))
                {
                    result.Add(res);
                    prevStates.Add(res, state);
                }
            }
        }

        //left
        if (room[xkoen - 1, ykoen] == 0)
        {
            newXFridge = xfridge;
            newYFridge = yfridge;
            newXKoen = xkoen;
            newYKoen = ykoen;

            if (xfridge + 1 == xkoen && yfridge == ykoen)
            {
                if (room[xfridge - 1, yfridge] == 0)
                {
                    newXFridge = xfridge - 1;
                }
            }
            newXKoen = xkoen - 1;

            if (!(newXKoen == newXFridge && newYKoen == newYFridge))
            {
                uint res = uintState(newXKoen, newYKoen, newXFridge, newYFridge);
                if (!prevStates.ContainsKey(res))
                {
                    result.Add(res);
                    prevStates.Add(res, state);
                }
            }
        }

        return result;
    }

    uint uintState(uint xkoen, uint ykoen, uint xfridge, uint yfridge)
    {   return ((xkoen << 24) + (ykoen << 16) + (xfridge << 8) + yfridge); }

    void Search()
    {
        prevStates.Add(beginState, beginState);
        Q.Enqueue(beginState);

        while (Q.Count > 0)
        {
            uint u = Q.Dequeue();
            IList<uint> stateList = CalcStates(u);
            foreach (uint v in stateList)
            {
                if (v % (1 << 16) == end)
                {
                    //endpouint is reached!
                    Output(v); return;
                }
                Q.Enqueue(v);
            }            
        }
        Console.WriteLine("No solution");
    }

    void Output(uint endState)
    {
        uint counter = 0;
        string path = "";
        bool genPath = (outputMode == "P");
        uint lastState = endState;

        while (lastState != beginState)
        {
            if (genPath)
            {
                uint to = lastState;
                uint from = prevStates[lastState];
                string move = "";

                uint xkoen = to >> 24;
                uint ykoen = (to >> 16) % 256;

                uint prevxkoen = from >> 24;
                uint prevykoen = (from >> 16) % 256;

                if (prevxkoen + 1 == xkoen)
                    move = "E";
                else if (prevxkoen - 1 == xkoen)
                    move = "W";
                else if (prevykoen + 1 == ykoen)
                    move = "S";
                else if (prevykoen - 1 == ykoen)
                    move = "N";

                    path = move + path;
            }

            lastState = prevStates[lastState];
            counter++;
        }


        Console.WriteLine(counter);
        if (genPath)
            Console.WriteLine(path);
    }
}

