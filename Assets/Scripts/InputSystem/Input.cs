using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    [SerializeField] private Ladder[] _ladders;
    [SerializeField] private Player _player;

    private Vector2 _direction;

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        _direction = callbackContext.ReadValue<Vector2>();

        if (callbackContext.performed)
        {
            foreach (var ladder in _ladders)
            {
                if (ladder.playerInInteractionZone)
                {
                    ladder.MoveToAnotherFloorForPlayer(_direction.y);
                }
            } 
        }

    }

    private void Update()
    {
        _player.Move(_direction);
    }

    public void OnInteract(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            //
        }
    }

    public void OnSpace(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            _player.Jump();
        }
    }
}