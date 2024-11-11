using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class ToStory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool isColliding = false; // Tracks whether the collision is still happening
    private Collider2D currentCollider; // Store the reference to the current collider

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Set the flag to true and store the reference to the collider
        StartCoroutine(DelayedStory(3f));
        
    }

    private IEnumerator DelayedStory(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Story();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // When the collision ends, reset the flag and clear the reference
        if (collision.GetComponent<Collider>() == currentCollider)
        {
            isColliding = false;
            currentCollider = null;
        }
    }

    private void CheckIfStillColliding()
    {
        // Check if the collision is still happening after the delay
        if (isColliding)
        {
            // Collision is still active after 0.5 seconds
            Story();
        }
    }
    public void Story()
    {
        isColliding = false;
        SceneManager.LoadScene("Story"); 
    }
}
