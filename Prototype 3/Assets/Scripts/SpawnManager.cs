using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obsataclePrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2.0f;
    private float repeatRate = 2.0f;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Get variables from player controller scripts
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        // Spawn obstacles on a delay and a rate
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
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
            Instantiate(obsataclePrefab, spawnPos, obsataclePrefab.transform.rotation);
        }
    }
}
