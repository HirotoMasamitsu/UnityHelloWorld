using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextObjBehavior : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Text text;
    private FortuneMessage fortune;

    // Start is called before the first frame update
    void Start()
    {
        this.text = GameObject.Find("Text").GetComponent<Text>();
        Debug.Log("TextObjBehavior Start()");
        fortune = new FortuneMessage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        Debug.Log(gameObject.name + " ‚ªƒNƒŠƒbƒN‚³‚ê‚½!");
        text.text = this.fortune.GetMessage();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        text.text = "Hello World!!";
        Debug.Log("Cursor Entering " + gameObject.name + " GameObject");
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        text.text = "Good bye!";
        Debug.Log("Cursor Exiting " + gameObject.name + " GameObject");
    }
}
