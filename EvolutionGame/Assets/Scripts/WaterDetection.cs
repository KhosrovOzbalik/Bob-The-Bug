using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDetection : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            if (collision.GetComponent<Player>().state == PlayerState.Swim)
            {
                
                collision.GetComponent<Animator>().SetTrigger("Swim");
            }
        }
        
    }

    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            collision.GetComponent<Animator>().SetTrigger("notSwim");
        }
    }
}
