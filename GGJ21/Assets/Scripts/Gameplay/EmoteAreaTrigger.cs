using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteAreaTrigger : MonoBehaviour
{
    public string emote = "test";
    public bool triggerOnlyOnce = false;
    public bool triggered = false;


    private void Start()
    {
        if (!GetComponentInChildren<Collider2D>())
        {
            Debug.LogError("EmoteAreaTrigger " + gameObject.name + " is missing a Collider2D.");
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
                Debug.LogError("EmoteAreaTrigger " + gameObject.name + " has a Collider2D, but not marked as \"Is Trigger\".");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerOnlyOnce && triggered)
            return;

        if (collision.GetComponent<PlayerEmote>())
        {
            collision.GetComponent<PlayerEmote>().Play(emote);
        }
        else if (collision.GetComponentInChildren<PlayerEmote>())
        {
            collision.GetComponentInChildren<PlayerEmote>().Play(emote);
        }
        else if (collision.GetComponentInParent<PlayerEmote>())
        {
            collision.GetComponentInParent<PlayerEmote>().Play(emote);
        }

        triggered = true;
    }

}
