using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputManager : MonoBehaviour
    {
        [Header("Input Action Asset")] 
        [SerializeField] private InputActionAsset playerControl;
        
        [Header("Action Map Name References")]
        [SerializeField] private string actionMapName = "Player";
        
        [Header("Action Name References")]
        [SerializeField] private string move = "Move";
        [SerializeField] private string jump = "Jump";
        [SerializeField] private string crouch = "Crouch";
        [SerializeField] private string look = "Look";
        [SerializeField] private string sprint = "Sprint";
        //[SerializeField] private string attack = "Attack";
        
        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction crouchAction;
        private InputAction lookAction;
        private InputAction sprintAction;
        //private InputAction attackAction;
        
        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        //public Vector2 AttackInput { get; private set; }
        public float SprintValue { get; private set; }
        public float CrouchValue { get; private set; }
        public bool JumpTriggered { get; private set; }
        
        public static PlayerInputManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Debug.Log("PlayerInputManager instance initialized.");
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            moveAction = playerControl.FindActionMap(actionMapName).FindAction(move);
            lookAction = playerControl.FindActionMap(actionMapName).FindAction(look);
            jumpAction = playerControl.FindActionMap(actionMapName).FindAction(jump);
            sprintAction = playerControl.FindActionMap(actionMapName).FindAction(sprint);
            crouchAction = playerControl.FindActionMap(actionMapName).FindAction(crouch);
            //attackAction = playerControl.FindActionMap(actionMapName).FindAction(attack);
            RegisterInputActions();
        }

        void RegisterInputActions()
        {
            moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
            moveAction.canceled += context => MoveInput = Vector2.zero;
            
            lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
            lookAction.canceled += context => LookInput = Vector2.zero;
            
            //attackAction.performed += context => AttackInput = context.ReadValue<Vector2>();
            //attackAction.performed += context => AttackInput = Vector2.zero;

            jumpAction.performed += context => JumpTriggered = true;
            jumpAction.canceled += context => JumpTriggered = false;
            
            sprintAction.performed += context => SprintValue = context.ReadValue<float>();
            sprintAction.canceled += context => SprintValue = 0f;
            
            crouchAction.performed += context => CrouchValue = context.ReadValue<float>();
            crouchAction.canceled += context => CrouchValue = 0f;
        }

        private void OnEnable()
        {
            moveAction.Enable();
            lookAction.Enable();
            jumpAction.Enable();
            sprintAction.Enable();
            crouchAction.Enable();
            //attackAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.Disable();
            lookAction.Disable();
            jumpAction.Disable();
            sprintAction.Disable();
            crouchAction.Disable();
            //attackAction.Disable();
        }
    } 
}

