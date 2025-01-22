using UnityEngine;

namespace Player
{
    public class CameraLook : MonoBehaviour
    {
        [SerializeField] private Transform playerBody;
        [SerializeField] private Camera mainCamera;

        [SerializeField] private float sensitivity = 2.0f;
        [SerializeField] private float maxXRotation = 90f;
        [SerializeField] private float minXRotation = -90f;

        private float currentXRotation = 0f;

        public void MouseLook(Vector2 delta)
        {
            RotateLook(delta * sensitivity);
        }

        private void Awake()
        {
            if (mainCamera == null)
                mainCamera = Camera.main;

            if (playerBody == null)
                playerBody = transform;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void RotateLook(Vector2 rotation)
        {
            playerBody.Rotate(Vector3.up * rotation.x);
            
            currentXRotation -= rotation.y;
            currentXRotation = Mathf.Clamp(currentXRotation, minXRotation, maxXRotation);
            
            mainCamera.transform.localRotation = Quaternion.Euler(currentXRotation, 0f, 0f);
        }
    }
}


