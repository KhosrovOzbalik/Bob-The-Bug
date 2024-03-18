using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHostile : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;
    private Transform ts;
    private Vector2 movement;
    public float moveSpeed = 5;
    public float targetRange = 20f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        ts = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < targetRange) 
        {
            print("in range");
            
            Vector2 direction = player.position - transform.position;
            direction.Normalize();
            movement = direction;
        }
        else
        {
            print("Our of range");
           
        }

    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < targetRange)
            Move(movement);
        else
            rb.velocity = Vector2.zero;
    }

    void Move(Vector2 direction)
    {

        rb.velocity = movement * moveSpeed;
        if (movement.x == 1 && ts.localScale.x > 0)
        {

            ts.localScale = new Vector3(-1 * ts.localScale.x, ts.localScale.y, 0);
        }
        else if (movement.x == -1 && ts.localScale.x < 0)
        {
            ts.localScale = new Vector3(-1 * ts.localScale.x, ts.localScale.y, 0);
        }
        //rb.position = Vector2.MoveTowards(rb.position, player.position, moveSpeed * Time.fixedDeltaTime);
        //rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

}
