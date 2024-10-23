using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    //public int score;
    //public Text scoreText;

    public static Manager instance;

    private void Awake(){
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level1"); 
    }
    public void Level2(){
        SceneManager.LoadScene("Level2");
    }
    public void Level3(){
        SceneManager.LoadScene("Level3");
    }
    public void Menu(){
        SceneManager.LoadScene("MainMenu");
    }
    public void Lobby()
    {
        SceneManager.LoadScene("Lobby"); 
    }
    public void Title()
    {
        SceneManager.LoadScene("Title"); 
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits"); 
    }
    public void QuitGame()
    {
        Debug.Log("Application Quit.\nThanks for Playing!");
        Application.Quit();
    }
    public void Win(){
        SceneManager.LoadScene("Win");
    }

    public void PlayerLost(string currentLevel)
    {
        PlayerPrefs.SetString("LastLevel", currentLevel);
        SceneManager.LoadScene("GameLost");
    }
}
