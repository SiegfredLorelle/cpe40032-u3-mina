using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce = 15.0f;
    public float gravityModifier = 2.0f;
    public bool isOnGround = true;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody of the player
        playerRb = GetComponent<Rigidbody>();

        // Adjust the physics of the game based on gravity modifier
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // On spacebar, player will jump if on the ground
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround) 
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If player collided with the ground, then it is on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        // If player collided with an obstacle, then the game is over
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!!");
            gameOver = true;
        }
    }
}

