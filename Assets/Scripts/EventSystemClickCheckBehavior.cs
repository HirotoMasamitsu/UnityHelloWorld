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
        var cardMouseEvent = new CardMouseEvent(GameObject.Find("CardSprite"), 0);
        this.eventDic.Add(cardMouseEvent.Name, cardMouseEvent);
        for (var i = 1; i <= 4; i++)
        {
            Debug.Log("FindName: " + (string.Format("CardSprite({0})", i)));
            var cardMouseEventI = new CardMouseEvent(GameObject.Find(string.Format("CardSprite ({0})", i)), i);
            Debug.Log("Find " + cardMouseEventI.Name);
            this.eventDic.Add(cardMouseEventI.Name, cardMouseEventI);
            
        }
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
        Debug.Log("Cursor Entering " + gameObject.name);
        if (this.eventDic.ContainsKey(gameObject.name))
        {
            this.eventDic[gameObject.name].Entered();
        }
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + gameObject.name);
        if (this.eventDic.ContainsKey(gameObject.name))
        {
            this.eventDic[gameObject.name].Exited();
        }
    }
}
