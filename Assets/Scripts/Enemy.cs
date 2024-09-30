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
    public int health = 1;

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
        if (collision.gameObject.CompareTag("Player"))
        {
            // enemy-player interactions here can be implemented here
            Debug.Log("Enemy collided with Player!");
        }
        if (collision.gameObject.CompareTag("Weapon"))
        {
            // enemy-player interactions here can be implemented here
            health--;
            if(health <= 0){
                Destroy(gameObject);
            }
            Debug.Log("Enemy collided with Weapon!");
        }
    }

    // Find and update the closest player target based on the distance
    private void LocateClosestTarget()
    {
        // Find all game objects tagged with "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

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
