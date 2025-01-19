using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [SerializeField] public CharacterController controller;
    [SerializeField] public Rigidbody rb;
    
    [SerializeField] public float speed = 8;
    [SerializeField] public float jumpHeight = 3;
    
    [SerializeField] public float gravity = -9.81f;
    [SerializeField] public float groundDistance = 0.4f;
    [SerializeField] public LayerMask groundMask;
    [SerializeField] public Transform groundCheck;

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
