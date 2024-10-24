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
    public GameObject itemDropPrefab;
    public float itemDropChance; // (set in `EnemySpawn`) chance for enemy to drop item
    public float deathScalingDuration = 0.3f;  // Duration of the gradual enemy decrease in scale for death animation
    private bool isDying = false; // Ensures that death logic is only triggered once so as to avoid multiple animations

    


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
        if (health <= 0 && !isDying) {
            KillEnemy();
        }

    }

    // trigger enemy death when hp <= 0 
    private void KillEnemy()
    {
        isDying = true;  // This bool ensures the death logic only triggers once

        Debug.Log("-Random.value-to check if enemy drops item");
        
        // Randomize item drop
        float v = Random.value;
        if (v <= itemDropChance)
        {
            Instantiate(itemDropPrefab, transform.position, Quaternion.identity); // Instantiate the item drop at the enemy's position
            Debug.Log("Item dropped (instantiated)" + v.ToString() + "  " + itemDropChance.ToString());
        }
        else
        {
            Debug.Log("No item dropped from enemy");
        }
        
        
        // Instantiate(itemDropPrefab, transform.position, Quaternion.identity); // Instantiate the item drop at the enemy's position
        // Start the enemy death animation
        StartCoroutine(ScaleDownAndDestroy());
    }

    // used to animate enemy death (gradually scales gameobject down until it is eventually destroyed.)
    private IEnumerator ScaleDownAndDestroy()
    {
        float elapsedTime = 0f;
        Vector3 originalScale = transform.localScale;

        while (elapsedTime < deathScalingDuration)
        {
            // Easing fn to make scaling down faster initially
            float t = elapsedTime / deathScalingDuration;
            t = t * t * t * t; // Quadratic "easing"

            // Scale the enemy down exponentially over time
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            elapsedTime += Time.deltaTime;
            yield return null;  // Wait until the next frame
        }

        // Ensure the enemy is completely scaled down before destroying
        transform.localScale = Vector3.zero;

        // Destroy the enemy after the animation
        Destroy(gameObject);
        Debug.Log("Enemy destroyed after death animation");
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

