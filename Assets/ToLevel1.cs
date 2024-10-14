using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToLevel1 : MonoBehaviour
{
    public string scene; 
    private bool player1;
    private bool player2;
    private bool player3;       

    void Start(){
        player1 = false;
        player2 = false;
        player3 = false;
    }

    void Update()
    {
        MoveOn();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {  
        if (collision.collider.CompareTag("Player1")){
            player1 = true;
        }
        if (collision.collider.CompareTag("Player2")){
            player2 = true;
        }
        if (collision.collider.CompareTag("Player3")){
            player3 = true;
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
        if (GameObject.FindGameObjectsWithTag("Player1").Length == 0 && player2 && player3 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
            Switch();
        }
        if (GameObject.FindGameObjectsWithTag("Player2").Length == 0 && player1 && player3 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
            Switch();
        }
        if (GameObject.FindGameObjectsWithTag("Player3").Length == 0 && player2 && player1 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
            Switch();
        }
        if (player1 && player2 && player3 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
            Switch();
        }
        if (GameObject.FindGameObjectsWithTag("Player1").Length == 0 && GameObject.FindGameObjectsWithTag("Player2").Length == 0 && player3 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
            Switch();
        }
        if (GameObject.FindGameObjectsWithTag("Player1").Length == 0 && GameObject.FindGameObjectsWithTag("Player3").Length == 0 && player2 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
            Switch();
        }
        if (GameObject.FindGameObjectsWithTag("Player2").Length == 0 && GameObject.FindGameObjectsWithTag("Player3").Length == 0 && player1 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
            Switch();
        }
        if (GameObject.FindGameObjectsWithTag("Player1").Length == 0 && GameObject.FindGameObjectsWithTag("Player2").Length == 0 && GameObject.FindGameObjectsWithTag("Player3").Length == 0 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
                Lose();
            }
    }
}
