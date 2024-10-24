using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI; // Add this namespace for NavMesh

public class SpriteMove : MonoBehaviour
{
    public static SpriteMove instance;
    public Camera camera;
    public float camZoomMultiplier = 0.5f;
    public float maxCamZoom;
    private float minCamZoom = 5;

    public float stoppingDistance = 0.1f; // Distance within which the agent stops
    // Prefab of the sprite to instantiate (must have a NavMeshAgent component)
    public GameObject spritePrefab1;
    public GameObject spritePrefab2;
    public GameObject spritePrefab3;

    public int spotx = 0;
    public int spoty = 0;

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
        SpriteAssign(spritePrefab1);
        SpriteAssign(spritePrefab2);
        SpriteAssign(spritePrefab3);
    }

    void Update()
    {

        // Switch between sprites using QWE
        SwitchNextAgent();

        // Set the target position for the currently selected sprite on mouse click
        if (Input.GetMouseButtonDown(0) && selectedSprite != null)
        {
            SetDestinationForSelectedSprite();
        }
        float maxDist = 0;
        Vector3 averagePos = Vector3.zero;
        int agentCount = 0;
        for (int i = 0; i < agents.Count; i++)
        {
            if (agents[i]) agentCount++;
        }
        for (int i = 0; i < agents.Count; i++)
        {
            // Check if the agent has been destroyed or is null
            if (agents[i] == null) continue; // if null skip
            // Debug.Log("agents.count: " + agentCount);
            averagePos = averagePos + agents[i].transform.position / agentCount;
            for (int j = 0; j < agents.Count; j++)
            {
                if (agents[j] == null) continue;

                float dst = (agents[i].transform.position - agents[j].transform.position).magnitude;
                maxDist = maxDist < dst ? dst : maxDist;
            }
        }

        float zoom = maxDist * camZoomMultiplier * (3 + CheckAlignment());
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, Mathf.Clamp(zoom, maxCamZoom, minCamZoom), Time.deltaTime);
        //camera.transform.position = new Vector3(Mathf.Clamp(averagePos.x,-(10 - 2 * zoom), (10 - 2 * zoom)), Mathf.Clamp(averagePos.y, -(10 - 2 * zoom*camera.aspect), (10 - 2 * zoom)), -10);
        camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(averagePos.x, averagePos.y, -10), Time.deltaTime);
    }

    public float CheckAlignment()
    {
        // Average the angles to determine overall alignment
        float averageAngle = 0;
        for (int i = 0; i < agents.Count - 1; i++)
        {
            if (agents[i] == null) continue; // Check if the agent is null and skip to the next iteration
            for (int j = i+1; j < agents.Count; j++)
            {
                if (agents[j] == null) continue; // another null check here ^
                Vector2 p1 = new Vector2(agents[i].transform.position.x, agents[i].transform.position.y);
                Vector2 p2 = new Vector2(agents[j].transform.position.x, agents[j].transform.position.y);
                averageAngle += Mathf.Abs(GetAngleFromHorizontal(p1, p2)) / agents.Count;
            }
        }
        float normalizedAngle = Mathf.Clamp((averageAngle - 20f) / 25f, 0f, 1f);
        // Apply sine-like function to map to the range [0, 1]
        float verticalStrength = Mathf.Sin(normalizedAngle * Mathf.PI / 2);
        // Debug.Log("Alignment: " + verticalStrength);
        return verticalStrength;
    }

    private float GetAngleFromHorizontal(Vector3 p1, Vector3 p2)
    {
        // Calculate the difference in the x and y coordinates
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;

        // Calculate the angle in degrees from the horizontal
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        // Return the absolute value of the angle to compare with 0�� and 90��
        return Mathf.Abs(angle);
    }

    void SpriteAssign(GameObject sprite){
    
        Vector3 initialPosition = new Vector3(spotx, spoty, 0); //put in position here
        spotx += 2;
        GameObject newSprite = Instantiate(sprite, initialPosition, Quaternion.Euler(0, 0, 0));
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
        Player playerComponent = sprite.GetComponent<Player>();

        if (playerComponent != null)
        {
            playerComponent.setHighlighted(isSelected);
        }
    }
}
