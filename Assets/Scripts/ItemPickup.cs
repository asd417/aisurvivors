using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public float pickupRange = 10.5f; // Distance at which item starts moving towards player
    public float attractionSpeed = 10.5f; // Speed at which item moves towards the player
    public float pickupDistance = 1.0f; // Distance at which item is considered "picked up"

    void Update()
    {
        // Find the closest player
        Transform closestPlayer = FindClosestPlayer();
        if (closestPlayer != null)
        {
            // Calculate distance to closest player
            float distanceToPlayer = Vector2.Distance(transform.position, closestPlayer.position);

            // Start moving towards the player if within pickup range
            if (distanceToPlayer <= pickupRange)
            {
                // Move the item towards the player
                Vector2 direction = (closestPlayer.position - transform.position).normalized;
                transform.position += (Vector3)direction * attractionSpeed * Time.deltaTime;

                // Check if item is close enough to be picked up
                if (distanceToPlayer <= pickupDistance)
                {
                    PickUp();
                }
            }
        }
    }

    // Method to handle the pickup logic (can be expanded for adding to inventory, etc.)
    void PickUp()
    {
        // Here you could add effects, update inventory, etc.
        Destroy(gameObject); // Destroy the item to simulate pickup
    }

    // Finds the closest player among the three
    Transform FindClosestPlayer()
    {
        // Collect players with individual tags
        GameObject[] player1 = GameObject.FindGameObjectsWithTag("Player1");
        GameObject[] player2 = GameObject.FindGameObjectsWithTag("Player2");
        GameObject[] player3 = GameObject.FindGameObjectsWithTag("Player3");

        // Combine all players into one array
        GameObject[] players = player1.Concat(player2).Concat(player3).ToArray();

        Transform closestPlayer = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject p in players)
        {
            float distance = Vector2.Distance(transform.position, p.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = p.transform;
            }
        }
        return closestPlayer;
    }
}
