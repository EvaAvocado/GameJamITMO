using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    [SerializeField] private Ladder[] _ladders;
    
    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        var direction = callbackContext.ReadValue<Vector2>();

        foreach (var ladder in _ladders)
        {
            if (ladder.playerInInteractionZone)
            {
                ladder.MoveToAnotherFloorForPlayer(direction.y);
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            //
        }
    }
}
