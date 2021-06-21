using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TEMP;

public class CardMouseEvent : IMouseEvent
{
    private SpriteRenderer renderer;
    private bool isFace;
    private int no;


    public CardMouseEvent(GameObject gameObject, int no, bool isFace = true)
    {
        this.Name = gameObject.name;
        this.renderer = GameObject.Find(gameObject.name).GetComponent<SpriteRenderer>();
        this.isFace = isFace;
        this.no = no;
        Debug.Log(string.Format("CardMouseEvent Create: {0}({1})", this.Name, this.no));
    }

    public string Name { get; private set; }

    public void Clicked()
    {
        Debug.Log("CardMouseEvent Clicked:" + this.Name);
        // クリックされたらカードを反転する
        this.isFace = !this.isFace;
        if (this.isFace)
        {
            //var sprite = Resources.Load<Sprite>(Path.Combine("Playing Cards", "Image", "PlayingCards", "Joker_Color"));
            // ランダムにカードのガラを決める
            //var v = this.rand.Next(0, 54);
            //Card card;
            //if (v < 52)
            //{
            //    card = new Card((v / 13) + 1, (v % 13) + 1);
            //}
            //else
            //{
            //    card = new Card(0, v - 51);
            //}
            //Debug.Log(string.Format("DrawCard: {0}({1})", card.ToString(), v));
            var card = VideoPokerGame.Hands[this.no];
            var sprite = Resources.Load<Sprite>(Path.Combine("Playing Cards", "Image", "PlayingCards", card.ToResourceString()));
            this.renderer.sprite = sprite;
        }
        else
        {
            var sprite = Resources.Load<Sprite>(Path.Combine("Playing Cards", "Image", "PlayingCards", "BackColor_Blue"));
            this.renderer.sprite = sprite;
        }
    }

    public void Entered()
    {
        Debug.Log("CardMouseEvent Entered:" + this.Name);
    }

    public void Exited()
    {
        Debug.Log("CardMouseEvent Exit:" + this.Name);
    }

}
