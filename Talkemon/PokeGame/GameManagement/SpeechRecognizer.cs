using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.IO;

partial class BattleState // SpeechRecognizer
{
    SpeechRecognitionEngine recognizer;
    bool isSwitch;

    private void InitRecognizer()
    {        
        recognizer = new SpeechRecognitionEngine();         //makes a new recognizer to be used for the speech recognition
        recognizer.SetInputToDefaultAudioDevice();          //makes sure the default mic is used for input to the recognizer

        //initialize all grammars here
        StreamReader reader = new StreamReader("Content/Pokemons/AllActions.txt");

        //make the basicmove grammar

        Choices basicmoves = new Choices();
        basicmoves.Add(reader.ReadLine().Split(';'));

        GrammarBuilder gbBasicmoves = new GrammarBuilder();
        gbBasicmoves.Append(basicmoves);

        Grammar gBasicmoves = new Grammar(gbBasicmoves);
        gBasicmoves.SpeechRecognized += GBasicmoves_SpeechRecognized;
        recognizer.LoadGrammar(gBasicmoves);

        //make the pokemon grammar

        Choices pokemons = new Choices();
        pokemons.Add(reader.ReadLine().Split(';'));

        GrammarBuilder gbPokemon = new GrammarBuilder();
        gbPokemon.Append(pokemons);

        Grammar gPokemon = new Grammar(gbPokemon);
        gPokemon.SpeechRecognized += GPokemon_SpeechRecognized;
        recognizer.LoadGrammar(gPokemon);

        //make the move grammar

        Choices moves = new Choices();
        moves.Add(reader.ReadLine().Split(';'));

        GrammarBuilder gbMoves = new GrammarBuilder();
        gbMoves.Append(moves);

        Grammar gMoves = new Grammar(gbMoves);
        gMoves.SpeechRecognized += GMoves_SpeechRecognized;
        recognizer.LoadGrammar(gMoves);

        //make the item grammar

        Choices items = new Choices();
        items.Add(reader.ReadLine().Split(';'));

        GrammarBuilder gbItems = new GrammarBuilder();
        gbItems.Append(items);

        Grammar gItems = new Grammar(gbItems);
        gItems.SpeechRecognized += GItems_SpeechRecognized;
        recognizer.LoadGrammar(gItems);

        recognizer.RecognizeAsync(RecognizeMode.Multiple);
    }

    private void GBasicmoves_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {
        if (!isEnemyTurn)
        {
                switch (e.Result.Text)
                {
                    case "fight":
                    case "attack":
                        //initialize the menu with fighting options
                        return;
        
                    case "do":
                        //message saying :"choose move"
                        return;
        
                    case "use":
                        //message saying: "choose move or item"
                        return;
        
                    case "item":
                        Item();
                        return;
        
                    case "switch":
                        Switch();
                        isSwitch = true;
                        //message saying: "choose pokemon to switch with"
                        return;
        
                    case "run":
                        return;
        
                    default:
                        return;

                }
        }



    }

    private void GItems_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {
        if (!isEnemyTurn)
        {
            switch (e.Result.Text)
            {
                case "potion":
                    
                    return;
                default:
                    return;
            
            }
        }


    }

    private void GPokemon_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {
        if (!isEnemyTurn)
        {
            if (isSwitch)
            {
                isSwitch = false;
                return;
            }
            return;
        }

        return;
    }

    private void GMoves_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {
        if (!isEnemyTurn)
        {
            Fight(currentpkmn, opponentpkmn, e.Result.Text);
            return;
        }

        return;
    }
}
