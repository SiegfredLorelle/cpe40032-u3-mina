using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerController playerControllerScript;

    public GameObject[] obstaclePrefabs;
    private Vector3 spawnPos = new Vector3(30, 0, 0);
    private float startDelay = 4.0f;
    private float shortestDelayRange = 0.5f;
    private float normalDelayRange = 1.0f;
    private float longestDelayRange = 1.75f;

    // Start is called before the first frame update
    void Start()
    {
        // Get player controller script from player to access its variables
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        // Spawn the first obstacle on a delay
        Invoke("SpawnObstacle", startDelay);
    }

    // Spawn obstacles via recusrive loop
    void SpawnObstacle()
    {
        // Spawn obstacles as long as the game is not yet over
        if (!playerControllerScript.gameOver)
        {
            // Spawn a random obstacle
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, obstaclePrefabs[obstacleIndex].transform.rotation);

            // Randomize a delay for the next spawn
            float randomDelay = Random.Range(normalDelayRange, longestDelayRange);

            // Increase the chance of getting a short delay if on boost
            if (playerControllerScript.isOnBoost)
            {
                randomDelay = Random.Range(shortestDelayRange, normalDelayRange);
            }

            // Spawn the obstacle at a random delay
            Invoke("SpawnObstacle", randomDelay);
        }

    }
}
