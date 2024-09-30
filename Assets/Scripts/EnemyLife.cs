using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public int healthPoints = 1;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            healthPoints--;

            if (healthPoints <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}