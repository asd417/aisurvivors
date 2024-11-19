using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Tooltip("Initial health points of the object.")]
    [SerializeField] public int healthPoints = 20;
    public int currentHealth;

    [Tooltip("Damage that the player takes upon enemy contact")]
    [SerializeField] public int dmg = 1;

    private bool EnemyIsTouching = false;
    private int damageTimer = 60;
    public Healthbar healthBar = null;

    private void Start()
    {
        currentHealth = healthPoints;
    }

    private void FixedUpdate()
    {
        damageTimer = (damageTimer + 1) % 20;
        if (damageTimer == 0 && EnemyIsTouching)
        {
            TakeDamage(dmg);
        }
        if (healthBar)
        {
            healthBar.transform.position = transform.position + Vector3.up * 1;
        }
    }

    private void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (healthBar)
        {
            healthBar.DisplayHealth(healthPoints, currentHealth);
        }
        if (currentHealth <= 0)
        {
            Player p = gameObject.GetComponent<Player>(); // Get the Player component
            if (p != null) {
                p.KillPlayer(); // Call the KillPlayer method on the Player component
                if(healthBar)
                {
                    Destroy(healthBar.gameObject);
                }
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