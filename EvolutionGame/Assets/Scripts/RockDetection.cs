using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.GetComponent<Player>().state == PlayerState.Dig)
            {
               
                collision.GetComponent<Animator>().SetBool("isDigging", true);
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            collision.GetComponent<Animator>().SetBool("isDigging", false);
        }
    }
}
