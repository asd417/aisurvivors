using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToLevel1 : MonoBehaviour
{
    public SpriteMove spriteMove;
    public string scene; 
    private int playersAtDoor = 0;

    void OnTriggerEnter2D(Collider2D collider)
    {  
        if (collider.CompareTag("Player1") || collider.CompareTag("Player2") || collider.CompareTag("Player3"))
        {
            playersAtDoor++;
        }
        if(playersAtDoor == spriteMove.agents.Count)
        {
            MoveOn();
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player1") || collider.CompareTag("Player2") || collider.CompareTag("Player3"))
        {
            playersAtDoor--;
        }
    }

    void Switch()
    {
        SceneManager.LoadScene(scene); 
    }

    void Lose(){
        SceneManager.LoadScene("Lose");
    }

    void MoveOn(){
        if (spriteMove.agents.Count != 0 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Switch();
        }
        if (spriteMove.agents.Count == 0)
        {
            Lose();
        }
    }
}
