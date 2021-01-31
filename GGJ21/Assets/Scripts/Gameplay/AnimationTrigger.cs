using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public Animator targetAnimator;
    public string triggerId;
    public bool triggerOnce = true;

    private bool triggered = false;

    public string[] objectTagFilter = new string[] { "Player" };


    private void Start()
    {
        if (!targetAnimator)
        {
            Debug.LogError("AnimationTrigger " + gameObject.name + " is missing a Target Animator reference.");
        }

        if (!GetComponentInChildren<Collider2D>())
        {
            Debug.LogError("AnimationTrigger " + gameObject.name + " is missing a Collider2D.");
        }
        else
        {

            Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
            bool foundTrigger = false;
            for (int x = 0; x < colliders.Length; x++)
            {
                if (colliders[x].isTrigger)
                    foundTrigger = true;
            }
            if (!foundTrigger)
                Debug.LogError("AnimationTrigger " + gameObject.name + " has a Collider2D, but not marked as \"Is Trigger\".");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered && triggerOnce)
            return;

        for (int x = 0; x < objectTagFilter.Length; x++)
        {
            if (objectTagFilter[x] == collision.tag)
            {
                targetAnimator.SetTrigger(triggerId);
                break;
            }
        }
    }
}
