using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Soundtrack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SoundManager.instance != null) {
            SoundManager.instance.StopAllSounds();
            SoundManager.instance.FadeIn("Gameplay-Instrumental1", 3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
