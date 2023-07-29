using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputProvider
{
    private static GameInputActions _input = new GameInputActions();

    public void Enable()
    {
        _input.Player.Move.Enable();
        _input.Player.Dash.Enable();
        _input.Player.Shield.Enable();
    }

    public void Disable()
    {
        _input.Player.Move.Disable();
        _input.Player.Dash.Disable();
        _input.Player.Shield.Disable();
    }

    public event Action<InputAction.CallbackContext> dashPerformed
    {
        add
        {
            _input.Player.Dash.performed += value;
        }
        remove
        {
            _input.Player.Dash.performed -= value;
        }
    }

    public event Action<InputAction.CallbackContext> shieldPerformed
    {
        add
        {
            _input.Player.Shield.performed += value;
        }
        remove
        {
            _input.Player.Shield.performed -= value;
        }
    }

    public Vector2 MovementInput()
    {
        return _input.Player.Move.ReadValue<Vector2>();
    }
}
