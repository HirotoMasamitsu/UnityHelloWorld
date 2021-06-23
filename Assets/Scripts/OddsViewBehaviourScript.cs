using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OddsViewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "OddsText")
        {
            VideoPokerGame.SetOddsText(gameObject.GetComponent<Text>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
