using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySoundtrack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SoundManager.instance != null) {
            SoundManager.instance.StopAllSounds();
            SoundManager.instance.FadeIn("LobbyMusic", 3f);
            SoundManager.instance.FadeIn("LobbyAmbientWind", 3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
