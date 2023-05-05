using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        var direction = callbackContext.ReadValue<Vector2>();
        //
    }

    public void OnInteract(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            //
        }
    }
}
