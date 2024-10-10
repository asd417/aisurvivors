using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToLevel1 : MonoBehaviour
{
    // Start is called before the first frame update
    /*private int count = 0;

    void Start()
    {
        bool player1 = false;
        bool player2 = false;
        bool player3 = false;

        //GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if(GameObject.FindGameObjectsWithTag("Player1")){
            count++;
        }

        if(GameObject.FindGameObjectsWithTag("Player2")){
            count++;
        }

        if(GameObject.FindGameObjectsWithTag("Player3")){
            count++;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Player1")){
            count++;
        }

        if(GameObject.FindGameObjectsWithTag("Player2")){
            count++;
        }

        if(GameObject.FindGameObjectsWithTag("Player3")){
            count++;
        }

        if (count == 0)
        {
            SceneManager.LoadScene("Lose");
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {  
        if (collision.CompareTag("Player1")){
            player1 = true;
        }
        if (collision.CompareTag("Player2")){
            player2 = true;
        }
        if (collision.CompareTag("Player3")){
            player3 = true;
        }

        if (count == 3 && player1 && player2 && player3){
            Level1();
        }

        
    }

    public void Leve1()
    {
        SceneManager.LoadScene("Info"); 
    }*/
}
