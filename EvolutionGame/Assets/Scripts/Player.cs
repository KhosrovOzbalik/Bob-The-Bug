using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public AudioSource biteEffect;
    // Player attributes
    public Rigidbody2D rb;
    public Transform ts;
    public Animator animator;
    public GameObject wings;


    public int size;
    public float sizeMult = 0.4f;
    public PlayerState state;
    // Try State in inspector
    public PlayerState tempState;


    // Move - Dash
    public float moveSpeed = 5f;
    public float activeMoveSpeed;
    public float dashSpeed;
    public float dashLength = .5f, dashCooldown = 1f;
    private float dashCounter;
    private float dashCoolCounter;
    public bool biting;
    // Move Direction
    Vector2 move;

    // Health
    public int maxHealth = 100;
    public int health;
    public float damageCooldown = 1f;
    public float damageCounter;

    private void Start()
    {
        health = maxHealth;
        activeMoveSpeed = moveSpeed;
        transform.localScale = new Vector3(size * sizeMult, size * sizeMult, 0);

        state = PlayerState.Normal;
        tempState = PlayerState.Normal;

        
    }

    void Update()
    {
        if (tempState != state)
        {
            changeState(tempState);
        }
        
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        rb.velocity = Vector2.zero;

        Bite();
    }

    private void FixedUpdate()
    {
        if (move != Vector2.zero)
        {
            
            Move();
        }
        else
        {
            
            animator.SetBool("isRunning", false);
        }
    }

    private void Bite()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                animator.SetTrigger("Bite");
                print("sa");
                biteEffect.Play();
                biting = true;
               
                

            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                biting = false;
                if (state == PlayerState.Fly)
                    animator.SetTrigger("Fly");
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Eat & Absorb
        if (collision.gameObject.layer == LayerMask.NameToLayer("Creature"))
        {

            Creature script = collision.gameObject.GetComponent<Creature>();
            if (script.size - 1 <= size && biting)
            {
                if (script.state != PlayerState.Normal)
                {
                    changeState(script.state);
                }
                restoreHealth(script.size * 10);
                if (script.state == PlayerState.Normal)
                {
                    size = script.size;
                }
                transform.localScale = new Vector3(size * sizeMult, size * sizeMult, 0);
                Destroy(collision.gameObject);
            }
            else if (script.isHostile && !biting && damageCounter >= damageCooldown)
            {
                damageCounter = 0;
                TakeDamage(15);
                
            }
            damageCounter += Time.deltaTime;
            

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Eat & Absorb
        if (collision.gameObject.layer == LayerMask.NameToLayer("Creature"))
        {
            if (collision.gameObject.tag == "Firefly")
            {
                SceneManager.LoadScene("FinalScene");
            }
            
            Creature script = collision.gameObject.GetComponent<Creature>();
            if ( script.size - 1 <= size && biting)
            {
                
                changeState(script.state);
                restoreHealth(script.size * 10);
                size = script.size;
                transform.localScale = new Vector3(size * sizeMult, size * sizeMult, 0);

                Destroy(collision.gameObject);
            }
            else if ( script.isHostile && !biting)
            {
                damageCounter = damageCooldown;
                TakeDamage(15);
            }
        }
    }

    void Move()
    {
        animator.SetBool("isRunning", true);
        if (move.x == -1 && ts.localScale.x > 0)
        {
            
            ts.localScale = new Vector3(-1 * ts.localScale.x, ts.localScale.y, 0);
        }
        else if (move.x == 1 && ts.localScale.x < 0)
        {
            ts.localScale = new Vector3(-1 * ts.localScale.x, ts.localScale.y, 0);
        }
        move.Normalize();
        
        rb.velocity = move * activeMoveSpeed;
        
        
    }

    void changeState(PlayerState newState)
    {
        state = newState;
        tempState = newState;
        

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("Water"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Cliff"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Rock"), false);
        if (newState != PlayerState.Fly)
        {
            print(newState);
            wings.SetActive(false);
            animator.SetTrigger("notFly");
        }
        if (newState == PlayerState.Fly) {
            wings.SetActive(true);
            animator.SetTrigger("Fly");
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Cliff"));
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Water"));
        }
        else if (newState == PlayerState.Swim) { Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Water")); }
        else if (newState == PlayerState.Dig) { Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Rock")); }
    }

    void TakeDamage(int x)
    {
        health -= x;
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void restoreHealth(int x)
    {
        if (health + x <= 100)
            health += x;
        else
            health = 100;

    }
}
