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
        Debug.Log("Trigger Level1 scene");
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        Debug.Log("Trigger Level2 scene");
        SceneManager.LoadScene("Level2");
    }

    public void Level3()
    {
        Debug.Log("Trigger Level3 scene");
        SceneManager.LoadScene("Level3");
    }

    public void Scythes()
    {
        SceneManager.LoadScene("Scythes");
    }
    public void Pens()
    {
        SceneManager.LoadScene("Pens");
    }
    public void EnergySwords()
    {
        SceneManager.LoadScene("EnergySwords");
    }
    public void Game2()
    {
        SceneManager.LoadScene("Level2 1");
    }
    public void Info()
    {
        SceneManager.LoadScene("Info");
    }

    public void Menu()
    {
        Debug.Log("Trigger Menu scene");
        SceneManager.LoadScene("MainMenu");
    }

    public void Lobby()
    {
        Debug.Log("Trigger Lobby scene");
        SceneManager.LoadScene("Lobby");
    }

    public void Title()
    {
        Debug.Log("Trigger Title scene");
        SceneManager.LoadScene("Title");    
    }

    public void Credits()
    {
        Debug.Log("Trigger Credits scene");
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Debug.Log("Application Quit.\nThanks for Playing!");
        Application.Quit();
    }

    public void Win()
    {
        Debug.Log("Trigger Win scene");
        SceneManager.LoadScene("Win");
    }

    public void PlayerLost(string currentLevel)
    {
        PlayerPrefs.SetString("LastLevel", currentLevel);
        SceneManager.LoadScene("GameLost");
    }





    private IEnumerator LoadSceneWithAudio(string sceneName, params string[] audioNames) {
        // Debug.Log($"Loading scene: {sceneName}");
        
        // Confirm SoundManager instance
        if (SoundManager.instance == null)
        {
            // Debug.LogError("SoundManager instance is null! Audio playback will not work.");
            yield break;
        }
        
        SoundManager.instance.StopAllSounds(); // Stop all sounds before changing scenes
        yield return null; // Wait for a frame to allow stopping logic to complete
        
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(0.5f); // Wait a moment for the scene to load

        // Iterate through each audio name
        foreach (var audioName in audioNames)
        {
            // Debug.Log($"Attempting to play audio: {audioName}");
            SoundManager.instance.FadeIn(audioName, 3f); // Play the music for the new scene
        }

        // Check if each audio source is playing
        foreach (var audioName in audioNames)
        {
            Sound sound = Array.Find(SoundManager.instance.sounds, s => s.name == audioName);
            if (sound != null && sound.source != null)
            {
                // Debug.Log($"{audioName} source is valid.");
                if (!sound.source.isPlaying)
                {
                    // Debug.LogWarning($"{audioName} is NOT playing!");
                }
            }
            else
            {
                // Debug.LogError($"Sound {audioName} not found or audio source is null!");
            }
        }
    }
}


