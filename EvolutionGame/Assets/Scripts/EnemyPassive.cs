using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPassive : MonoBehaviour
{
    public Player player;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 5;
    public float targetRange = 20f;
    private Transform ts;
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

            Vector2 direction = transform.position - player.transform.position;
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
        if (movement.x == -1 && ts.localScale.x > 0)
        {

            ts.localScale = new Vector3(-1 * ts.localScale.x, ts.localScale.y, 0);
        }
        else if (movement.x == 1 && ts.localScale.x < 0)
        {
            ts.localScale = new Vector3(-1 * ts.localScale.x, ts.localScale.y, 0);
        }
        //rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
