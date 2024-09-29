using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Add this namespace for NavMesh

public class SpriteMove : MonoBehaviour
{
    public static SpriteMove instance;

    public string keyTab = "tab";
    public float stoppingDistance = 0.1f; // Distance within which the agent stops

    // Prefab of the sprite to instantiate (must have a NavMeshAgent component)
    public GameObject spritePrefab;

    // Number of sprites to instantiate
    public int numberOfSprites;

    // List to hold the instantiated sprite objects
    private List<GameObject> agents = new List<GameObject>();

    // List to keep track of the NavMeshAgents for each sprite
    private List<NavMeshAgent> navMeshAgents = new List<NavMeshAgent>();

    private GameObject selectedSprite;
    private int currentSpriteIndex = 0;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of Manager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Instantiate a finite number of sprites and add them to the list
        if (spritePrefab != null)
        {
            for (int i = 0; i < numberOfSprites; i++)
            {
                Vector3 initialPosition = new Vector3(i * 2.0f, 0, 0); // Replace with a position on your NavMesh
                
                // Instantiate the sprite prefab at a valid position on the 2D NavMesh
                GameObject newSprite = Instantiate(spritePrefab, initialPosition, Quaternion.identity);
                agents.Add(newSprite);

                // Ensure each sprite has a NavMeshAgent component
                NavMeshAgent agent = newSprite.GetComponent<NavMeshAgent>();
                if (agent == null)
                {
                    agent = newSprite.AddComponent<NavMeshAgent>(); // Add component if not present
                }

                // Configure NavMeshAgent properties (optional)
                agent.stoppingDistance = stoppingDistance;
                agent.updateUpAxis = false; // Disable the update of the up axis to work in 2D
                agent.updateRotation = false; // Disable rotation updates to prevent 3D rotations

                navMeshAgents.Add(agent);
            }

            // Select the first sprite if there are any sprites in the list
            if (agents.Count > 0)
            {
                selectedSprite = agents[currentSpriteIndex];
                HighlightSprite(selectedSprite, true);
            }
        }
        else
        {
            Debug.LogWarning("Sprite prefab is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        // Switch between sprites using QWE
        SwitchNextAgent();

        // Toggle between sprites using the Tab key, if there are any sprites
        if (Input.GetKeyDown(keyTab) && agents.Count > 0)
        {
            //ToggleNextSprite();
        }

        // Set the target position for the currently selected sprite on mouse click
        if (Input.GetMouseButton(0) && selectedSprite != null)
        {
            Debug.Log("Mouse button down");
            SetDestinationForSelectedSprite();
        }
    }
    // Instead of tab, use QWE
    private void SwitchNextAgent()
    {
        
        if (Input.GetKeyDown(KeyCode.Q) && agents.Count > 0)
        {
            if (selectedSprite != null)
            {
                HighlightSprite(selectedSprite, false);
            }
            // Do something when the spacebar is pressed
            Debug.Log("Agent 1 was selected");
            currentSpriteIndex = 0;
            selectedSprite = agents[0];
            HighlightSprite(selectedSprite, true);
        }
        if (Input.GetKeyDown(KeyCode.W) && agents.Count > 1)
        {
            if (selectedSprite != null)
            {
                HighlightSprite(selectedSprite, false);
            }
            // Do something when the spacebar is pressed
            Debug.Log("Agent 2 was selected");
            currentSpriteIndex = 1;
            selectedSprite = agents[1];
            HighlightSprite(selectedSprite, true);
        }
        if (Input.GetKeyDown(KeyCode.E) && agents.Count > 2)
        {
            if (selectedSprite != null)
            {
                HighlightSprite(selectedSprite, false);
            }
            // Do something when the spacebar is pressed
            Debug.Log("Agent 3 was selected");
            currentSpriteIndex = 2;
            selectedSprite = agents[2];
            HighlightSprite(selectedSprite, true);
        }
    }

    // Method to toggle between sprites
    private void ToggleNextSprite()
    {
        if (agents == null || agents.Count == 0)
            return;

        if (selectedSprite != null)
        {
            HighlightSprite(selectedSprite, false);
        }

        currentSpriteIndex = (currentSpriteIndex + 1) % agents.Count;
        selectedSprite = agents[currentSpriteIndex];

        HighlightSprite(selectedSprite, true);
    }

    // Method to set the target position for the currently selected sprite
    private void SetDestinationForSelectedSprite()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 for 2D

        // Find the NavMeshAgent associated with the selected sprite
        NavMeshAgent selectedAgent = navMeshAgents[currentSpriteIndex];

        // Set the target position for the NavMeshAgent
        selectedAgent.SetDestination(mousePosition);
    }

    // Method to highlight the selected sprite (optional, visual feedback)
    private void HighlightSprite(GameObject sprite, bool isSelected)
    {
        SpriteRenderer spriteRenderer = sprite.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = isSelected ? Color.yellow : Color.white;
        }
    }
}
