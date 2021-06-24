using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataTextBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "DataText")
        {
            VideoPokerGame.SetDataText(gameObject.GetComponent<Text>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
