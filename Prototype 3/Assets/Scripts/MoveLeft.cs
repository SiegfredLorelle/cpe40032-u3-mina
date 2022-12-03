using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private float leftBound = -15.0f;
    private float normalSpeed = 30.0f;
    private int boostMultiplier = 2;

    // Start is called before the first frame update
    void Start()
    {
        // Get player controller script from player to access its variables
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the intro is done, and the game is not yet over
        if (playerControllerScript.isIntroDone && !playerControllerScript.gameOver)
        {
            // Set normal speed for prefabs and background
            float speed = normalSpeed;

            // If on boost, increase the speed
            if (playerControllerScript.isOnBoost)
            {
                speed *= boostMultiplier;
            }

            // Move the prefabs and background to the left (creates an illusion that the player is running to the right)
            transform.Translate(Vector3.left * speed * Time.deltaTime);


            // Destroy obstacle upon reaching the left bound
            if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
        }
    }
}
