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

    public GameObject Chip, HealthBonus, WeaponBonus;
    public V2SpriteManager sm;
    private int chipCountStart = 0;

    public float deathScalingDuration = 0.3f;  // Duration of the gradual enemy decrease in scale for death animation
    private bool isDying = false; // Ensures that death logic is only triggered once so as to avoid multiple animations

    private int takingDamage = 0;
    private GameObject dmgTextPrefab;
    private Transform dmgTextCanvas;
    private int dmgTakeInterval = 30; // take dmg every 0.5 seconds
    private int lastDmgFrame = 0;
    private int currentFrame = 0;
    public int randomDamage = 5;
    Animator animator;
    Rigidbody2D rb;

    public bool Enemy1 = false;
    public bool Enemy2 = false;
    public bool Boss = false;

    private string[] deathSounds = { "EnemyDeath1", "EnemyDeath2", "EnemyDeath3", "EnemyDeath4" };
    private string[] damageSounds = { "PlayerHitEnemy1", "PlayerHitEnemy2", "PlayerHitEnemy3" };

    private void Start()
    {   
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Disable rotation and up-axis updating
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // Set agent's speed to specified movement speed
        agent.speed = speed;

        // Repeatedly find the closest player at regular intervals
        InvokeRepeating(nameof(LocateClosestTarget), 0f, targetUpdateInterval);
        
    }
    private void FixedUpdate()
    {
        currentFrame++;
        if (takingDamage > 0 && currentFrame > lastDmgFrame + dmgTakeInterval)
        {
            lastDmgFrame = currentFrame;
            TakeDamage(takingDamage);
            
        }
    }
    private void Update()
    {
        if (Enemy1){
            float verticalVelocity = agent.velocity.y;
            animator.SetFloat("yVelocity", verticalVelocity);
        }
        if (Enemy2){
            float xVelocity = agent.velocity.x;
            animator.SetFloat("xVelocity", xVelocity);
        }
        if (Boss){
            if (chipCountStart < sm.chipCount){
                health = health - (sm.chipCount - chipCountStart);
            }
            float xVelocity = agent.velocity.x;
            float verticalVelocity = agent.velocity.y;
            animator.SetFloat("xV", xVelocity);
            animator.SetFloat("yV", verticalVelocity);
        }
        chipCountStart = sm.chipCount;
        if (target != null) //if the target exists
        {
            // Move agent towards target pos
            agent.SetDestination(target.position);
        }
    }
    public void TakeDamage(int dmg)
    {
        int newDmg = dmg + Random.Range(0, randomDamage);
        health -= newDmg;
        //Debug.Log($"Enemy health: {health}");
        if (health <= 0 && !isDying) {
            KillEnemy();
        }
        GameObject text = Instantiate(dmgTextPrefab, dmgTextCanvas);
        text.GetComponent<DmgText>().SetDmgPos(newDmg, transform.position);
        
        // Randomize an index to select a sound
        int randomIndex = Random.Range(0, damageSounds.Length);
        // Debug.Log($"Random Index: {randomIndex}"); // Log the index for debugging

        SoundManager.instance.Play(damageSounds[randomIndex]);
    }
    public void SetDmgTextInfo(GameObject dmgTextPrefab, Transform dmgTextCanvas)
    {
        this.dmgTextCanvas = dmgTextCanvas;
        this.dmgTextPrefab = dmgTextPrefab;
    }
    public void AddEnemyTakingDmg(int dmg)
    {
        takingDamage += dmg;
    }
    public void RemoveEnemyTakingDmg(int dmg)
    {
        takingDamage -= dmg;
        takingDamage = Mathf.Max(takingDamage, 0);
    }
    // trigger enemy death when hp <= 0 
    private void KillEnemy()
    {
        isDying = true;  // This bool ensures the death logic only triggers once
        
        if(!Boss){
            // Randomize item drop
            float v = Random.value;
            if (v <= .75)
            {
                Instantiate(Chip, transform.position, Quaternion.identity); // Instantiate the item drop at the enemy's position
                Debug.Log("Item dropped (instantiated)" + v.ToString());
            }
            if (v > .75 && v <= .95)
            {
                Instantiate(HealthBonus, transform.position, Quaternion.identity);
            }
            if (v > .95)
            {
                Instantiate(WeaponBonus, transform.position, Quaternion.identity);
            }
        }
        
        
        
        // Instantiate(itemDropPrefab, transform.position, Quaternion.identity); // Instantiate the item drop at the enemy's position
        
        // trigger randomized enemy death sound
        int randomIndex = Random.Range(0, deathSounds.Length);
        SoundManager.instance.Play(deathSounds[randomIndex]);
                
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

