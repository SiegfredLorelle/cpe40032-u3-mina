using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    private Vector3 spawnPos = new Vector3(30, 0, 0);
    private float startDelay = 2.0f;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Get variables from player controller scripts
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        // Spawn the first obstacle on a delay
        Invoke("SpawnObstacle", startDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        // Spawn obstacles as long as the game is not yet over
        if (!playerControllerScript.gameOver)
        {
            // Spawn a random obstacle
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, obstaclePrefabs[obstacleIndex].transform.rotation);

            // Spawn the next obstacle at a random delay
            float randomDelay = Random.Range(1.25f, 3.0f);
            Invoke("SpawnObstacle", randomDelay);
        }

    }
}
