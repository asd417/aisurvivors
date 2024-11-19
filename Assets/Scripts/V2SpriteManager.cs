using NavMeshPlus.Extensions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class V2SpriteManager : MonoBehaviour
{
    public static SpriteMove instance;
    public Camera camera;
    public float camZoomMultiplier = 0.5f;
    public float maxCamZoom;
    private float minCamZoom = 5;

    public Weapon weapon; 

    public int chipCount = 0;

    public float stoppingDistance = 0.1f; // Distance within which the agent stops
    // Prefab of the sprite to instantiate (must have a NavMeshAgent component)
    public GameObject spritePrefab1;
    public GameObject spritePrefab2;
    public GameObject spritePrefab3;

    public int spotx = 0;
    public int spoty = 0;

    public AudioClip selected;
    public AudioClip error;

    // Number of sprites to instantiate
    public int agentCount = 0;

    // List to hold the instantiated sprite objects
    public List<GameObject> agents = new List<GameObject>(3);

    private GameObject selectedSprite;
    private int currentSpriteIndex = 0;

    public bool shouldSpawn = true;
    private bool ready = false;
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        chipCount = 0;
    }

    private void AddAgentsToSpriteManager()
    {
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
        GameObject player3 = GameObject.FindGameObjectWithTag("Player3");
        if (player1)
        {
            agentCount++;
            agents[0] = player1;
        }
        if (player2)
        {
            agentCount++;
            agents[1] = player2;
        }
        if (player3)
        {
            agentCount++;
            agents[2] = player3;
        }

    }
    public Transform GetClosestPlayer(Vector3 position)
    {
        float minDistance = float.MaxValue;
        Transform player = null;
        foreach (var agent in agents)
        {
            if (agent)
            {
                float ndist = (position - agent.transform.position).magnitude;
                if (ndist < minDistance)
                {
                    minDistance = ndist;
                    player = agent.transform;
                }
            }
        }
        if (player == null) throw new Exception("Player Not Found");
        return player;
    }
    private void ConnectAgentsToScene()
    {
        camera = Camera.main;
        GameObject p1s = GameObject.Find("Player1Spawn");
        GameObject p2s = GameObject.Find("Player2Spawn");
        GameObject p3s = GameObject.Find("Player3Spawn");
        if (agents[0] != null && p1s != null) agents[0].GetComponent<NavMeshAgent>().Warp(p1s.transform.position);
        if (agents[1] != null && p2s != null) agents[1].GetComponent<NavMeshAgent>().Warp(p2s.transform.position);
        if (agents[2] != null && p3s != null) agents[2].GetComponent<NavMeshAgent>().Warp(p3s.transform.position);
        agents[0].GetComponent<HealthManager>().healthBar = GameObject.Find("Healthbar_Frank").GetComponent<Healthbar>();
        agents[1].GetComponent<HealthManager>().healthBar = GameObject.Find("Healthbar_Maz").GetComponent<Healthbar>();
        agents[2].GetComponent<HealthManager>().healthBar = GameObject.Find("Healthbar_Orin").GetComponent<Healthbar>();
    }

    void Start() //Only fires in lobby because this gameobject is persistent
    {
        if (SceneManager.GetActiveScene().name == "Level2 1")
        {
            SpriteAssign(spritePrefab1);
            SpriteAssign(spritePrefab2);
            SpriteAssign(spritePrefab3);
            ConnectAgentsToScene();
        }
        else
        {
            AddAgentsToSpriteManager();
            ConnectAgentsToScene();
        }
    }
    void Update()
    {
        if (!camera) return;
        // Switch between sprites using QWE
        SwitchNextAgent();

        // Set the target position for the currently selected sprite on mouse click
        if (Input.GetMouseButtonDown(0) && selectedSprite != null)
        {
            SetDestinationForSelectedSprite();
        }
        float maxDist = 0;
        Vector3 averagePos = Vector3.zero;
        int curAgentCount = 0;
        for (int i = 0; i < agents.Count; i++)
        {
            if (agents[i]) curAgentCount++;
        }
        agentCount = curAgentCount;
        for (int i = 0; i < agents.Count; i++)
        {
            // Check if the agent has been destroyed or is null
            if (agents[i] == null) continue;
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
    public int GetAgentCount()
    {
        return agentCount;
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
        newSprite.GetComponent<NavMeshAgent>().updateRotation = false;
        agents.Add(newSprite);
    }

    private void SwitchNextAgent()
    {
        if (Input.GetKeyDown(KeyCode.Q) && agents.Count > 0)
        {
            
            if (selectedSprite != null)
            {
                AudioSource.PlayClipAtPoint(selected, transform.position);
                HighlightSprite(selectedSprite, false);
            }
            else{
                AudioSource.PlayClipAtPoint(error, transform.position);
            }
            // Do something when the spacebar is pressed
            Debug.Log("Agent 1 was selected");
            currentSpriteIndex = 0;
            selectedSprite = agents[0];
            if (selectedSprite != null) {
                HighlightSprite(selectedSprite, true);
            }
        }
        if (Input.GetKeyDown(KeyCode.W) && agents.Count > 1)
        {
            if (selectedSprite != null)
            {
                AudioSource.PlayClipAtPoint(selected, transform.position);
                HighlightSprite(selectedSprite, false);
            }
            else{
                AudioSource.PlayClipAtPoint(error, transform.position);
            }
            // Do something when the spacebar is pressed
            Debug.Log("Agent 2 was selected");
            currentSpriteIndex = 1;
            selectedSprite = agents[1];
            if (selectedSprite != null) {
                HighlightSprite(selectedSprite, true);
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && agents.Count > 2)
        {
            if (selectedSprite != null)
            {
                AudioSource.PlayClipAtPoint(selected, transform.position);
                HighlightSprite(selectedSprite, false);
            }
            else{
                AudioSource.PlayClipAtPoint(error, transform.position);
            }
            // Do something when the spacebar is pressed
            Debug.Log("Agent 3 was selected");
            currentSpriteIndex = 2;
            selectedSprite = agents[2];
            if (selectedSprite != null) {
                HighlightSprite(selectedSprite, true);
            }
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
        NavMeshAgent selectedAgent = agents[currentSpriteIndex].GetComponent<NavMeshAgent>();

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
