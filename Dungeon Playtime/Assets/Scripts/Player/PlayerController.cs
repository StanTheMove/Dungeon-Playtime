using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement speed")]
        [SerializeField] private float walkSpeed = 3.0f;
        [SerializeField] private float sprintMultiplier = 2.0f;
        [SerializeField] private float crouchSpeed = 1.0f;
        
        [Header("Jump Parameters")]
        [SerializeField] private float jumpForce = 5.0f;
        [SerializeField] private float gravity = 9.81f;
        
        private CharacterController characterController;
        private PlayerInputManager inputManager;
        private CameraLook cameraLook;
        private Vector3 currentMovement;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            inputManager = PlayerInputManager.Instance;
            
            if (cameraLook == null)
                cameraLook = GetComponentInChildren<CameraLook>();

            if (inputManager == null)
                Debug.LogError("PlayerInputManager.Instance is not initialized. Ensure it is active in the scene.");
            
            if (cameraLook == null)
                Debug.LogError("CameraLook component is missing or not assigned.");
        }

        private void Update()
        {
            if (inputManager == null || cameraLook == null)
                return;
            
            ManageMovement();
            cameraLook.MouseLook(inputManager.LookInput);
        }

        void ManageMovement()
        {
            if (inputManager == null) return;

            float speed = walkSpeed * (inputManager.SprintValue > 0 ? sprintMultiplier : 1f);

            Vector3 inputDirection = new Vector3(inputManager.MoveInput.x, 0f, inputManager.MoveInput.y);
            Vector3 worldDirection = transform.TransformDirection(inputDirection);
            worldDirection.Normalize();

            currentMovement.x = worldDirection.x * speed;
            currentMovement.z = worldDirection.z * speed;

            ManageJumping();
            characterController.Move(currentMovement * Time.deltaTime);
        }

        void ManageJumping()
        {
            if (characterController.isGrounded)
            {
                currentMovement.y = -0.5f;

                if (inputManager.JumpTriggered)
                {
                    currentMovement.y = jumpForce;
                }
            }
            else
            {
                currentMovement.y -= gravity * Time.deltaTime;
            }
        }
    }
}
