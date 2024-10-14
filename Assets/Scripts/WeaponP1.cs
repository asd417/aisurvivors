using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using Unity.VisualScripting;


public class WeaponP1 : MonoBehaviour
{
    [Tooltip("Speed at which the sprite rotates around the player.")]
    private float rotationSpeed = .6f;

    public GameObject DamageTextCanvas;
    private Transform targetPlayer;
    private bool isAttached = false;
    private float angle_euler = 0;
    private int dist = 1;

    [SerializeField, Tooltip("Damage dealt to the player")]
    int damage = 1;

    [SerializeField, Tooltip("Invincibility buffer in seconds between damage intervals")]
    float invincibilityBuffer = 1.0f;
    private bool canDealDamage = true;
    public GameObject dmgTextPrefab;

    private void Start()
    {
        DamageTextCanvas = GameObject.Find("DmgTextDrawer");
    }

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
        if ((!isAttached && collision.CompareTag("Player1")))
        {
            targetPlayer = collision.transform;
            isAttached = true;
        }

        if (isAttached && collision.CompareTag("Enemy"))
        {
            //Need to check if the gameobject has a enemy component
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                GameObject text = Instantiate(dmgTextPrefab, DamageTextCanvas.transform);
                text.GetComponent<DmgText>().SetDmgPos(damage, collision.transform.position);
            }
        }
    }
}