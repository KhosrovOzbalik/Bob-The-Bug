using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    // 1 - 2 - 3 
    public int tempSize = 1;
    public float sizeMult = 0.7f;
    public PlayerState state;
    public bool isHostile;
    public int size = 1;
    private void Start()
    {
        transform.localScale = new Vector3(size * sizeMult, size * sizeMult, 0);
    }
    private void Update()
    {
        sizeCheck();    
    }

    private void sizeCheck()
    {
        if (tempSize != size)
        {
            size = tempSize;
            transform.localScale = new Vector3(size * sizeMult, size * sizeMult, 0);
        }
    }

}
