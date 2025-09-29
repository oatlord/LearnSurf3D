using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions playerInput;
    private Rigidbody rb;
    // private CapsuleCollider capsuleCollider;
    public LayerMask groundLayer;
    public Transform groundCheckPoint;
    private float movementY = 1f;
    public float slowDownRate = 2f; // How quickly to slow down (units per second)
    public float groundDistance = 0.4f;
    // private float radius;
    public float playerSpeed = 8f;
    public float jumpForce = 2f;
    

    private bool groundCheck;

    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // capsuleCollider = GetComponent<CapsuleCollider>();
        // radius = capsuleCollider.radius;
    }
    void Awake()
    {
        playerInput = new InputSystem_Actions();
    }

    void OnEnable()
    {
        playerInput.Player.Enable();
    }

    void OnDisable() {
        playerInput.Player.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        groundCheck = Physics.CheckSphere(groundCheckPoint.position, groundDistance, groundLayer);

        Debug.Log("Ground Check: " + groundCheck);

        if (rb != null)
        {
            Vector2 inputVector = playerInput.Player.Move.ReadValue<Vector2>();

            // Gradually slow down movementY if SlowDown is pressed
            if (playerInput.Player.SlowDown.IsPressed() && groundCheck)
            {
                Debug.Log("Slow Down action triggered");
                movementY = Mathf.MoveTowards(movementY, 0f, slowDownRate * Time.deltaTime);
            }
            else
            {
                // Restore movementY to 1 when SlowDown is not pressed
                movementY = Mathf.MoveTowards(movementY, 1f, slowDownRate * Time.deltaTime);
            }

            // Use movementY for forward/backward movement
            Vector3 movement = new Vector3(inputVector.x, 0, movementY);
            rb.MovePosition(transform.position + movement * Time.deltaTime * playerSpeed);

            Debug.Log("Rigidbody is present");

            if (playerInput.Player.Jump.IsPressed() && groundCheck)
            {
                Debug.Log("Jump action triggered");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
