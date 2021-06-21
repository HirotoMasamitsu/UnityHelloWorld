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
            t.GetComponent<Text>().text = "Button Clicked!!";
        } 
        else
        {
        }
    }
}
