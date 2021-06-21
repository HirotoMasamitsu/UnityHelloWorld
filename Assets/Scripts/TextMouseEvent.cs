using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMouseEvent : IMouseEvent
{
    private Text text;
    private FortuneMessage fortune;

    public TextMouseEvent(GameObject gameObject)
    {
        this.Name = gameObject.name;
        this.text = GameObject.Find(gameObject.name).GetComponent<Text>();
        Debug.Log("TextMouseEvent Create:" + this.Name);
        fortune = new FortuneMessage();
        VideoPokerGame.SetText(this.text);
    }

    public string Name { get; private set; }

    public void Clicked()
    {
        Debug.Log("TextMouseEvent Clicked:" + this.Name);
        //this.text.text = this.fortune.GetMessage();
    }

    public void Entered()
    {
        Debug.Log("TextMouseEvent Entered:" + this.Name);
        //this.text.text = "Hello World!";
    }

    public void Exited()
    {
        Debug.Log("TextMouseEvent Exit:" + this.Name);
        //this.text.text = "Good bye!";
    }

}
