using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomControl : MonoBehaviour
{
    /// <summary>
    /// Idle bouncing, without the jump
    /// </summary>
    public float normalBounce = 2f;

    /// <summary>
    /// Higher bounce when pressing jump
    /// </summary>
    public float jumpBounce = 7f;

    internal Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            bool upContact = false;

            foreach(ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y < 0f)
                    upContact = true;
            }

            if (upContact)
            {
                if (Input.GetButton("Jump"))
                {
                    player.Bounce(jumpBounce);
                    animator.SetBool("bouncing", true);
                    Debug.Log("Bounce");
                }
                else
                {
                    player.Bounce(normalBounce);
                }

            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            animator.SetBool("bouncing", false);
        }
    }
}
