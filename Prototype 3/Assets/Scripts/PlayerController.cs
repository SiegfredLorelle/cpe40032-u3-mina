using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    private ScoreManager scoreManagerScript;

    public float jumpForce = 600f;
    public float gravityModifier = 2.0f;
    public bool isOnGround = true;
    public bool gameOver = false;
    public bool isOnSecondJump = false;
    public bool isOnBoost = false;
    public bool isIntroDone = false;
    private string[] idleAnimations = new string[] { "Idle_WipeMouth", "Salute", "Idle_CheckWatch" };



    // Start is called before the first frame update
    void Start()
    {
        // Get the components from player or other game objects
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        scoreManagerScript = GameObject.Find("Score Manager").GetComponent<ScoreManager>();

        // Adjust the physics of the game based on gravity modifier
        Physics.gravity *= gravityModifier;

        int Index = Random.Range(0, idleAnimations.Length);
        playerAnim.Play(idleAnimations[Index]);


    }










    // Update is called once per frame
    void Update()
    {

        if (!isIntroDone)

        {

            if (!playerAnim.GetCurrentAnimatorStateInfo(2).IsTag("Idle") && Time.time > 1)
            {
                playerAnim.SetFloat("Speed_f", 1.0f);

                if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Run_Static"))
                {
                    isIntroDone = true;
                    dirtParticle.Play();

                }
            }
        }

        else
        {
            var main = dirtParticle.main;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnim.SetFloat("Speed_f", 1.75f);
                main.simulationSpeed = 1.30f;

                isOnBoost = true;
            }
            else
            {
                playerAnim.SetFloat("Speed_f", 1.0f);
                main.simulationSpeed = 0.65f;

                isOnBoost = false;
            }



            // On spacebar, player will jump if on ground or on first jump assuming the game is not yet over
            if (Input.GetKeyDown(KeyCode.Space) && (isOnGround || !isOnSecondJump) && !gameOver)
            {

                // If not on ground then it on second jump
                if (!isOnGround)
                {
                    isOnSecondJump = true;
                }

                // Player jumps using force
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                // Play jump sound
                playerAudio.PlayOneShot(jumpSound, 1.0f);


                // Play animation and stop dirt particle if on first jump
                if (!isOnSecondJump)
                {
                    playerAnim.SetTrigger("Jump_trig");
                    dirtParticle.Stop();
                }

                isOnGround = false;
            }
        }
         
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If player collided with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isIntroDone)
            {
                // Play dirt particle as long as the game is not yet over
                if (!gameOver)
                {
                    isOnGround = true;
                    isOnSecondJump = false;

                    if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Run_Static"))
                    {
                        dirtParticle.Play();
                    }
                }
            }
        }

        // If player collided with an obstacle, then the game is over
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Ensures it only occurs once by setting game over to true inside the code block
            if (!gameOver)
            {
                // Play death animation
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);

                // Play explosion and sound effects, turn off dirt particles
                explosionParticle.Play();
                dirtParticle.Stop();
                playerAudio.PlayOneShot(crashSound, 0.5f);

                int finalScore = (int)scoreManagerScript.score;
                Debug.Log($"Game Over!!  Final Score: {finalScore}");

                gameOver = true;

            }


        }
    }
}

