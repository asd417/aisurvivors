using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class LevelSwitch : MonoBehaviour
{
    public V2SpriteManager spriteMove;
    public GateManager manager;

    public bool hay = false;
    public bool wood = false;
    public bool L1Enter = false;
    public bool L2Enter = false;
    public bool L3Enter = false;
    public bool WorldEnter = false;
    public bool Boss = false;

    public bool enemyR1 = false;
    public bool enemyR2 = false;
    public bool enemyR3 = false;
    public bool bossGo = false;

    public GameObject wallPrefab; 
    public static GameObject currentWall;

    private int playersAtDoor = 0;

    int spriteCount;

    

    void Start()
    {
        if (manager == null)
        {
            manager = FindObjectOfType<GateManager>();  // Find the GateManager in the scene
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    { 
        spriteCount = spriteMove.GetAgentCount();
        if (IsPlayer(collider))
        {
            playersAtDoor++;
            if(Boss) {
                bossGo = true;
                SoundManager.instance.Play("BossAlarm"); // need to test
            }
            if (playersAtDoor == spriteMove.GetAgentCount())
            {
                if (spriteCount != 0 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    enemyR1 = false;
                    enemyR2 = false;
                    enemyR3 = false;
                    bossGo = false;
                    if (manager != null)
                    {
                        Debug.Log("Calling Act on GameObject: " + this.gameObject.name);
                        manager.Act(this.gameObject);  // Pass the GameObject correctly
                    }
                    else
                    {
                        Debug.LogError("GateManager (manager) is not assigned!");
                    }
                    if(L1Enter) enemyR1 = true;
                    if(L2Enter) enemyR2 = true;
                    if(L3Enter) enemyR3 = true;
                }
            }
        }
        if (hay) SceneManager.LoadScene("Story");
        if (wood) SceneManager.LoadScene("Info");
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        
        if (IsPlayer(collider) && (!L1Enter && !L2Enter && !L3Enter))
        {
            playersAtDoor--;
        }
    }

    private void Update()
    {
        if (!spriteMove)
        {
            GameObject obj = GameObject.Find("SpriteManager");
            if (obj) spriteMove = obj.GetComponent<V2SpriteManager>();
        }
        if (spriteMove.GetAgentCount() == 0 && spriteMove && GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            Lose();
        }
    }

    void Lose()
    {
        SceneManager.LoadScene("Lose");
        SoundManager.instance.StopAllSounds();
        SoundManager.instance.FadeIn("MenuPlayAnimation3", 1f);
        SoundManager.instance.FadeIn("LightBuzz", 10f);
    }

    private bool IsPlayer(Collider2D collider)
    {
        return collider.CompareTag("Player1") || collider.CompareTag("Player2") || collider.CompareTag("Player3");
    }
}
