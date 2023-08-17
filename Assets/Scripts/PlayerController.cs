using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 6f;
    private Animator animator;
    private Rigidbody playerRb;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip pickSound;
    public AudioClip killSound;
    public GameObject goal;
    

    private AudioSource playerAudio;
    private GameManager gameManager;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool OnGround;
    public bool powerUp;
    public float jumpRate = 1f;
    private float canJump = -1f;
    //private int lives = 3;
    //public bool gameOver;
    private Vector3 initialPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Physics.gravity *= gravityModifier;
        powerUp = false;
        float horizontalInput = Input.GetAxis("Horizontal");
        
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput > 0)
        {
            MoveRight();
            
        }
        else if (horizontalInput < 0)
        {
            MoveLeft();
            
        }
        if (horizontalInput != 0)
        {
            animator.SetFloat("Speed_f" ,1.5f);
        }
        else
        {
            animator.SetFloat("Speed_f", 0f);
        }
        if(transform.position.z > 0.5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.5f);
        }
        else if (transform.position.z < -0.5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
        }

        if(transform.position.x < -5)
        {
            transform.position = new Vector3(-5, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Space) && gameManager.gameOver == false && Time.time> canJump )
        {
            canJump = Time.time + jumpRate;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            OnGround = false;
            animator.SetTrigger("Jump_trig");
            //dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);

        }
        if(transform.position.x > goal.transform.position.x)
        {
            gameManager.gameOver = true;
            gameManager.levelComplete();
        }
    }
 
    private void MoveRight()
    {
        if(gameManager.gameOver == false)
        {
            Vector3 rightDirection = transform.forward * moveSpeed * Time.deltaTime;
            transform.position += rightDirection;
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        
    }

    private void MoveLeft()
    {
        if (gameManager.gameOver == false)
        {
            Vector3 leftDirection = transform.forward * moveSpeed * Time.deltaTime;
            transform.position += leftDirection;
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
            
    }
    private void OnCollisionEnter(Collision collision)
    {
        // if (collision.gameObject.CompareTag("Ground"))
        // {
        //OnGround = true;
        //dirtParticle.Play();
        // }
        //  else if (collision.gameObject.CompareTag("Obstacle"))
        // {
        //  gameOver = true;
        //  playerAnim.SetBool("Death_b", true);
        //  playerAnim.SetInteger("DeathType_int", 1);
        // explosionParticle.Play();
        // dirtParticle.Stop();
        // playerAudio.PlayOneShot(crashSound, 1.0f);
        //}
        if (collision.gameObject.CompareTag("Goomba"))
        {
            if(powerUp == true)
            {
                Destroy(collision.gameObject);
            }
            Vector3 normal = collision.contacts[0].normal;
            if (normal.y > 0.7f) // Check if collision is from above
            {
                Destroy(collision.gameObject);
                playerAudio.PlayOneShot(killSound, 1.0f);
                gameManager.UpdateScore(10);// Remove the Goomba when jumped on
                // You might also want to play a sound or add score here
            }
            if(normal.x < -0.7f || normal.x > 0.7f)
            {
                if(powerUp == false)
                {
                    gameManager.Damage();
                    playerAudio.PlayOneShot(crashSound, 1.0f);
                    transform.position = initialPosition;
                }
                
            }
        }
        

    }
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("trigger"))
        {
            gameManager.Damage();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            transform.position = initialPosition;

        }
        if(other.gameObject.CompareTag("coin"))
        {
            gameManager.UpdateScore(5);
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(pickSound, 1.0f);
        }
        if (other.gameObject.CompareTag("star"))
        {
            Destroy(other.gameObject);
            powerUp = true;
            StartCoroutine(PowerUpCountDown());
        }
        if(other.gameObject.CompareTag("Bullet"))
        {
            gameManager.Damage();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            transform.position = initialPosition;
        }
        if (other.gameObject.CompareTag("Spike"))
        {
            gameManager.Damage();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            transform.position = initialPosition;
        }


    }
    IEnumerator PowerUpCountDown()
    {
        yield return new WaitForSeconds(7);
        powerUp = false;

    }





}
