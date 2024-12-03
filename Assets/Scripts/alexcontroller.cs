using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class alexcontroller : MonoBehaviour
{

    private Rigidbody rb;

    private int count;
    // Movement along X and Y axes.
    private float movementX;
    private float movementY;
    // Speed at which the player moves.
    public float speed = 5;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    // Initial speed
    public float movementSpeed = 5f;
    // Amount to increase speed on collision
    //public float speedIncreaseAmount = 2f;

    public float jumpAmount = 2;
    int jumpCounter;
    //public bool isActive = false;  // Is the shield currently active?
    //public int shieldHealth = 100; // Shield health (if you want it to take damage)
    //public GameObject shieldVisual; // The visual effect for the shield (like a model, or particle effect)

    // Start is called before the first frame update
    void Start()
    {
        jumpCounter = 0;
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.J)) && (jumpCounter < 2))
        {
            rb.AddForce(Vector2.up * jumpAmount, ForceMode.Impulse);
            jumpCounter++;
            Debug.Log("succesful jump!");
        }
        if(rb.transform.position.y < 30 && jumpCounter > 0)
        {
            jumpCounter = 0;
            Debug.Log("reset jump");
        }
    }
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    void SetCountText()
    {
        if (count >= 4) ;
        {
            winTextObject.SetActive(true);
        }

        countText.text = "Count: " + count.ToString();
        Destroy(GameObject.FindGameObjectWithTag("Enemy2"));
    }


    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);

        // Apply movement using transform (no physics)
        transform.position += movement * speed * Time.deltaTime;
        //Destroy(GameObject.FindGameObjectWithTag("Enemy"));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            count = count + 1;

            SetCountText();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collide with player");

        if (collision.gameObject.CompareTag("Enemy2"))
        {
            // Destroy the current object
            Destroy(gameObject);
            // Update the winText to display "You Lose!"
            Debug.Log("hit player");
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
        //if (collision.gameObject.CompareTag("DynamicObject"))
        //{
            //movementSpeed += speedIncreaseAmount; // Increase speed
            //Debug.Log("Speed increased! New speed: " + movementSpeed);
        //}
    }


}
