using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Inventory
{
    IList<Item> inventory;
    IList<Pokemon> pokemons;

    public Inventory()
    {
        inventory = new List<Item>();

        
        StreamReader reader = new StreamReader("Content/Player/Inventory.txt");
        string inv = reader.ReadLine();
        string[] temp = inv.Split(';');
        LoadItems(temp);


    }

    private void LoadItems(string[] allItems)
    {
        for (int i = 0; i < allItems.Length; i++)
        {
            Item item = new Item(allItems[i]);
            inventory.Add(item);
        }
    }

    public IList<Item> Inv
    {
        get { return inventory; }
    }

}