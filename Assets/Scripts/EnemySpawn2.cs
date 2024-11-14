using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn2 : MonoBehaviour
{
    public GameObject spriteEnemyPrefab1;
    public GameObject spriteEnemyPrefab2;
    public GameObject bossEnemy;

    public int spawnx1, spawny1, spawnx2, spawny2;
    public int spawnx3, spawny3, spawnx4, spawny4;
    public int spawnx5, spawny5, spawnx6, spawny6;
    public int spawnBx, spawnBy;

    public int numberEnemy1, numberEnemy2, numberEnemy3;

    public LevelSwitch levelSwitch1;
    public LevelSwitch levelSwitch2;
    public LevelSwitch levelSwitch3;
    public LevelSwitch bossTrigger;

    private bool isSpawningEnemies1 = false;
    private bool isSpawningEnemies2 = false;
    private bool isSpawningEnemies3 = false;
    private bool isBossSpawned = false;

    public bool triggered = false;

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
        if (levelSwitch3.enemyR3 && !isSpawningEnemies3)
        {
            StartCoroutine(SpawnEnemiesWithDelay3());
            isSpawningEnemies3 = true;
        }
        if (bossTrigger.bossGo && !isBossSpawned){
            BossSpawn();
            isBossSpawned = true;
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
            
            if (selectedPrefab != null)
            {
                GameObject em = Instantiate(selectedPrefab, initialPosition, Quaternion.identity);
                //em.GetComponent<Enemy>().itemDropChance = 1.0f;
            }

            yield return new WaitForSeconds(1f);
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
            
            
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator SpawnEnemiesWithDelay3()
    {
        for (int i = 0; i < numberEnemy3; i++)
        {
            Vector3 initialPosition = (Random.Range(0, 2) == 0) 
                ? new Vector3(spawnx5, spawny5)
                : new Vector3(spawnx6, spawny6);

            GameObject selectedPrefab = (Random.value < 0.666f) ? spriteEnemyPrefab1 : spriteEnemyPrefab2;
            Instantiate(selectedPrefab, initialPosition, Quaternion.identity);
        
            yield return new WaitForSeconds(1f);
        }
    }

    public void BossSpawn(){
        Vector3 initialPosition = new Vector3(spawnBx, spawnBy);
        Instantiate(bossEnemy, initialPosition, Quaternion.identity);
    }
}