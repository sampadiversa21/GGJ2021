using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveControl : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;

    private bool jumped;
    private int jumpNumber;

    private bool isGrounded;

    private Vector2 inputDirection;

    [SerializeField]
    private float speed = 4.0f;

    [SerializeField]
    private int jumpHeight = 5;

    [SerializeField]
    private int maxJump = 1;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
        jumped = false;
        jumpNumber = 0;
        myRigidbody2D = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputDirection = context.action.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if(jumpNumber < maxJump)
                jumped = true;
        }
    }

    protected void HandleMovement()
    {
        myRigidbody2D.AddForce(new Vector2(Time.deltaTime * speed * inputDirection.x * 100f, 0), ForceMode2D.Force);

        if (jumpNumber > 0 && isGrounded)
        {
            jumpNumber = 0;
        }

        if (jumped && jumpNumber < maxJump)
        {
            jumpNumber++;
            jumped = false;
            isGrounded = false;
            myRigidbody2D.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

}
