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

    public float moveSpeed = 5f;

    // List of all moveable sprites in the scene
    public List<GameObject> sprites;

    private GameObject selectedSprite;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private int currentSpriteIndex = 0;

    void Start()
    {
        // Initialize the first sprite as selected if there are any sprites in the list
        if (sprites.Count > 0)
        {
            selectedSprite = sprites[currentSpriteIndex];
            HighlightSprite(selectedSprite, true);
        }
    }

    void Update()
    {
        // Toggle between sprites using the Tab key
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleNextSprite();
        }

        // Set the target position for the currently selected sprite on mouse click
        if (Input.GetMouseButtonDown(0) && selectedSprite != null)
        {
            SetTargetPosition();
        }

        // Move the selected sprite to the target position
        if (isMoving && selectedSprite != null)
        {
            MoveSelectedSprite();
        }
    }

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
        SceneManager.LoadScene("LEVEL_NAME"); 
    }
    public void Level2(){
        SceneManager.LoadScene("LEVEL_NAME");
    }
    public void Level3(){
        SceneManager.LoadScene("LEVEL_NAME");
    }
    public void Menu(){
        SceneManager.LoadScene("MainMenu");
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

    // Method to toggle between sprites
    private void ToggleNextSprite()
    {
        // Deselect the current sprite
        if (selectedSprite != null)
        {
            HighlightSprite(selectedSprite, false);
        }

        // Move to the next sprite in the list (looping around)
        currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Count;
        selectedSprite = sprites[currentSpriteIndex];

        // Highlight the new selected sprite
        HighlightSprite(selectedSprite, true);
    }

    // Method to set the target position for the currently selected sprite
    private void SetTargetPosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 for 2D

        targetPosition = mousePosition;
        isMoving = true;
    }

    // Method to move the currently selected sprite to the target position
    private void MoveSelectedSprite()
    {
        float step = moveSpeed * Time.deltaTime;
        selectedSprite.transform.position = Vector3.MoveTowards(selectedSprite.transform.position, targetPosition, step);

        // Stop moving if the sprite reaches the target position
        if (Vector3.Distance(selectedSprite.transform.position, targetPosition) < 0.001f)
        {
            isMoving = false;
        }
    }

    // Method to highlight the selected sprite (optional, visual feedback)
    private void HighlightSprite(GameObject sprite, bool isSelected)
    {
        SpriteRenderer spriteRenderer = sprite.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Change the color based on selection status
            spriteRenderer.color = isSelected ? Color.yellow : Color.white;
        }
    }
}
