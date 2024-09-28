using UnityEngine;
using UnityEngine.UIElements;

public class RotateAroundPlayer : MonoBehaviour
{
    [Tooltip("Speed at which the sprite rotates around the player.")]
    private float rotationSpeed = 1f;

    private Transform targetPlayer;
    private bool isAttached = false;
    private float angle_euler = 0;
    private int dist = 2;

    void Update()
    {
        if (isAttached && targetPlayer != null)
        {
            angle_euler = (angle_euler + rotationSpeed) % 360;
            transform.eulerAngles = new Vector3 (0,0, angle_euler);
            transform.position = targetPlayer.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle_euler) * dist, Mathf.Sin(Mathf.Deg2Rad * angle_euler) * dist, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");
        if (collision.CompareTag("Player"))
        {
            targetPlayer = collision.transform;
            isAttached = true;
        }
    }
}