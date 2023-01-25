using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player;
    private Animator anim;
    private enum State { idle, running, jumping, falling, hurt }
    private State state = State.idle;
    private Collider2D coll;
    public GameObject GameOverUI;


    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask obstacle;
    [SerializeField] private float speed = 15f;
    [SerializeField] private float jumpForce = 40f;
    [SerializeField] public static int balloons = 0;
    [SerializeField] private Text balloonText;
    [SerializeField] private float hurtForce = 20f;
    [SerializeField] private AudioSource footsteps;
    [SerializeField] private AudioSource balloonSound;
    [SerializeField] private AudioSource ouch;
    [SerializeField] private AudioSource powerup;
    [SerializeField] private int health;
    [SerializeField] private Text healthAmount;
    [SerializeField] private GameObject ParticlePrefab;
   



    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        healthAmount.text = health.ToString();
     

    }

    // Update is called once per frame
    private void Update()
    {
        if(state != State.hurt)
        {
            Movement();
         
        }
       
          AnimationState();
        anim.SetInteger("state", (int)state);
      

    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Collectible")
        {
            balloonSound.Play();

            ScoreScript.scoreValue += 10;
            Destroy(collision.gameObject);
            Instantiate(ParticlePrefab, gameObject.transform.position, Quaternion.identity);
            balloons += 1;
            Debug.Log("Player touched me!");
            balloonText.text = balloons.ToString();
            
        }

        if(collision.tag == "Powerup")
        {
            Destroy(collision.gameObject);
            speed = 30f;
            jumpForce = 60f;
            powerup.Play();
            GetComponent<SpriteRenderer>().color = Color.blue;
            StartCoroutine(ResetPower());
        }
        

    }

    private void OnCollisionEnter2D(Collision2D other) { 

        if(other.gameObject.tag == "Enemy" )
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if(state == State.falling)
            {
                enemy.JumpedOn();
                Jump();
            }

            else
            {
                state = State.hurt;
                ouch.Play();

                HandleHealth();

                if (other.gameObject.transform.position.x > transform.position.x)
                {

                    player.velocity = new Vector2(-hurtForce, player.velocity.y);
                    Debug.Log("Auch!");
                }
                else
                {

                    player.velocity = new Vector2(hurtForce, player.velocity.y);
                    Debug.Log("Auch!");
                }
            }

        }
}

    private void HandleHealth()
    {
        health -= 1;
        healthAmount.text = health.ToString();
        if (health <= 0)
        {

            EndGame();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection < 0)
        {
            player.velocity = new Vector2(-speed, player.velocity.y);
            transform.localScale = new Vector2(-1, 1);

        }

        else if (hDirection > 0)
        {

            player.velocity = new Vector2(speed, player.velocity.y); //player velocity kad veiktu gravity
            transform.localScale = new Vector2(1, 1);

        }
        
        if (Input.GetButtonDown("Jump") && ((coll.IsTouchingLayers(ground)) || (coll.IsTouchingLayers(obstacle)))) // keyDown send the message just once
        {
            Jump();
        }
    }

    private void Jump()
    {
        player.velocity = new Vector2(player.velocity.x, jumpForce);
        state = State.jumping;
    }

    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (player.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }

        else if (state == State.hurt)
        {
            if (Mathf.Abs(player.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }



        else if (Mathf.Abs(player.velocity.x) > 2f)
        {
            //moving
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void Footstep()
    {
        if (coll.IsTouchingLayers(ground))
        {
            footsteps.Play();
        }
    }

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(3);
        speed = 15f;
        jumpForce = 40f;
        powerup.Stop();
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void EndGame()
    {

        Debug.Log("Game Over");
        GameOverUI.SetActive(true);
    }
    
}

