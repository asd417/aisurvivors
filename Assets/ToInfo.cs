using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToInfo : MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Set the flag to true and store the reference to the collider
        isColliding = true;
        currentCollider = collision.collider;

        // Call the method to check the collision after 0.5 seconds
        Invoke(nameof(CheckIfStillColliding), 1f);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // When the collision ends, reset the flag and clear the reference
        if (collision.collider == currentCollider)
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
            Info();
        }
    }
    public void Info()
    {
        SceneManager.LoadScene("Info"); 
    }
}
