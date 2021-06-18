using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TEMP
{
    /// <summary>
    /// トランプの役
    /// </summary>
    public enum PKHands
    {
        /// <summary>
        /// ロイヤルストレートフラッシュ
        /// </summary>
        RoyalStraightFlash = 0,
        /// <summary>
        /// ストレートフラッシュ
        /// </summary>
        StraightFlash = 1,
        /// <summary>
        /// フォーカード
        /// </summary>
        FourCards = 2,
        /// <summary>
        /// フルハウス
        /// </summary>
        FullHouse = 3,
        /// <summary>
        /// フラッシュ
        /// </summary>
        Flash = 4,
        /// <summary>
        /// ストレート
        /// </summary>
        Straight = 5,
        /// <summary>
        /// スリーカード
        /// </summary>
        ThreeCards = 6,
        /// <summary>
        /// ツーペア
        /// </summary>
        TwoPair = 7,
        /// <summary>
        /// ワンペア
        /// </summary>
        OnePair = 8,
        /// <summary>
        /// ノーペア
        /// </summary>
        NoPair = 9,
        /// <summary>
        /// その他判定エラー
        /// </summary>
        ERROR = 99
    }

    /// <summary>
    /// ポーカーの役をチェックする静的クラス
    /// </summary>
    public static class PKCheck
    {
        /// <summary>
        /// 7枚のカードから5枚を選出する組み合わせを定義した配列
        /// </summary>
        private static readonly int[][] SEVEN_HANDS = new int[][]{
                                     new int[]{ 0, 1, 2, 3, 4 },
                                     new int[]{ 0, 1, 2, 3, 5 },
                                     new int[]{ 0, 1, 2, 3, 6 },
                                     new int[]{ 0, 1, 2, 4, 5 },
                                     new int[]{ 0, 1, 2, 4, 6 },
                                     new int[]{ 0, 1, 2, 5, 6 },
                                     new int[]{ 0, 1, 3, 4, 5 },
                                     new int[]{ 0, 1, 3, 4, 6 },
                                     new int[]{ 0, 1, 3, 5, 6 },
                                     new int[]{ 0, 1, 4, 5, 6 },
                                     new int[]{ 0, 2, 3, 4, 5 },
                                     new int[]{ 0, 2, 3, 4, 6 },
                                     new int[]{ 0, 2, 3, 5, 6 },
                                     new int[]{ 0, 2, 4, 5, 6 },
                                     new int[]{ 0, 3, 4, 5, 6 },
                                     new int[]{ 1, 2, 3, 4, 5 },
                                     new int[]{ 1, 2, 3, 4, 6 },
                                     new int[]{ 1, 2, 3, 5, 6 },
                                     new int[]{ 1, 2, 4, 5, 6 },
                                     new int[]{ 1, 3, 4, 5, 6 },
                                     new int[]{ 2, 3, 4, 5, 6 }
        };


        /// <summary>
        /// 7枚のカードから5枚を選び出した時の最も強い役を取得する静的メソッド
        /// </summary>
        /// <param name="cards">カードの配列</param>
        /// <returns>成立する最強の役</returns>
        public static PKHands CheckSevenHands(Card[] cards)
        {
            // 初期値
            PKHands ret = PKHands.ERROR;
            // 7枚のカードが渡されていることを確認する
            if (cards != null && cards.Length == 7)
            {
                // 7枚から5枚選出のすべての組み合わせを試す
                for (int i = 0; i < SEVEN_HANDS.Length; i++)
                {
                    int[] handPattern = SEVEN_HANDS[i];
                    // 5枚を選出する
                    List<Card> handCards = new List<Card>();
                    for (int j = 0; j < handPattern.Length; j++)
                    {
                        handCards.Add(cards[handPattern[j]]);
                    }
                    // 選出した5枚で成立する役を取得
                    PKHands handResult = CheckHands(handCards.ToArray());
                    // すでに取得した役より強ければ更新
                    if ((int)handResult < (int)ret)
                    {
                        ret = handResult;
                    }
                }
            }
            // 取得した役を返す
            return ret;
        }

        /// <summary>
        /// 5枚の手札のポーカー役を取得する静的メソッド
        /// </summary>
        /// <param name="cards">カードの配列</param>
        /// <returns>成立するポーカーの役</returns>
        public static PKHands CheckHands(Card[] cards)
        {
            // 初期値
            PKHands ret = PKHands.ERROR;
            // 5枚のカードが渡されていることを確認する
            if (cards == null || cards.Length < 5)
            {
                return ret;
            }
            else if (cards.Length > 5)
            {
                return ret;
            }
            else
            {
                ret = PKHands.NoPair;
                // 5枚のカードを並べ替え
                Array.Sort(cards);
                // 解析を行う
                bool flashFlag = true;
                Suit flashSuit = Suit.Joker;
                bool straightFlag = true;
                int straightCount = -1;
                Dictionary<PCNumber, int> pcDic = new Dictionary<PCNumber, int>();
                for (int i = 0; i < cards.Length; i++)
                {
                    // ストレート、フラッシュのチェック
                    if (i == 0)
                    {
                        // 1枚目はスートと番号を記憶
                        flashSuit = cards[i].Suit;
                        straightCount = (int)cards[i].Number;
                    }
                    else
                    {
                        // 2枚目以降の処理
                        // 同スートチェック
                        if (flashFlag == true && cards[i].Suit != flashSuit)
                        {
                            flashFlag = false;
                        }
                        // 連番チェック
                        if (straightFlag == true)
                        {
                            if (straightCount == 1)
                            {
                                // 1枚目がAの場合は2、10がストレートチェックの対象になる
                                int cardNum = (int)cards[i].Number;
                                if (cardNum == 2 || cardNum == 10)
                                {
                                    straightCount = cardNum;
                                }
                                else
                                {
                                    straightFlag = false;
                                }
                            }
                            else
                            {
                                // 1枚目がA以外なら連番チェック
                                straightCount += 1;
                                if ((int)cards[i].Number != straightCount)
                                {
                                    straightFlag = false;
                                }
                            }
                        }
                    }
                    // 同一番号枚数チェック
                    if (pcDic.Keys.Contains<PCNumber>(cards[i].Number) == true)
                    {
                        pcDic[cards[i].Number]++;
                    }
                    else
                    {
                        pcDic.Add(cards[i].Number, 1);
                    }
                }
                // ストレートフラッシュのチェック
                if (flashFlag && straightFlag)
                {
                    // 1枚目がA、5枚目がKならロイヤル成立
                    if (cards[0].Number == PCNumber.A && cards[cards.Length - 1].Number == PCNumber.King)
                    {
                        ret = PKHands.RoyalStraightFlash;
                    }
                    else
                    {
                        ret = PKHands.StraightFlash;
                    }
                }
                // フラッシュ
                else if (flashFlag)
                {
                    ret = PKHands.Flash;
                }
                // ストレート
                else if (straightFlag)
                {
                    ret = PKHands.Straight;
                }
                // その他役のチェック
                else
                {
                    // ペア以上チェック
                    int maxCards = 0;
                    int pairCount = 0;
                    foreach (PCNumber key in pcDic.Keys)
                    {
                        if (maxCards < pcDic[key])
                        {
                            maxCards = pcDic[key];
                        }
                        if (pcDic[key] >= 2)
                        {
                            pairCount++;
                        }
                    }
                    if (maxCards >= 4)
                    {
                        // フォーカード
                        ret = PKHands.FourCards;
                    }
                    else if (maxCards == 3 && pairCount >= 2)
                    {
                        // フルハウス
                        ret = PKHands.FullHouse;
                    }
                    else if (maxCards == 3)
                    {
                        // スリーカード
                        ret = PKHands.ThreeCards;
                    }
                    else if (pairCount >= 2)
                    {
                        // ツーペア
                        ret = PKHands.TwoPair;
                    }
                    else if (pairCount == 1)
                    {
                        // ワンペア
                        ret = PKHands.OnePair;
                    }
                    else
                    {
                        // ノーペア
                        ret = PKHands.NoPair;
                    }
                }
                // 取得した役を返す
                return ret;
            }
        }

    }
}
