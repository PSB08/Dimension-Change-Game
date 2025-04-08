using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO", fileName = "PlayerInputSO", order = 0)]
public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
{
    public event Action<Vector2> OnMovementChange;
    public event Action OnAttackPressed;
    public event Action OnJumpPressed;

    private Controls _controls;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
        }
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        OnMovementChange?.Invoke(input);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnAttackPressed?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnJumpPressed?.Invoke();
    }
    
    
}
