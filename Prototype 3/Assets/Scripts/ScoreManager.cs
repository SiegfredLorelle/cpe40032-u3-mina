using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private PlayerController playerControllerScript;

    private int noMultiplier = 1;
    private int boostMultiplerRate = 5;
    private int generalMultiplier = 10;
    private float totalTime;
    public float score;

    // Start is called before the first frame update
    void Start()
    {
        // Get player controller script from player to access its variables
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // If intro is done, and game is not yet over
        if (playerControllerScript.isIntroDone && !playerControllerScript.gameOver)
        {
            // If not on boost, then no multiplier
            int boostMultiplier = noMultiplier;

            // If on boost, there is a boost multiplier
            if (playerControllerScript.isOnBoost)
            {
                boostMultiplier = boostMultiplerRate;
            }

            // Scoring system where total time is the score
            totalTime += Time.deltaTime * boostMultiplier;

            // multiplier here just increases the scores (because low score does't seem exciting)
            score = totalTime * generalMultiplier;

            // Constantly show the score in console
            Debug.Log($"Score: {(int)score}");
        }
    }
}
