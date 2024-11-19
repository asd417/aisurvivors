using System.Collections;
using System.Collections.Generic;
using System.Linq; // for concat
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private float pickupRadius = 5.0f;  // Distance within which the player can pick up the item
    public float pickupAnimationSpeed = 2.0f;  // Speed of the pickup animation

    public bool chip = false;
    public bool health = false;
    public bool weapon = false;

    public V2SpriteManager spriteManager;

    private void Start()
    {
        //Debug.Log("Start ItemPickup");
        spriteManager = GameObject.Find("SpriteManager").GetComponent<V2SpriteManager>();
        PickUpItem();
    }

    void OnTriggerEnter2D(Collider2D collider){
        
        if (IsPlayer(collider)){
            if (chip){
                spriteManager.chipCount++;
                Destroy(gameObject);
            }
            if (health){
                HealthManager hm = collider.GetComponent<HealthManager>();
                hm.Heal(0.15f);
                Destroy(gameObject);
            }
            if (weapon){
                //chipCount++;
                Destroy(gameObject);
            }
        }
    }

    private bool IsPlayer(Collider2D collider)
    {
        return collider.CompareTag("Player1") || collider.CompareTag("Player2") || collider.CompareTag("Player3");
    }























    IEnumerator moveTowardPlayerCoroutine()
    {
        while (true)
        {
            //Debug.Log("MovingTowards " + Player.name);
            Transform Player = spriteManager.GetClosestPlayer(transform.position);
            Debug.Log($"{Player} {transform.position} {Player.position} {(transform.position - Player.position).magnitude} {(transform.position - Player.position).magnitude < pickupRadius}");
            if (Player != null && (transform.position-Player.position).magnitude < pickupRadius)
            {
                Debug.Log("Moving pickup");
                transform.position = Vector3.Lerp(transform.position, Player.position, pickupAnimationSpeed * Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
            // Once the item reaches the player, pick it up
            if ((transform.position - Player.position).magnitude < 0.1f)
            {
                // Add item to the player's inventory (if you have an inventory system) or trigger any other logic here
                Debug.Log("Item picked up by: " + Player.name);
                Destroy(gameObject);
                break;
            }
        }   
    }

    // Function to handle picking up the item
    void PickUpItem()
    {
        // Basic animation: the item moves towards the player
        StartCoroutine(moveTowardPlayerCoroutine());
    }
}
