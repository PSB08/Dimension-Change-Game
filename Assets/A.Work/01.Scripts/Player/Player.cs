using System;
using System.Collections;
using UnityEngine;

namespace Script.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private CharacterMovement movement;
        [SerializeField] private PlayerInputSO playerInput;
        [SerializeField] private Transform trans;
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
            
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.layer == LayerMask.NameToLayer("ResetGround"))
            {
                StartCoroutine(RespawnCoroutine());
            }
        }

        private IEnumerator RespawnCoroutine()
        {
            transform.position = trans.position;
            yield return new WaitForSeconds(1f);
        }
        
        
    }
}

