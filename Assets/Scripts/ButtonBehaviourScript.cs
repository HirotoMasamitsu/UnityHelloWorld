using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviourScript : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("Button OnClick! " + gameObject.name);
        var t = gameObject.transform.Find("Text");
        if (t != null)
        {
            if (VideoPokerGame.Mode != 1)
            {
                VideoPokerGame.Reset();
                t.GetComponent<Text>().text = "Hand Change";
            } 
            else
            {
                VideoPokerGame.Change();
                t.GetComponent<Text>().text = "Retry";
            }
        } 
        else
        {
        }
    }

    //publi
}
