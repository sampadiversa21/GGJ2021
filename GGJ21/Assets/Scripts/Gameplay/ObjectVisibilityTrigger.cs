using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVisibilityTrigger : MonoBehaviour
{
    public GameObject targetObject;
    public TriggerAction triggerAction = TriggerAction.EnableObject;

    public string[] objectTagFilter = new string[] { "Player" };

    private bool targetState
    {
        get { return triggerAction == TriggerAction.EnableObject ? true : false; }
    }

    public enum TriggerAction
    {
        EnableObject,
        DisableObject
    }

    private void Start()
    {
        if (!targetObject)
        {
            Debug.LogError("WorldLayerTrigger " + gameObject.name + " is missing a Target Object reference.");
        }

        if (!GetComponentInChildren<Collider2D>())
        {
            Debug.LogError("WorldLayerTrigger " + gameObject.name + " is missing a Collider2D.");
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
                Debug.LogError("WorldLayerTrigger " + gameObject.name + " has a Collider2D, but not marked as \"Is Trigger\".");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetObject.activeSelf == targetState)
            return;

        for (int x = 0; x < objectTagFilter.Length; x++)
        {
            if (objectTagFilter[x] == collision.tag)
            {
                targetObject.SetActive(targetState);
                break;
            }
        }
    }
}
