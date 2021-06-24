using System.Collections;
using System.Collections.Generic;

using TEMP;
using UnityEngine;
using UnityEngine.UI;

public static class VideoPokerGame
{
    private static System.Random rand = new System.Random();
    private static Deck deck;
    private static Odds odds = new Odds(true);
    private static int score = 0;
    private static DataAnalyze dataAnalyse = new DataAnalyze(true);

    private static Text text;
    private static Text scoreText;
    private static Text oddsText;
    private static Text dataText;
    private static List<CardMouseEvent> cardEventList = new List<CardMouseEvent>();

    public static Card[] Hands { get; private set; }
    public static bool[] IsHolds { get; private set; }

    public static int Mode { get; private set; }


    public static void SetText(Text textUI)
    {
        text = textUI;
    }

    public static void SetScoreText(Text textUI)
    {
        scoreText = textUI;
    }

    public static void SetDataText(Text textUI)
    {
        dataText = textUI;
    }

    public static void SetOddsText(Text textUI)
    {
        oddsText = textUI;
        oddsText.text = string.Format("{0}: x{1}\r\n{2}: x{3}\r\n{4}: x{5}\r\n{6}: x{7}\r\n{8}: x{9}\r\n{10}: x{11}\r\n{12}: x{13}\r\n{14}: x{15}\r\n{16}: x{17}\r\n{18}: x{19}",
            PKHands.RoyalStraightFlush, odds.GetWinScore(new PokerResults(PKHands.RoyalStraightFlush, new int[] { 1 }), 1),
            PKHands.StraightFlush, odds.GetWinScore(new PokerResults(PKHands.StraightFlush, new int[] { 1 }), 1),
            PKHands.FourCards, odds.GetWinScore(new PokerResults(PKHands.FourCards, new int[] { 1 }), 1),
            PKHands.FullHouse, odds.GetWinScore(new PokerResults(PKHands.FullHouse, new int[] { 1 }), 1),
            PKHands.Flush, odds.GetWinScore(new PokerResults(PKHands.Flush, new int[] { 1 }), 1),
            PKHands.Straight, odds.GetWinScore(new PokerResults(PKHands.Straight, new int[] { 1 }), 1),
            PKHands.ThreeCards, odds.GetWinScore(new PokerResults(PKHands.ThreeCards, new int[] { 1 }), 1),
            PKHands.TwoPair, odds.GetWinScore(new PokerResults(PKHands.TwoPair, new int[] { 1 }), 1),
            (odds.UseJacksOrBetter ? "Jacks or Better" : PKHands.OnePair.ToString()), odds.GetWinScore(new PokerResults(PKHands.OnePair, new int[] { 1 }), 1),
            PKHands.HighCard, odds.GetWinScore(new PokerResults(PKHands.HighCard, new int[] { 1 }), 1));
    }

    public static void SetCardEvent(CardMouseEvent cme) 
    {
        Debug.Log(string.Format("SetCardEvent {0}", cme));
        cardEventList.Add(cme);
    }

    public static void Reset()
    {
        dataAnalyse.Load();
        dataText.text = dataAnalyse.ToString();
        Mode = 0;
        score -= 1;
        if (scoreText != null)
        {
            scoreText.text = string.Format("Score: {0}", score);
        }
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
        score += odds.GetWinScore(checkHands, 1);
        if (text != null)
        {
            text.text = checkHands.ToString();
        }
        if (scoreText != null)
        {
            scoreText.text = string.Format("Score: {0}", score);
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

        dataAnalyse.Update(score, checkHands);
        dataAnalyse.Save();
        dataText.text = dataAnalyse.ToString();

        Mode = 2;
    }

}
