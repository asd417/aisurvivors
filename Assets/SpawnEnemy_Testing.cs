using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemy_Testing : MonoBehaviour
{
    // Prefab of the enemy sprite to instantiate
    public GameObject spriteEnemyPrefab;
    public int enemySpawnLocation_xval;

    void Start()
    {
        // Instantiate a finite number of sprites and add them to the list
        if (spriteEnemyPrefab != null)
        {
            Vector3 initialPosition = new Vector3(enemySpawnLocation_xval, 0, 0);
            
            // Instantiate the sprite prefab at a valid position on the 2D NavMesh
            GameObject newEnemySprite = Instantiate(spriteEnemyPrefab, initialPosition, Quaternion.identity);
        }
    }

    void Update()
    {
        
    }
}