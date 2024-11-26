using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private float delayBeforeChange = 5f;
    public bool isControls = false;

    private void Start()
    {
        if (!isControls){
            SoundManager.instance.StopAllSounds();
            StartCoroutine(PlayIntroAnimationSounds());
            StartCoroutine(ChangeSceneToLobbyAfterDelay());  
        } 
    }

    private IEnumerator ChangeSceneToLobbyAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeChange);
        if (isControls){
            Destroy(gameObject);
        }
        else{
            SceneManager.LoadScene("Level2 1");
            SoundManager.instance.FadeOut("MenuAnimation3", 3f);
        }
        

    }




    private IEnumerator PlayIntroAnimationSounds() // used for Title scene change (intro animation)
    {
        if (!isControls){
            // trigger 3rd background sfx first
            SoundManager.instance.Play("MenuPlayAnimation3");

            // slashes sfx
            SoundManager.instance.Play("MenuPlayAnimation1");
            yield return new WaitForSeconds(2f); // duration to wait between sounds

            // crescendo
            SoundManager.instance.Play("MenuPlayAnimation2"); // Replace with your second sound name
        }
        
    }
}
