using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    public Transform head;
    public float playerSpeed = 5.0f;
    public float playerAcceleration = 2.0f;
    public float jumpForce = 6.0f;
    public LayerMask groundLayer;

    private Rigidbody rb;

    private PlayerInputActions playerControls;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 move = playerControls.Player.Move.ReadValue<Vector2>();

        Vector3 direction = move.x * head.right + move.y * head.forward;
        rb.velocity = Vector3.Lerp(rb.velocity, direction.normalized * playerSpeed 
            + rb.velocity.y * Vector3.up, playerAcceleration * Time.deltaTime);
        
        if (playerControls.Player.Jump.triggered && isTouchingGround())
        {
            rb.velocity += jumpForce * Vector3.up;
        }
    }

    private bool isTouchingGround()
    {
        return Physics.CheckBox(transform.position, new Vector3(0.1f, 0.1f, 0.1f), Quaternion.identity, groundLayer);
    }

    public void OnEnable()
    {
        playerControls.Enable();
    }

    public void OnDisable()
    {
        playerControls.Disable();
    }
}
