using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    
    public GameObject shieldVisual;  // Reference to the shield's visual object (could be a 3D model, etc.)
    private bool isShieldActive = false;  // Flag to track if the shield is active
    public float repelForce = 10f;  // Force applied to enemies that hit the shield
    
    // Reference to the player's collider (for ignoring collision with enemies while shield is active)
    private Collider playerCollider;


    // Start is called before the first frame update
    void Start()
    {
        // Make sure the shield is initially inactive
        if (shieldVisual != null)
            shieldVisual.SetActive(false);

        // Get the player's collider (assuming the player has a collider component)
        playerCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player presses the space key to activate the shield
        if (Input.GetKeyDown(KeyCode.Space) && !isShieldActive)
        {
            ActivateShield();
        }
    }

    // Method to activate the shield
    void ActivateShield()
    {
        isShieldActive = true;  // Set the shield as active
        if (shieldVisual != null)
        {
            shieldVisual.SetActive(true);  // Enable the shield visual
        }

        // Disable collisions between the player and enemies while the shield is active
        Collider[] enemyColliders = FindObjectsOfType<Collider>();
        foreach (var enemy in enemyColliders)
        {
            if (enemy.CompareTag("Enemy2"))
            {
                Physics.IgnoreCollision(playerCollider, enemy);
            }
        }

        // Start the shield deactivation timer (5 seconds)
        Invoke("DeactivateShield", 5f);  // Deactivate after 5 seconds
    }

    // Method to deactivate the shield
    void DeactivateShield()
    {
        isShieldActive = false;  // Set the shield as inactive
        if (shieldVisual != null)
        {
            shieldVisual.SetActive(false);  // Disable the shield visual
        }
        // Re-enable collisions between the player and enemies after the shield is deactivated
        Collider[] enemyColliders = FindObjectsOfType<Collider>();
        foreach (var enemy in enemyColliders)
        {
            if (enemy.CompareTag("Enemy2"))
            {
                Physics.IgnoreCollision(playerCollider, enemy, false);  // Re-enable collision
            }
        }
    }

    // Detect when an enemy enters the shield's trigger zone
    void OnTriggerEnter(Collider other)
    {
        if (isShieldActive && other.CompareTag("Enemy2"))  // Check if the colliding object is an enemy
        {
            // Repel the enemy by applying a force
            Rigidbody enemyRigidbody = other.GetComponent<Rigidbody>();
            if (enemyRigidbody != null)
            {
                Vector3 repelDirection = other.transform.position - transform.position;  // Get direction from shield to enemy
                enemyRigidbody.AddForce(repelDirection.normalized * repelForce, ForceMode.Impulse);
            }

            // You can also add more logic here, like damaging the enemy or applying an effect
            Debug.Log("Enemy repelled!");
        }
    }
}
