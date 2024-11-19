using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdle : MonoBehaviour
{
    public float rotationAngle = 15f;   // Maximum rotation angle (in degrees) for tilting
    public float rotationSpeed = 2f;    // Speed of the tilt oscillation

    void Update()
    {
        // Oscillate rotation (tilt side to side)
        float rotation = Mathf.Sin(Time.time * rotationSpeed) * rotationAngle;
        transform.localRotation = Quaternion.Euler(0, 0, rotation);

        // Bounce up and down relative to the starting position
        //float bounce = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        //transform.position = new Vector3(transform.position.x, transform.position.y + bounce, transform.position.z);
    }
}

