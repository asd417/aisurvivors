using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player class to control different aspects of player prefab with script
    [SerializeField]
    public Color playerColor = Color.white;
    public Color highlightColor = Color.green;
    public float playerDeathDuration = 0.3f;  // Duration of gradual scale decrease for death animation

    private void Start()
    {
        setHighlighted(false);
    }

    public void setHighlighted(bool highlighted)
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        if (highlighted)
        {
            sr.color = highlightColor;
        }
        else
        {
            sr.color = playerColor;
        }
    }

    // trigger player death when hp <= 0 
    public void KillPlayer()
    {        
        // maybe add player's weapon drop logic here: ...

        // Start the death animation
        StartCoroutine(PlayerScaleDownAndDestroy());
    }

    // used to animate death (gradually scales gameobject down until it is eventually destroyed.)
    public IEnumerator PlayerScaleDownAndDestroy()
    {
        float elapsedTime = 0f;
        Vector3 originalScale = transform.localScale;

        while (elapsedTime < playerDeathDuration)
        {
            // Easing fn to make scaling down faster initially
            float t = elapsedTime / playerDeathDuration;
            t = t * t * t * t; // Quadratic "easing"

            // Scale down exponentially, over time
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            elapsedTime += Time.deltaTime;
            yield return null;  // Wait until the next frame
        }

        // Ensure the player is completely scaled down before destroying
        transform.localScale = Vector3.zero;

        // Destroy the player after the animation
        Destroy(gameObject);
        Debug.Log("Player destroyed after death animation");
    }
}
