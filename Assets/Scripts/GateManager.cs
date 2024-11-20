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
    private bool wallInstantiated4 = false;

    public GameObject hay, wood;
    public GameObject lobbyExit, l1Enter;
    public GameObject l1Exit, l2Enter;
    public GameObject l2Exit, l3Enter;
    public GameObject l3Exit, worldEnter;

    public GameObject gate1, gate2, gate3, gate4;

    private GameObject currentWall1, currentWall2, currentWall3, currentWall4;

    void Start()
    {
        // trigger background audio for lobby level
        SoundManager.instance.FadeIn("LobbyMusic", 2f);
        SoundManager.instance.FadeIn("LobbyAmbientWind", 2f);

        // initialize wall bools
        currentWall1 = Instantiate(gate1, gate1.transform.position, Quaternion.identity);
        wallInstantiated1 = true;
        currentWall2 = Instantiate(gate2, gate2.transform.position, Quaternion.identity);
        wallInstantiated2 = true;
        currentWall3 = Instantiate(gate3, gate3.transform.position, Quaternion.identity);
        wallInstantiated3 = true;
        currentWall4 = Instantiate(gate4, gate4.transform.position, Quaternion.identity);
        wallInstantiated4 = true;
    }

    public void Act(GameObject gate){
        string name = gate.name;
        if (name == "LobbyExit" || name == "L1Exit" || name == "L2Exit" || name == "L3Exit"){
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
            SoundManager.instance.Play("GateOpening");
            Destroy(currentWall1);
            wallInstantiated1 = false;
            SoundManager.instance.FadeOut("LobbyAmbientWind", 10f);
            SoundManager.instance.Crossfade("LobbyMusic", "Gameplay-Instrumental1", 3f); // (soundOut, soundIn, duration)

        }
        if (currentWall2 != null && gate == "L1Exit")
        {
            SoundManager.instance.Play("GateOpening");
            Destroy(currentWall2);
            wallInstantiated2 = false;
            SoundManager.instance.Crossfade("Gameplay-Instrumental1", "Gameplay-Instrumental2", 3f);

        }
        if (currentWall3 != null && gate == "L2Exit")
        {
            SoundManager.instance.Play("GateOpening");
            Destroy(currentWall3);
            wallInstantiated3 = false;
            SoundManager.instance.Crossfade("Gameplay-Instrumental2", "Gameplay-Instrumental3", 3f);

        }
        if (currentWall4 != null && gate == "L3Exit")
        {
            SoundManager.instance.Play("GateOpening");
            Destroy(currentWall4);
            wallInstantiated4 = false;
            SoundManager.instance.FadeOut("Gameplay-Instrumental3", 2f);
            //
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
        if (!wallInstantiated3 && gate3 != null && gate == "L3Enter")
        {
            currentWall3 = Instantiate(gate3, gate3.transform.position, Quaternion.identity);
            wallInstantiated3 = true;
        }
        if (!wallInstantiated4 && gate4 != null && gate == "WorldEnter")
        {
            currentWall4 = Instantiate(gate4, gate4.transform.position, Quaternion.identity);
            wallInstantiated4 = true;
        }

    }

}
