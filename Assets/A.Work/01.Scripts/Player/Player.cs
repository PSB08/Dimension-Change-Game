using System;
using System.Collections;
using UnityEngine;

namespace Script.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private CharacterMovement movement;
        [SerializeField] private PlayerInputSO playerInput;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform trans;
        private bool isSprinted = false;
        public int cnt = 0;
    
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

        private void HandleMovementChange(Vector2 movementInput)
        {
            movement.SetMovementDirection(movementInput);

            bool isMoving = movementInput.magnitude > 0.1f;

            animator.SetBool("WALK", isMoving);
            animator.SetBool("IDLE", !isMoving);
        }

        private void HandleJumpPressed()
        {
            if (movement.IsGrounded)
            {
                movement.Jump();
                animator.SetTrigger("JUMP");
            }
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
            cnt++;
            transform.position = trans.position;
            yield return new WaitForSeconds(1f);
        }
        
        
    }
}

