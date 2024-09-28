using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    List<GameObject> agents;
    int selected = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Do something when the spacebar is pressed
            Debug.Log("Agent 1 was selected");
            selected = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Do something when the spacebar is pressed
            Debug.Log("Agent 2 was selected");
            selected = 1;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Do something when the spacebar is pressed
            Debug.Log("Agent 3 was selected");
            selected = 2;
        }

    }
}
