using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    //Player class to control different aspects of player prefab with script
    [SerializeField]
    private Color playerColor = Color.white;
    private Color highlightColor = Color.white;
    public float playerDeathDuration = 0.3f;  // Duration of gradual scale decrease for death animation
    public SpriteRenderer sr;

    public Weapon weapon;
    public GameObject fogSprite1;
    //public GameObject fogSprite2;
    public float fogScale = 2.5f;
    private float targetFogScale = 2.5f;
    private float fogScaleTime = 0.0f;
    public void SetFogScale(float scale)
    {
        fogSprite1.transform.localScale = new Vector3(scale, scale, scale);
        //fogSprite2.transform.localScale = new Vector3(scale, scale, scale);
    }
    public void ResetFogScale()
    {
        fogSprite1.transform.localScale = new Vector3(fogScale, fogScale, fogScale);
        //fogSprite2.transform.localScale = new Vector3(fogScale, fogScale, fogScale);
    }

    public void InterpolateFogScale(float _fogScale, float timeMultiplier)
    {
        targetFogScale = _fogScale;
        fogScaleTime = timeMultiplier;
    }

    private void Update()
    {
        fogScale = Mathf.Lerp(fogScale, targetFogScale, fogScaleTime);
        SetFogScale(fogScale);
    }

    private void Start()
    {
        setHighlighted(false);
    }

    public void setHighlighted(bool highlighted)
    {
        if (highlighted)
        {
            SoundManager.instance.Play("PlayerSelect1");
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
        foreach (Transform c in transform)
        {
            Weapon w = c.GetComponent<Weapon>();
            if (w != null)
            {
                w.Detach();
            }
        }
        if(weapon != null) weapon.Detach();
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
        // Debug.Log("Player destroyed after death animation");
    }
}
