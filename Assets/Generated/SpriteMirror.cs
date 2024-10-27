using UnityEngine;

public class SpriteMirror : MonoBehaviour
{
    [Tooltip("Speed threshold to determine direction change.")]
    [SerializeField] private float speedThreshold = 0.1f;

    private SpriteRenderer spriteRenderer;
    private Vector3 lastPosition;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        float deltaX = currentPosition.x - lastPosition.x;

        if (Mathf.Abs(deltaX) > speedThreshold)
        {
            if (deltaX > 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (deltaX < 0)
            {
                spriteRenderer.flipX = false;
            }
        }

        lastPosition = currentPosition;
    }
}