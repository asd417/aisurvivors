using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Prefab of the first enemy sprite to instantiate
    [Tooltip("Prefab of the first enemy sprite to instantiate")]
    public GameObject spriteEnemyPrefab1;

    // Prefab of the second enemy sprite to instantiate
    [Tooltip("Prefab of the second enemy sprite to instantiate")]
    public GameObject spriteEnemyPrefab2;

    [Tooltip("X coordinate for the first spawn point")]
    public int spawnx1;

    [Tooltip("Y coordinate for the first spawn point")]
    public int spawny1;

    [Tooltip("X coordinate for the second spawn point")]
    public int spawnx2;

    [Tooltip("Y coordinate for the second spawn point")]
    public int spawny2;

    [Tooltip("Number of enemies to spawn")]
    public int numberEnemy;

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

            if (spawnPoint == 0)
            {
                initialPosition = new Vector3(spawnx1, spawny1);
            }
            else
            {
                initialPosition = new Vector3(spawnx2, spawny2);
            }

            GameObject selectedPrefab = Random.value < 0.666f ? spriteEnemyPrefab1 : spriteEnemyPrefab2;

            if (selectedPrefab != null)
            {
                GameObject em = Instantiate(selectedPrefab, initialPosition, Quaternion.identity);
                em.GetComponent<Enemy>().itemDropChance = 0.5f;
            }

            yield return new WaitForSeconds(1f);
        }
    }
}