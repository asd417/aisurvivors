using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;


public class Weapon : MonoBehaviour
{
    [Tooltip("Speed at which the sprite rotates around the player.")]
    public float rotationSpeed = 3f;

    public GameObject DamageTextCanvas;
    private Transform targetPlayer;
    private bool isAttached = false;
    private float angle_euler = 0;
    private int dist = 1;
    public bool player1CanPickUp = true;
    public bool player2CanPickUp = true;
    public bool player3CanPickUp = true;
    public bool weaponPersistsThroughScene = true;

    [SerializeField, Tooltip("Weapon - Damage dealt to enemy")]
    int damage = 1;

    // [SerializeField, Tooltip("Invincibility buffer in seconds between damage intervals")]
    // float invincibilityBuffer = 1.0f;
    // private bool canDealDamage = true;
    public GameObject dmgTextPrefab;

    private void Start()
    {
        DamageTextCanvas = GameObject.Find("DmgTextDrawer");
    }

    void Update()
    {
        if (isAttached && targetPlayer != null)
        {
            angle_euler = (angle_euler + rotationSpeed * Time.deltaTime * 60f) % 360;
            transform.eulerAngles = new Vector3 (0,0, angle_euler);
            transform.position = targetPlayer.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle_euler) * dist, Mathf.Sin(Mathf.Deg2Rad * angle_euler) * dist, 0);
        }
    }

    /// <summary>
    /// Function used to detach this weapon from player
    /// </summary>
    public void Detach()
    {
        targetPlayer = null;
        isAttached = false;
        transform.SetParent(null);
        SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((!isAttached && collision.CompareTag("Player1") && player1CanPickUp)|| 
        (!isAttached && collision.CompareTag("Player2") && player2CanPickUp) || 
        (!isAttached && collision.CompareTag("Player3") && player3CanPickUp))
        {
            targetPlayer = collision.transform;
            isAttached = true;
            //Weapon needs to be a child of player so that it can follow the player into the next scene
            if(weaponPersistsThroughScene) transform.SetParent(targetPlayer);
        }

        if (isAttached && collision.CompareTag("Enemy"))
        {
            //Need to check if the gameobject has a enemy component
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.AddEnemyTakingDmg(damage);
                enemy.SetDmgTextInfo(dmgTextPrefab, DamageTextCanvas.transform);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isAttached && collision.CompareTag("Enemy"))
        {
            //Need to check if the gameobject has a enemy component
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.RemoveEnemyTakingDmg(damage);
            }
        }
    }
}