using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdle : MonoBehaviour
{
    public float rotationAngle = 15f;   // Maximum rotation angle (in degrees) for tilting
    public float rotationSpeed = 2f;    // Speed of the tilt oscillation
    public float bounceHeight = 1f;   // Maximum height for the bounce
    public float bounceSpeed = 1f;      // Speed of the bounce

    private Vector3 startPosition;      // Initial position of the item

    void Start()
    {
        // Record the starting position of the item
        startPosition = transform.position;
    }

    void Update()
    {   // item idle animation currently bugged
        // // Oscillate rotation (tilt side to side)
        // float rotation = Mathf.Sin(Time.time * rotationSpeed) * rotationAngle;
        // transform.localRotation = Quaternion.Euler(0, 0, rotation);

        // // Bounce up and down relative to the starting position
        // float bounce = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        // transform.position = new Vector3(startPosition.x, startPosition.y + bounce, startPosition.z);
    }
}

