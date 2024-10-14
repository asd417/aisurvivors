using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Prefab of the enemy sprite to instantiate
    public GameObject spriteEnemyPrefab;
    public int spawnx1;
    public int spawny1;
    public int spawnx2;
    public int spawny2;
    public int numberEnemy;

/*
    void Start()
    {
        // Instantiate a finite number of sprites and add them to the list
        for (int i = numberEnemy; i <= numberEnemy; i++){
            Vector3 initialPosition;
            int spawnPoint = Random.Range(0,2);

            if (spriteEnemyPrefab != null)
            {
                if (spawnPoint == 0){
                    initialPosition = new Vector3(spawnx1, spawny1);
                }
                else if (spawnPoint == 1){
                    initialPosition = new Vector3(spawnx2, spawny2);
                }
                else{
                    initialPosition = new Vector3(0, 0);
                }
                
            
            // Instantiate the sprite prefab at a valid position on the 2D NavMesh
                GameObject newEnemySprite = Instantiate(spriteEnemyPrefab, initialPosition, Quaternion.identity);
            }
        }
    }

    void Update()
    {
        
    }
    */
    void Start()
    {
        StartCoroutine(SpawnEnemiesWithDelay());
    }

    IEnumerator SpawnEnemiesWithDelay()
    {

        for (int i = 0; i < numberEnemy; i++)
        {
            Vector3 initialPosition;
            int spawnPoint = Random.Range(0, 2);

            if (spriteEnemyPrefab != null)
            {
                if (spawnPoint == 0)
                {
                    initialPosition = new Vector3(spawnx1, spawny1);
                }
                else
                {
                    initialPosition = new Vector3(spawnx2, spawny2);
                }

                // Instantiate the sprite prefab at a valid position
                Instantiate(spriteEnemyPrefab, initialPosition, Quaternion.identity);
            }

            // Wait for 0.5 seconds before spawning the next enemy
            yield return new WaitForSeconds(1f);
        }
    }
}
