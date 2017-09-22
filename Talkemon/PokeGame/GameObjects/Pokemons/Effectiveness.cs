using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Effectiveness
{
    private double[,] effectivenessGrid;
    private List<string> effectivenessList;
    public double effectivenessModifier;
    private int width = 18, height = 18;


    public Effectiveness()
    {
        EffectivenessGrid();
        EffectivenessList();
    }

    //deze methode 1 keer doen om de grid en de list te vormen 
    //(zonder deze methode moeten die andere 2 methoden public zijn, wat niet mooi is)

    //geef type van aanval en type(s) van de pokemon waar je de aanval op doet en berekent dan de modifier van super effective/resistance
    public double calculateEffectiveness(string attackType, string defenderType1, string defenderType2)
    {
        int x1, x2, y;
        double modifier1, modifier2 = 1;
        //search index van attackType in list = y
        y = effectivenessList.FindIndex(x => x.StartsWith(attackType));
        //search index van defenderType1 in list = x1
        x1 = effectivenessList.FindIndex(x => x.StartsWith(defenderType1));
        modifier1 = effectivenessGrid[x1, y];
        if (defenderType2 != null)
        {
            //search index van defenderType2 in list = x2
            x2 = effectivenessList.FindIndex(x => x.StartsWith(defenderType2));
            modifier2 = effectivenessGrid[x2, y];
        }
        effectivenessModifier = modifier1 * modifier2;
        return effectivenessModifier;
    }

    //hier onder worden het grid en de lijst gevuld

    private void EffectivenessGrid()
    {
        effectivenessGrid = new double[width, height];
        //fill grid with 1
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                effectivenessGrid[x, y] = 1;
        /*fill grid with effectiveness
         * 0 = normal
         * 1 = fire
         * 2 = water
         * 3 = electric
         * 4 = grass
         * 5 = ice
         * 6 = fighting
         * 7 = poison
         * 8 = ground
         * 9 = flying
         * 10 = psychic
         * 11 = bug
         * 12 = rock
         * 13 = ghost
         * 14 = dragon
         * 15 = dark
         * 16 = steel
         * 17 = fairy
         * 
         * [defending type, attack type]
         */
        effectivenessGrid[12, 0] = 0.5;
        effectivenessGrid[13, 0] = 0;
        effectivenessGrid[16, 0] = 0.5;
        effectivenessGrid[1, 1] = 0.5;
        effectivenessGrid[2, 1] = 0.5;
        effectivenessGrid[4, 1] = 2;
        effectivenessGrid[5, 1] = 2;
        effectivenessGrid[11, 1] = 2;
        effectivenessGrid[12, 1] = 0.5;
        effectivenessGrid[14, 1] = 0.5;
        effectivenessGrid[16, 1] = 0.5;
        effectivenessGrid[1, 2] = 2;
        effectivenessGrid[2, 2] = 0.5;
        effectivenessGrid[4, 2] = 0.5;
        effectivenessGrid[8, 2] = 2;
        effectivenessGrid[12, 2] = 2;
        effectivenessGrid[14, 2] = 0.5;
        effectivenessGrid[2, 3] = 2;
        effectivenessGrid[3, 3] = 0.5;
        effectivenessGrid[4, 3] = 0.5;
        effectivenessGrid[8, 3] = 0;
        effectivenessGrid[9, 3] = 2;
        effectivenessGrid[14, 3] = 0.5;
        effectivenessGrid[1, 4] = 0.5;
        effectivenessGrid[2, 4] = 2;
        effectivenessGrid[4, 4] = 0.5;
        effectivenessGrid[7, 4] = 0.5;
        effectivenessGrid[8, 4] = 2;
        effectivenessGrid[9, 4] = 0.5;
        effectivenessGrid[11, 4] = 0.5;
        effectivenessGrid[12, 4] = 2;
        effectivenessGrid[14, 4] = 0.5;
        effectivenessGrid[16, 4] = 0.5;
        effectivenessGrid[1, 5] = 0.5;
        effectivenessGrid[2, 5] = 0.5;
        effectivenessGrid[4, 5] = 2;
        effectivenessGrid[5, 5] = 0.5;
        effectivenessGrid[8, 5] = 2;
        effectivenessGrid[9, 5] = 2;
        effectivenessGrid[14, 5] = 0.5;
        effectivenessGrid[16, 5] = 0.5;
        effectivenessGrid[0, 6] = 2;
        effectivenessGrid[5, 6] = 2;
        effectivenessGrid[7, 6] = 0.5;
        effectivenessGrid[9, 6] = 0.5;
        effectivenessGrid[10, 6] = 0.5;
        effectivenessGrid[11, 6] = 0.5;
        effectivenessGrid[12, 6] = 2;
        effectivenessGrid[13, 6] = 0;
        effectivenessGrid[15, 6] = 2;
        effectivenessGrid[16, 6] = 2;
        effectivenessGrid[17, 6] = 0.5;
        effectivenessGrid[4, 7] = 2;
        effectivenessGrid[7, 7] = 0.5;
        effectivenessGrid[8, 7] = 0.5;
        effectivenessGrid[12, 7] = 0.5;
        effectivenessGrid[13, 7] = 0.5;
        effectivenessGrid[16, 7] = 0;
        effectivenessGrid[17, 7] = 2;
        effectivenessGrid[1, 8] = 2;
        effectivenessGrid[3, 8] = 2;
        effectivenessGrid[4, 8] = 0.5;
        effectivenessGrid[7, 8] = 2;
        effectivenessGrid[9, 8] = 0;
        effectivenessGrid[11, 8] = 0.5;
        effectivenessGrid[12, 8] = 2;
        effectivenessGrid[16, 8] = 2;
        effectivenessGrid[3, 9] = 0.5;
        effectivenessGrid[4, 9] = 2;
        effectivenessGrid[6, 9] = 2;
        effectivenessGrid[11, 9] = 2;
        effectivenessGrid[12, 9] = 0.5;
        effectivenessGrid[16, 9] = 0.5;
        effectivenessGrid[6, 10] = 2;
        effectivenessGrid[7, 10] = 2;
        effectivenessGrid[10, 10] = 0.5;
        effectivenessGrid[15, 10] = 0;
        effectivenessGrid[16, 10] = 0.5;
        effectivenessGrid[1, 11] = 0.5;
        effectivenessGrid[4, 11] = 2;
        effectivenessGrid[6, 11] = 0.5;
        effectivenessGrid[7, 11] = 0.5;
        effectivenessGrid[9, 11] = 0.5;
        effectivenessGrid[10, 11] = 2;
        effectivenessGrid[13, 11] = 0.5;
        effectivenessGrid[15, 11] = 2;
        effectivenessGrid[16, 11] = 0.5;
        effectivenessGrid[17, 11] = 0.5;
        effectivenessGrid[1, 12] = 2;
        effectivenessGrid[5, 12] = 2;
        effectivenessGrid[6, 12] = 0.5;
        effectivenessGrid[8, 12] = 0.5;
        effectivenessGrid[9, 12] = 2;
        effectivenessGrid[11, 12] = 2;
        effectivenessGrid[16, 12] = 0.5;
        effectivenessGrid[0, 13] = 0;
        effectivenessGrid[10, 13] = 2;
        effectivenessGrid[13, 13] = 2;
        effectivenessGrid[15, 13] = 0.5;
        effectivenessGrid[14, 14] = 2;
        effectivenessGrid[16, 14] = 0.5;
        effectivenessGrid[17, 14] = 0;
        effectivenessGrid[6, 15] = 0.5;
        effectivenessGrid[10, 15] = 2;
        effectivenessGrid[13, 15] = 2;
        effectivenessGrid[15, 15] = 0.5;
        effectivenessGrid[17, 15] = 0.5;
        effectivenessGrid[1, 16] = 0.5;
        effectivenessGrid[2, 16] = 0.5;
        effectivenessGrid[3, 16] = 0.5;
        effectivenessGrid[5, 16] = 2;
        effectivenessGrid[12, 16] = 2;
        effectivenessGrid[16, 16] = 0.5;
        effectivenessGrid[17, 16] = 2;
        effectivenessGrid[1, 17] = 0.5;
        effectivenessGrid[6, 17] = 2;
        effectivenessGrid[14, 17] = 2;
        effectivenessGrid[15, 17] = 2;
        effectivenessGrid[16, 17] = 0.5;
    }

    private void EffectivenessList()
    {
        effectivenessList = new List<string>();
        //fill list with all types
        effectivenessList.Add("Normal");
        effectivenessList.Add("Fire");
        effectivenessList.Add("Water");
        effectivenessList.Add("Electric");
        effectivenessList.Add("Grass");
        effectivenessList.Add("Ice");
        effectivenessList.Add("Fighting");
        effectivenessList.Add("Poison");
        effectivenessList.Add("Ground");
        effectivenessList.Add("Flying");
        effectivenessList.Add("Psychic");
        effectivenessList.Add("Bug");
        effectivenessList.Add("Rock");
        effectivenessList.Add("Ghost");
        effectivenessList.Add("Dragon");
        effectivenessList.Add("Dark");
        effectivenessList.Add("Steel");
        effectivenessList.Add("Fairy");
    }
}
