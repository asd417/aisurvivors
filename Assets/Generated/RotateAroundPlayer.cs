using UnityEngine;

public class RotateAroundPlayer : MonoBehaviour
{
    [Tooltip("Speed at which the sprite rotates around the player.")]
    [SerializeField] private float rotationSpeed = 5f;

    private Transform targetPlayer;
    private bool isAttached = false;

    void Update()
    {
        if (isAttached && targetPlayer != null)
        {
            transform.RotateAround(targetPlayer.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            targetPlayer = collision.transform;
            isAttached = true;
        }
    }
}