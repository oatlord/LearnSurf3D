using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions playerInput;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    public LayerMask groundLayer;
    private float radius;
    public float playerSpeed = 8f;
    public float jumpForce = 2f;

    private bool groundCheck;

    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        radius = capsuleCollider.radius;
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
        groundCheck = Physics.CheckSphere(transform.position + Vector3.up*(radius*0.9f), radius, groundLayer);
        Debug.Log("Ground Check: " + groundCheck);

        if (rb != null)
        {
            Vector2 inputVector = playerInput.Player.Move.ReadValue<Vector2>();
            rb.MovePosition(transform.position + new Vector3(0, 0, 1) * Time.deltaTime * playerSpeed);

            Debug.Log("Rigidbody is present");

            if (playerInput.Player.Move.IsPressed())
            {
                Debug.Log("Input Vector: " + inputVector);
                Vector3 movement = new Vector3(inputVector.x, 0, 1);
                rb.MovePosition(transform.position + movement * Time.deltaTime * playerSpeed);
            }

            if (playerInput.Player.Jump.IsPressed() && groundCheck)
            {
                Debug.Log("Jump action triggered");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
