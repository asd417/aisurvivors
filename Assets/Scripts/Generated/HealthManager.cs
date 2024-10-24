using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Tooltip("Initial health points of the object.")]
    [SerializeField] private int healthPoints = 3;

    [Tooltip("Damage that the player takes upon enemy contact")]
    [SerializeField] public int dmg = 1;

    private bool EnemyIsTouching = false;
    private int damageTimer = 60;


    private void FixedUpdate()
    {
        damageTimer = (damageTimer + 1) % 60;
        if (damageTimer == 0 && EnemyIsTouching)
        {
            TakeDamage(dmg);
        }
    }

    private void TakeDamage(int dmg)
    {
        healthPoints -= dmg;
        if (healthPoints <= 0)
        {
            Player p = gameObject.GetComponent<Player>(); // Get the Player component
            if (p != null) {
                p.KillPlayer(); // Call the KillPlayer method on the Player component
            }
            else {
                Debug.LogError("Player component not found on this GameObject!");
            }        
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