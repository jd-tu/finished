using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    private float leftRange = 185.0f;
    private float rightRange = 215.0f;

    private float speed = 15;
    public Rigidbody playerRb;
    public float horizontalInput;
    public int pointValue;
    private float horizontalMultiplier = 1.25f;

    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public AudioClip crashSound;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate ()
    {
        if(! gameManager.isGameActive) return;

        if(transform.position.x < leftRange)
        {
            transform.position = new Vector3(leftRange, transform.position.y, transform.position.z);
        } 
        
        if(transform.position.x > rightRange)
        {
            transform.position = new Vector3(rightRange, transform.position.y, transform.position.z);
        }

        Vector3 moveForward = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        playerRb.MovePosition(playerRb.position + moveForward + horizontalMove);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(gameManager.isGameActive)
        {
            if(collision.gameObject.CompareTag("Animal")){
                gameManager.GameOver();
                // dirtParticle.Play();
            } else if(collision.gameObject.CompareTag("Diamond")){
                // explosionParticle.transform.position = this.transform.position;
                gameManager.UpdateScore(pointValue);
                explosionParticle.Play();
                playerAudio.PlayOneShot(crashSound, 0.5f);
                Destroy(collision.gameObject);
                // Debug.Log("bum bum");

                // gameOver = true;
                // playerAnim.SetBool("Death_b", true);
                // playerAnim.SetInteger("DeathType_int", 1);
                // dirtParticle.Stop();
            }
        }
    }
}
