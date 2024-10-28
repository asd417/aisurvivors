using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private float delayBeforeChange = 5f;

    private void Start()
    {
        StartCoroutine(ChangeSceneToLobbyAfterDelay());   
    }

    private IEnumerator ChangeSceneToLobbyAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeChange);
        SceneManager.LoadScene("Lobby");
    }
}
