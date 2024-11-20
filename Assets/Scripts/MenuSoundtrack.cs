using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundtrack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SoundManager.instance != null) {
            SoundManager.instance.StopAllSounds();
            SoundManager.instance.FadeIn("preMainMenuMusic", 1f);
            StartCoroutine(WaitAndFadeInNext()); // this stops pre and plays primary main menu music after
        }
    }

    private IEnumerator WaitAndFadeInNext()
    {
        // Wait for 22 seconds
        yield return new WaitForSeconds(25f);

        // Execute the fadein (1s) after the wait
        SoundManager.instance.FadeIn("MainMenuMusic", 1f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
