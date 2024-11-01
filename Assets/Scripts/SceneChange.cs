using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private float delayBeforeChange = 5f;

    private void Start()
    {
        SoundManager.instance.StopAllSounds();
        StartCoroutine(PlayIntroAnimationSounds());
        StartCoroutine(ChangeSceneToLobbyAfterDelay());   
    }

    private IEnumerator ChangeSceneToLobbyAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeChange);
        SceneManager.LoadScene("Lobby");

    }




    private IEnumerator PlayIntroAnimationSounds() // used for Title scene change (intro animation)
    {
        // slashes sfx
        SoundManager.instance.Play("MenuPlayAnimation1");
        yield return new WaitForSeconds(2f); // duration to wait between sounds

        // crescendo
        SoundManager.instance.Play("MenuPlayAnimation2"); // Replace with your second sound name
    }
}