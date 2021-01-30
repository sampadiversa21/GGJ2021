using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputVec = context.action.ReadValue<Vector2>();

        Debug.Log("Moved!");
        Debug.Log(inputVec);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
            Debug.Log("Jump");
    }
}
