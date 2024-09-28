using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Tooltip("Initial health points of the object.")]
    [SerializeField] private int healthPoints = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            healthPoints--;

            if (healthPoints <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}