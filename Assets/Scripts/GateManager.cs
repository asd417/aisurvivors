using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class GateManager : MonoBehaviour
{
    private bool wallInstantiated1 = false;
    private bool wallInstantiated2 = false;
    private bool wallInstantiated3 = false;

    public GameObject hay, wood;
    public GameObject lobbyExit, l1Enter;
    public GameObject l1Exit, l2Enter;
    public GameObject l2Exit;

    public GameObject gate1, gate2, gate3;

    private GameObject currentWall1, currentWall2, currentWall3;

    void Start()
    {
        currentWall1 = Instantiate(gate1, gate1.transform.position, Quaternion.identity);
        wallInstantiated1 = true;
        currentWall2 = Instantiate(gate2, gate2.transform.position, Quaternion.identity);
        wallInstantiated2 = true;
        currentWall3 = Instantiate(gate3, gate3.transform.position, Quaternion.identity);
        wallInstantiated3 = true;
    }

    void Update()
    {
        
    }

    public void Act(GameObject gate){
        string name = gate.name;
        if (name == "LobbyExit" || name == "L1Exit" || name == "L2Exit"){
            MoveOn(name);
        }
        else{
            Wall(name);
        }
    }

    void MoveOn(string gate)
    {
        if (currentWall1 != null && gate == "LobbyExit")
        {
            Destroy(currentWall1);
            wallInstantiated1 = false;
        }
        if (currentWall2 != null && gate == "L1Exit")
        {
            Destroy(currentWall2);
            wallInstantiated2 = false;
        }
        if (currentWall3 != null && gate == "L2Exit")
        {
            Destroy(currentWall3);
            wallInstantiated3 = false;
        }
        
    }

    void Wall(string gate)
    {
        if (!wallInstantiated1 && gate1 != null && gate == "L1Enter")
        {
            currentWall1 = Instantiate(gate1, gate1.transform.position, Quaternion.identity);
            wallInstantiated1 = true;
        }
        if (!wallInstantiated2 && gate2 != null && gate == "L2Enter")
        {
            currentWall2 = Instantiate(gate2, gate2.transform.position, Quaternion.identity);
            wallInstantiated2 = true;
        }

    }

}
