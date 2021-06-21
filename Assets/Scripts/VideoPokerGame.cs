using System.Collections;
using System.Collections.Generic;

using TEMP;
using UnityEngine;
using UnityEngine.UI;

public static class VideoPokerGame
{
    private static System.Random rand = new System.Random();
    private static Deck deck;
    
    private static Text text;

    public static Card[] Hands { get; private set; }
    public static bool[] IsHolds { get; private set; }


    public static void SetText(Text textUI)
    {
        text = textUI;
    }

    public static void Reset()
    {
        deck = new Deck(rand);
        Hands = new Card[5];
        IsHolds = new bool[5];
        deck.Shuffle();
        for (var i = 0; i < Hands.Length; i++)
        {
            Hands[i] = deck.Draw();
            IsHolds[i] = true;
        }
        var tempArray = new Card[5];
        System.Array.Copy(Hands, tempArray, Hands.Length);
        var checkHands = PKCheck.CheckHands(tempArray);
        if (text != null)
        {
            text.text = checkHands.ToString();
        }
    }

}
