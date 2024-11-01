using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

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
        if(playersAtDoor == spriteMove.GetAgentCount())
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
    private void Update()
    {
        if(!spriteMove)
        {
            GameObject obj = GameObject.Find("SpriteManager");
            if (obj)
            {
                spriteMove = obj.GetComponent<SpriteMove>();
            }
        }
        if (spriteMove.GetAgentCount() == 0 && spriteMove)
        {
            Lose();
        }
    }

    void Switch()
    {
        //Scene targetScene = SceneManager.GetSceneByName(scene); // Target scene
        //spriteMove.TransferAgentsToNewScene(targetScene);
        SceneManager.LoadScene(scene); 
    }

    void Lose(){
        SceneManager.LoadScene("Lose");
    }

    void MoveOn(){
        int spriteCount = spriteMove.GetAgentCount();
        if (spriteCount != 0 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Switch();
        }
    }
}
