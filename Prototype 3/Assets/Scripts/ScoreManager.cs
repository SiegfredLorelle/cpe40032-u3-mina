using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float totalTime;
    private int multiplier = 10;
    private float score;

    private PlayerController playerControllerScript;


    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!playerControllerScript.gameOver)
        {
             int boostMultiplier = 1;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                boostMultiplier = 5;
            }


            totalTime += Time.deltaTime * boostMultiplier;
            score = totalTime * multiplier;

            Debug.Log((int)score);
        }

    }
}
