using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExposeAnimTriggers : MonoBehaviour
{
    public Animator animator;

    public void SetTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    public void SetTrigger(int id)
    {
        animator.SetTrigger(id);
    }

    public void SetBool(string id, bool value)
    {
        animator.SetBool(id, value);
    }

    public void SetFloat(string id, float value)
    {
        animator.SetFloat(id, value);
    }
}
