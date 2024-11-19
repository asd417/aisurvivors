using System.Collections;
using System.Collections.Generic;
using System.Linq; // for concat
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public float pickupRadius = 0.25f;  // Distance within which the player can pick up the item
    public float pickupAnimationSpeed = 2f;  // Speed of the pickup animation

    public bool chip = false;
    public bool health = false;
    public bool weapon = false;

    public V2SpriteManager cCount;

    private void Start()
    {
        cCount = GameObject.Find("SpriteManager").GetComponent<V2SpriteManager>();
    }

    void OnTriggerEnter2D(Collider2D collider){
        
        if (IsPlayer(collider)){
            if (chip){
                cCount.chipCount++;
                Destroy(gameObject);
            }
            if (health){
                HealthManager hm = collider.GetComponent<HealthManager>();
                hm.currentHealth += (int)(hm.healthPoints*.15);
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

























    // Function to handle picking up the item
    void PickUpItem(Transform player)
    {
        // Basic animation: the item moves towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, pickupAnimationSpeed * Time.deltaTime);

        // Once the item reaches the player, pick it up
        if (Vector2.Distance(transform.position, player.position) < 10f)
        {
            // Add item to the player's inventory (if you have an inventory system) or trigger any other logic here
            Debug.Log("Item picked up by: " + player.name);
            Destroy(gameObject);
        }
    }
}
