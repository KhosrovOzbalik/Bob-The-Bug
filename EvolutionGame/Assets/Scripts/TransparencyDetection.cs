using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyDetection : MonoBehaviour
{
    public bool playerDetected;
    GameObject playerGO;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        playerDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true;
            playerGO = collision.gameObject;

            spriteRenderer.sortingLayerName = "FrontBuilding";

            var tempColor = spriteRenderer.color;
            tempColor.a = 0.5f;
            spriteRenderer.color = tempColor;


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
            spriteRenderer.sortingLayerName = "BG";
            var tempColor = spriteRenderer.color;
            tempColor.a = 1;
            spriteRenderer.color = tempColor;

        }
    }
}
