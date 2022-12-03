using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private float leftBound = -15.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Get variables from player controller scripts
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the game is not yet over, move the object to the left
        if (!playerControllerScript.gameOver)
        {
            float speed = 30.0f;

            if (playerControllerScript.isOnBoost)
            {
                speed = 60.0f;
            }

            transform.Translate(Vector3.left * speed * Time.deltaTime);


        }

        // Destroy obstacle upon reaching the left bound
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
