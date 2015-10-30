using UnityEngine;
using System.Collections;

public class EnemyScanned : MonoBehaviour
{
    public int attackDamage = 10;               // The amount of health taken away per attack.
    GameObject player;                          // Reference to the player GameObject.
    PrototypeHealth playerHealth;                  // Reference to the player's health.
    void Awake()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PrototypeHealth>();

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            print("EnemyScanned");
        }
        if (other.gameObject == player)
        {

            // ... damage the player.
            playerHealth.TakeDamage(attackDamage);
        }


    }

}