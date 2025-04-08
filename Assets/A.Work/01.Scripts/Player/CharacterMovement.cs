using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
        [SerializeField] private float gravity = -9.8f;
        [SerializeField] private float jumpHeight = 1f;
        [SerializeField] private CharacterController controller;
        [SerializeField] private Transform parent;

        public bool IsGrounded => controller.isGrounded;

        private Vector3 _velocity;
        public Vector3 Velocity => _velocity;
        private float _verticalVelocity;
        private Vector3 _movementDirection;

        public void SetMovementDirection(Vector2 movementInput)
        {
            _movementDirection = new Vector3(movementInput.x, 0, movementInput.y).normalized;
        }

        public void Jump()
        {
            if (IsGrounded)
                _verticalVelocity = Mathf.Sqrt(jumpHeight * -1f * gravity);
        }

        private void FixedUpdate()
        {
            CalculateMovement();
            ApplyGravity();
            Move();
        }

        private void CalculateMovement()
        {
            _velocity = _movementDirection;
            _velocity *= moveSpeed * Time.fixedDeltaTime;

            if (_velocity.magnitude > 0)
            {
                Quaternion targetRot = Quaternion.LookRotation(_velocity);
                float rotationSpeed = 8f;
                parent.rotation = Quaternion.Lerp(parent.rotation, targetRot, Time.fixedDeltaTime * rotationSpeed);
            }
        }

        private void ApplyGravity()
        {
            if (IsGrounded && _verticalVelocity < 0)
            {
                _verticalVelocity = -0.03f;
            }
            else
                _verticalVelocity += gravity * Time.fixedDeltaTime;

            _velocity.y = _verticalVelocity;
        }

        private void Move()
        {
            controller.Move(_velocity);
        }


    
    
}
