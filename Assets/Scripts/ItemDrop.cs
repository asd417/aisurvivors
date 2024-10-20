using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public float animationDuration = 1.0f;
    public Vector3 startScale = new Vector3(0.01f, 0.01f, 0.01f);
    public Vector3 endScale = Vector3.one; // Default scale

    private float elapsedTime = 0f;

    private void Start()
    {
        // Set the item to the start scale initially
        transform.localScale = startScale;
    }

    private void Update()
    {
        // Animate the scaling over time
        if (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / animationDuration;
            transform.localScale = Vector3.Lerp(startScale, endScale, progress);
        }
        else
        {
            // Ensure the item is set to the final scale after animation
            transform.localScale = endScale;
        }
    }
}
