using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventSystemClickCheckBehavior : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Dictionary<string, IMouseEvent> eventDic;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("EventSystemClickCheckBehavior Start()");
        this.eventDic = new Dictionary<string, IMouseEvent>();
        var textMouseEvent = new TextMouseEvent(GameObject.Find("Text"));
        this.eventDic.Add(textMouseEvent.Name, textMouseEvent);
        var cardMouseEvent = new CardMouseEvent(GameObject.Find("CardSprite"));
        this.eventDic.Add(cardMouseEvent.Name, cardMouseEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        Debug.Log(gameObject.name + " Clicked!");
        if (this.eventDic.ContainsKey(gameObject.name))
        {
            this.eventDic[gameObject.name].Clicked();
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + gameObject.name + " GameObject");
        if (this.eventDic.ContainsKey(gameObject.name))
        {
            this.eventDic[gameObject.name].Entered();
        }
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + gameObject.name + " GameObject");
        if (this.eventDic.ContainsKey(gameObject.name))
        {
            this.eventDic[gameObject.name].Exited();
        }
    }
}
