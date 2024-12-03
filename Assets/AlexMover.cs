using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlexMover : MonoBehaviour
{
    private Rigidbody rb;
    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    public float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Check if the Rigidbody component is attached to the GameObject
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on " + gameObject.name);
        }
    }

    // This method will be triggered by the input system
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        // Check if the Rigidbody is valid
        if (rb != null)
        {
            // Create a 3D movement vector using the X and Y inputs.
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);

            // Apply force to the Rigidbody to move the player.
            rb.AddForce(movement * speed);
        }
    }
}
