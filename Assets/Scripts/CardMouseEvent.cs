using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CardMouseEvent : IMouseEvent
{
    private SpriteRenderer renderer;
    private bool isFace;


    public CardMouseEvent(GameObject gameObject, bool isFace = true)
    {
        this.Name = gameObject.name;
        this.renderer = GameObject.Find(gameObject.name).GetComponent<SpriteRenderer>();
        this.isFace = isFace;
        Debug.Log("CardMouseEvent Create:" + this.Name);
    }

    public string Name { get; private set; }

    public void Clicked()
    {
        Debug.Log("CardMouseEvent Clicked:" + this.Name);
        // クリックされたらカードを反転する
        this.isFace = !this.isFace;
        if (this.isFace)
        {
            var sprite = Resources.Load<Sprite>(Path.Combine("Playing Cards", "Image", "PlayingCards", "Joker_Color"));
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
