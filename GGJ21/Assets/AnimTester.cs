using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTester : MonoBehaviour
{
    public Animator animator;
    public KeyCode keyCode;
    public string trigger;

    void Update()
    {
        if(Input.GetKeyDown(keyCode))
        {
            animator.SetTrigger(trigger);
        }
    }
}
