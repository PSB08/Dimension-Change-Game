using System;
using UnityEngine;

namespace Script.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private CharacterMovement movement;
        [SerializeField] private PlayerInputSO playerInput;
        private bool isSprinted = false;
    
        private void Awake()
        {
            playerInput.OnMovementChange += HandleMovementChange;
            playerInput.OnJumpPressed += HandleJumpPressed;
            playerInput.OnAttackPressed += HandleAttackPressed;
        }

        private void OnDestroy()
        {
            playerInput.OnMovementChange -= HandleMovementChange;
            playerInput.OnJumpPressed -= HandleJumpPressed;
            playerInput.OnAttackPressed -= HandleAttackPressed;
        }

        private void HandleMovementChange(Vector2 movementnput)
        {
            movement.SetMovementDirection(movementnput);
        }

        private void HandleJumpPressed()
        {
            if (movement.IsGrounded)
                movement.Jump();
        }

        private void HandleAttackPressed()
        {
            Debug.Log("Red Player Attack");
        }

        
        
    }
}

