using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Tooltip("Initial health points of the object.")]
    [SerializeField] private int healthPoints = 3;
    private bool EnemyIsTouching = false;
    private int damageTimer = 60;

    private void FixedUpdate()
    {
        damageTimer = (damageTimer + 1) % 60;
        if (damageTimer == 0 && EnemyIsTouching)
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        healthPoints--;
        if (healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyIsTouching = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyIsTouching = false;
        }
    }
}