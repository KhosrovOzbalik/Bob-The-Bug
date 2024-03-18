using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public Animator transition;
    void Start()
    {
        transition.SetTrigger("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
