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
    private static List<CardMouseEvent> cardEventList = new List<CardMouseEvent>();

    public static Card[] Hands { get; private set; }
    public static bool[] IsHolds { get; private set; }

    public static int Mode { get; private set; }


    public static void SetText(Text textUI)
    {
        text = textUI;
    }

    public static void SetCardEvent(CardMouseEvent cme) 
    {
        Debug.Log(string.Format("SetCardEvent {0}", cme));
        cardEventList.Add(cme);
    }

    public static void Reset()
    {
        Mode = 0;
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
        for (var i = 0; i < Hands.Length; i++)
        {
            if (i < cardEventList.Count)
            {
                cardEventList[i].SetCard(Hands[i]);
                cardEventList[i].SetFace(true);
            }
        }

        for (var i = 0; i < Hands.Length; i++)
        {
            Debug.Log(string.Format("Hand{0}:{1}", i, Hands[i].ToString()));
        }

        Mode = 1;
    }

    public static void Change()
    {
        for (var i = 0; i < Hands.Length; i++)
        {
            if (i < cardEventList.Count)
            {
                Debug.Log(string.Format("Hand{0} isFace:{1}", i, cardEventList[i].IsFace));
                IsHolds[i] = cardEventList[i].IsFace;
            }
        }

        for (var i = 0; i < Hands.Length; i++)
        {
            if (!IsHolds[i])
            {
                Hands[i] = deck.Draw();
            }
        }
        var tempArray = new Card[5];
        System.Array.Copy(Hands, tempArray, Hands.Length);
        var checkHands = PKCheck.CheckHands(tempArray);
        if (text != null)
        {
            text.text = checkHands.ToString();
        }
        for (var i = 0; i < Hands.Length; i++)
        {
            if (i < cardEventList.Count)
            {
                cardEventList[i].SetCard(Hands[i]);
                cardEventList[i].SetFace(true);
            }
        }

        for (var i = 0; i < Hands.Length; i++)
        {
            Debug.Log(string.Format("Hand{0}:{1}", i, Hands[i].ToString()));
        }

        Mode = 2;
    }

}
