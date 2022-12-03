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
    public float jumpForce = 15.0f;
    public float gravityModifier = 2.0f;
    public bool isOnGround = true;
    public bool gameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        // Get the components from the player
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        // Adjust the physics of the game based on gravity modifier
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // On spacebar, player will jump if on the ground and the game is not yet over
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver) 
        {
            // Player jumps using force
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // Play animation and sound effects, turn off dirt particle
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            dirtParticle.Stop();

            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If player collided with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {

            // Play dirt particle as long as the game is not yet over
            if (!gameOver)
            {
                dirtParticle.Play();
                isOnGround = true;
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
                
                Debug.Log("Game Over!!");
                gameOver = true;
            }


        }
    }
}

