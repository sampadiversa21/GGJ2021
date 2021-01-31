using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerController>().isPecking)
            {
                GetComponent<Animator>().SetBool("break", true);
            }
        }
    }

    public void FinishAnimation()
    {
        Destroy(gameObject);
    }
}
