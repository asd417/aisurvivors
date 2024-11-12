using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn2 : MonoBehaviour
{
    public GameObject spriteEnemyPrefab1;
    public GameObject spriteEnemyPrefab2;

    public int spawnx1, spawny1, spawnx2, spawny2;
    public int spawnx3, spawny3, spawnx4, spawny4;

    public int numberEnemy1, numberEnemy2;

    public LevelSwitch levelSwitch1;
    public LevelSwitch levelSwitch2;

    private bool isSpawningEnemies1 = false;
    private bool isSpawningEnemies2 = false;

    void Start()
    {
        
    }

    void Update(){
        if (levelSwitch1.enemyR1 && !isSpawningEnemies1)
        {
            StartCoroutine(SpawnEnemiesWithDelay1());
            isSpawningEnemies1 = true;
        }
        if (levelSwitch2.enemyR2 && !isSpawningEnemies2)
        {
            StartCoroutine(SpawnEnemiesWithDelay2());
            isSpawningEnemies2 = true;
        }
    }

    IEnumerator SpawnEnemiesWithDelay1()
    {
        for (int i = 0; i < numberEnemy1; i++)
        {
            Vector3 initialPosition = (Random.Range(0, 2) == 0) 
                ? new Vector3(spawnx1, spawny1)
                : new Vector3(spawnx2, spawny2);

            GameObject selectedPrefab = (Random.value < 0.666f) ? spriteEnemyPrefab1 : spriteEnemyPrefab2;
            Instantiate(selectedPrefab, initialPosition, Quaternion.identity);
            
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator SpawnEnemiesWithDelay2()
    {
        for (int i = 0; i < numberEnemy2; i++)
        {
            Vector3 initialPosition = (Random.Range(0, 2) == 0) 
                ? new Vector3(spawnx3, spawny3)
                : new Vector3(spawnx4, spawny4);

            GameObject selectedPrefab = (Random.value < 0.666f) ? spriteEnemyPrefab1 : spriteEnemyPrefab2;
            Instantiate(selectedPrefab, initialPosition, Quaternion.identity);
            
            yield return new WaitForSeconds(.5f);
        }
    }
}