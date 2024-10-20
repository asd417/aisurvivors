using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float targetUpdateInterval = 0.5f; // frequency for updating the "closest" (player) target
    [SerializeField] Transform target;  // closest player that enemy will target/track
    NavMeshAgent agent;                 // NavMeshAgent for pathfinding
    [SerializeField] float speed = 3.0f;  // Movement speed
    public int health = 2;
    public GameObject itemDropPrefab; // to be reference to `ComputerChip-ItemDrop` prefab



    private void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();

        // Disable rotation and up-axis updating
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // Set agent's speed to specified movement speed
        agent.speed = speed;

        // Repeatedly find the closest player at regular intervals
        InvokeRepeating(nameof(LocateClosestTarget), 0f, targetUpdateInterval);
    }

    private void Update()
    {
        if (target != null) //if the target exists
        {
            // Move agent towards target pos
            agent.SetDestination(target.position);
        }
    }

    // Handle what happens when enemy reaches player/collides with something
    private void OnCollisionEnter2D(Collision2D collision)
    {   
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        Debug.Log($"Enemy health: {health}");
        if (health <= 0)
        {
            // Instantiate the item drop at the enemy's position
            Debug.Log("Preparing item drop!");

            Instantiate(itemDropPrefab, transform.position, Quaternion.identity);
            Debug.Log("Item drop instantiated.");

            Destroy(gameObject);
        }
    }

    // Find and update the closest player target based on the distance
    private void LocateClosestTarget()
    {
        // Find all game objects tagged with "Player"
        List<GameObject> playersList = new List<GameObject>();

        // Add all GameObjects with different tags to the list
        playersList.AddRange(GameObject.FindGameObjectsWithTag("Player1"));
        playersList.AddRange(GameObject.FindGameObjectsWithTag("Player2"));
        playersList.AddRange(GameObject.FindGameObjectsWithTag("Player3"));

        // Convert the list to an array if needed
        GameObject[] players = playersList.ToArray();

        if (players.Length == 0)
        {
            target = null;  // No players, so no target
            return;
        }

        // Set the first player as the initial closest target
        GameObject closestPlayer = players[0];
        float closestDistance = Vector2.Distance(transform.position, closestPlayer.transform.position);

        // Loop through all players to find the one closest to the enemy
        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < closestDistance)
            {
                closestPlayer = player;
                closestDistance = distanceToPlayer;
            }
        }

        // Update the target to the closest player
        target = closestPlayer.transform;
    }
}

