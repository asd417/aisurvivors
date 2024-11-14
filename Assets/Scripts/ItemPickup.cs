using System.Collections;
using System.Collections.Generic;
using System.Linq; // for concat
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public float pickupRadius = 0.25f;  // Distance within which the player can pick up the item
    public float pickupAnimationSpeed = 2f;  // Speed of the pickup animation

    void Start() {
    }


    void Update()
    {
        Transform player = FindClosestPlayer();

        if (player != null && Vector2.Distance(transform.position, player.position) < pickupRadius)
        {
            PickUpItem(player);
        }
    }

    // Finds the closest player among the three
    Transform FindClosestPlayer() {
        // Collect players with individual tags
        GameObject[] player1 = GameObject.FindGameObjectsWithTag("Player1");
        GameObject[] player2 = GameObject.FindGameObjectsWithTag("Player2");
        GameObject[] player3 = GameObject.FindGameObjectsWithTag("Player3");

        // Combine all players into one array
        GameObject[] players = player1.Concat(player2).Concat(player3).ToArray();
        
        float closestDistance = Mathf.Infinity;

        foreach (GameObject p in players)
        {
            float distance = Vector2.Distance(transform.position, p.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                return p.transform;
            }
        }
        return null;
    }

    // Function to handle picking up the item
    void PickUpItem(Transform player)
    {
        // Basic animation: the item moves towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, pickupAnimationSpeed * Time.deltaTime);

        // Once the item reaches the player, pick it up
        if (Vector2.Distance(transform.position, player.position) < 10f)
        {
            // Add item to the player's inventory (if you have an inventory system) or trigger any other logic here
            Debug.Log("Item picked up by: " + player.name);
            Destroy(gameObject);
        }
    }
}
