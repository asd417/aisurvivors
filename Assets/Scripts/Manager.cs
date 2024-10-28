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

    // private void Start() {
    //     SoundManager.instance.Play("MainMenuMusic");
    // }

    public void Level1()
    {
        StartCoroutine(LoadSceneWithAudio("Level1", "Gameplay-Instrumental1"));
    }

    public void Level2()
    {
        StartCoroutine(LoadSceneWithAudio("Level2", "Gameplay-Instrumental2"));
    }

    public void Level3()
    {
        StartCoroutine(LoadSceneWithAudio("Level3", "Gameplay-Instrumental3"));
    }

    public void Menu()
    {
        StartCoroutine(LoadSceneWithAudio("MainMenu", "MainMenuMusic"));
    }

    public void Lobby()
    {
        StartCoroutine(LoadSceneWithAudio("Lobby", "LobbyMusic"));
    }

    public void Title()
    {
        StartCoroutine(PlayIntroAnimationSounds());
        StartCoroutine(LoadSceneWithAudio("Title"));
    }

    public void Credits()
    {
        StartCoroutine(LoadSceneWithAudio("Credits", "CreditsSong"));
    }

    public void QuitGame()
    {
        Debug.Log("Application Quit.\nThanks for Playing!");
        Application.Quit();
    }

    public void Win()
    {
        StartCoroutine(LoadSceneWithAudio("Win")); // will add music param later
    }

    public void PlayerLost(string currentLevel)
    {
        PlayerPrefs.SetString("LastLevel", currentLevel);
        StartCoroutine(LoadSceneWithAudio("GameLost")); // will add music param later
    }





    private IEnumerator LoadSceneWithAudio(string sceneName, params string[] audioNames)
    {
        Debug.Log($"Loading scene: {sceneName}"); // Debug log to indicate scene loading
        // Confirm SoundManager instance
        if (SoundManager.instance == null)
        {
            Debug.LogError("SoundManager instance is null! Audio playback will not work.");
            yield break;
        }
        SoundManager.instance.StopAllSounds(); // Stop all sounds before changing scenes
        yield return null; // Wait for a frame to allow stopping logic to complete
        
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(0.5f); // Wait a moment for the scene to load

        foreach (var audioName in audioNames) // Iterate through each audio name
        {
            Debug.Log($"Attempting to play audio: {audioName}"); // Debug log before playing audio
            SoundManager.instance.FadeIn(audioName, 3f); // Play the music for the new scene
        }
        // Check if each audio source is playing
            foreach (var audioName in audioNames)
            {
                Sound sound = Array.Find(SoundManager.instance.sounds, s => s.name == audioName);
                if (sound != null && sound.source != null)
                {
                    Debug.Log($"{audioName} source is valid.");
                    if (sound.source.isPlaying)
                    {
                        Debug.Log($"{audioName} is playing.");
                    }
                    else
                    {
                        Debug.LogWarning($"{audioName} is NOT playing!");
                    }
                }
                else
                {
                    Debug.LogError($"Sound {audioName} not found or audio source is null!");
                }
            }
        }


    private IEnumerator PlayIntroAnimationSounds() // used for Title scene change (intro animation)
    {
        // slashes sfx
        SoundManager.instance.Play("MenuPlayAnimation1");
        yield return new WaitForSeconds(2f); // duration to wait between sounds

        // crescendo
        SoundManager.instance.Play("MenuPlayAnimation2"); // Replace with your second sound name
    }
}


