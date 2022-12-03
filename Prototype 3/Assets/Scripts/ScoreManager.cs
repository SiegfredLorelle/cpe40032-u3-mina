using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private PlayerController playerControllerScript;

    private float totalTime;
    private int multiplier = 10;
    public float score;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.isIntroDone)
        {
            if (!playerControllerScript.gameOver)
            {
                int boostMultiplier = 1;

                if (playerControllerScript.isOnBoost)
                {
                    boostMultiplier = 5;
                }


                totalTime += Time.deltaTime * boostMultiplier;
                score = totalTime * multiplier;

                Debug.Log($"Score: {(int)score}");
            }
        }


    }
}
