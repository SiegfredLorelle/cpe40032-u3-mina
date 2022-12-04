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
    private AudioSource cameraAudio;

    private float jumpForce = 715.0f;
    private float gravityModifier = 3.0f;
    private float Gamedelay = 1.0f;
    private float minAnimationSpeed = 1.0f;
    private float maxAnimationSpeed = 1.75f;
    private float minParticleSpeed = 0.65f;
    private float maxParticleSpeed = 1.30f;
    private string[] idleAnimations = new string[] { "Idle_WipeMouth", "Salute", "Idle_CheckWatch" };
    private bool isOnGround = true;
    public bool gameOver = false;
    public bool isOnSecondJump = false;
    public bool isOnBoost = false;
    public bool isIntroDone = false;


    // Start is called before the first frame update
    void Start()
    {
        // Get the components from player and other game objects
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        scoreManagerScript = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
        cameraAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        // Show in console the controls of the game
        Debug.Log("[Spacebar] : Jump      [L Shift] : Boost");

        // Adjust the physics of the game based on gravity modifier
        Physics.gravity *= gravityModifier;

        // Play a random idle animation
        int Index = Random.Range(0, idleAnimations.Length);
        playerAnim.Play(idleAnimations[Index]);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if intro is done
        if (!isIntroDone)
        {
            // If the idle animations is finished playing
            if (!playerAnim.GetCurrentAnimatorStateInfo(2).IsTag("Idle") && Time.time > Gamedelay)
            {
                // start running animation
                playerAnim.SetFloat("Speed_f", minAnimationSpeed);

                // if already on running animation and its transitions are finished
                if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Run_Static"))
                {
                    // intro is done, start playing dirt particles 
                    isIntroDone = true;
                    dirtParticle.Play();
                }
            }
        }

        // If intro is done
        else
        {
            // variable needed to access simulation speed of dirt particle
            var main = dirtParticle.main;

            // If Left Shift is pressed
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // Increase the animation speed of run and simulaton speed of dirt particles
                playerAnim.SetFloat("Speed_f", maxAnimationSpeed);
                main.simulationSpeed = maxParticleSpeed;

                // Turn on boost
                isOnBoost = true;
            }

            // Left Shift is not pressed
            else
            {
                // Keep the following on normal speed: animation speed of run and simulaton speed of dirt particles
                playerAnim.SetFloat("Speed_f", minAnimationSpeed);
                main.simulationSpeed = minParticleSpeed;

                // Turn off boost
                isOnBoost = false;
            }


            // On spacebar, player will jump if on ground or on first jump, assuming the game is not yet over
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

                // Set on ground to false since recently jumped (on air)
                isOnGround = false;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        // If player collided with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            // If intro is done and game is not yet over
            if (isIntroDone && !gameOver)
            {
                // Play dirt particle
                dirtParticle.Play();

                // Set variables
                isOnGround = true;
                isOnSecondJump = false;
            }
        }


        // If player collided with an obstacle, then the game is over
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // If game is not yet over (to be set to over within code block to ensure it will only run once)
            if (!gameOver)
            {
                // Play death animation
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);

                // Play explosion and sound effects
                explosionParticle.Play();
                playerAudio.PlayOneShot(crashSound, 0.5f);

                // Turn off dirt particles and background music from camera
                dirtParticle.Stop();
                cameraAudio.Stop();

                // Inform players that the game is over and show their final score
                int finalScore = (int)scoreManagerScript.score;
                Debug.Log($"Game Over!!      Final Score: {finalScore}");

                // Set the game to be over
                gameOver = true;
            }
        }
    }
}

