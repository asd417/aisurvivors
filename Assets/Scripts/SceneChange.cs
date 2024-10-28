using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private float delayBeforeChange = 5f;

    private void Start()
    {
        StartCoroutine(PlayIntroAnimationSFX());
        StartCoroutine(ChangeSceneToLobbyAfterDelay());
        
    }

    private IEnumerator PlayIntroAnimationSFX()
    {
        // sword slashing
        SoundManager.instance.Play("MenuPlayAnimation1");
        yield return new WaitForSeconds(2f); // duration to wait between sounds

        // crescendo
        SoundManager.instance.Play("MenuPlayAnimation2"); // Replace with your second sound name
    }
    private IEnumerator ChangeSceneToLobbyAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeChange);

        // trigger lobby background sfx/soundtracks, then load scene:
        SoundManager.instance.FadeIn("LobbyAmbientWind", 1f);
        SoundManager.instance.FadeIn("LobbyMusic", 1f);
        SceneManager.LoadScene("Lobby");
    }
}
