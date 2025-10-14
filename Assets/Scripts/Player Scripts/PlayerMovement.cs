using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;

public class PlayerMovement : MonoBehaviour
{
    // Debug visualization for SphereCast

    private InputSystem_Actions playerInput;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    public LayerMask groundLayer;
    public Transform groundCheckPoint;
    private float movementY = 1f;
    public float slowDownRate = 2f; // How quickly to slow down (units per second)
    private float platformRotation;
    public float groundDistance = 0.5f;
    // private float radius;
    public float playerSpeed = 8f;
    public float jumpForce = 2f;
    private bool groundCheck;
    private bool platformCheck;

    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
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

    void OnDisable()
    {
        playerInput.Player.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // groundCheck = Physics.CheckSphere(groundCheckPoint.position, groundDistance, groundLayer);
        // groundCheck = Physics.SphereCast(groundCheckPoint.position, groundDistance, Vector3.down, out hit, groundDistance, groundLayer);
        // groundCheck = Physics.SphereCast(groundCheckPoint.position, capsuleCollider.radius, Vector3.down, out RaycastHit hit, groundDistance, groundLayer);
        RaycastHit hit;
        groundCheck = Physics.Raycast(groundCheckPoint.position, Vector3.down, out hit, groundDistance, groundLayer);
        // if (Physics.SphereCast(groundCheckPoint.position, capsuleCollider.radius, Vector3.down, out RaycastHit hit, groundDistance, groundLayer))
        // {
        //     groundCheck = true;
        //     Debug.Log("Hit: " + hit.transform.name);
        // }
        // else
        // {
        //     groundCheck = false;
        //     // platformCheck = false;
        // }
        // platformRotation = ;

        // if (groundCheck)
        // {
        //     // Smoothly rotate player to match the platform normal
        //     Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        //     transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        // }

        Debug.Log("Ground Check: " + groundCheck);
        // Debug.Log("Hit: " + hit.transform.name);

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

            // Debug.Log("Rigidbody is present");

            if (playerInput.Player.Jump.IsPressed() && groundCheck)
            {
                Debug.Log("Jump action triggered");
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (capsuleCollider == null)
            capsuleCollider = GetComponent<CapsuleCollider>();
        if (groundCheckPoint == null || capsuleCollider == null)
            return;
        Gizmos.color = Color.red;
        Vector3 start = groundCheckPoint.position;
        Vector3 end = start + Vector3.down * groundDistance;
        // Draw the path of the spherecast
        Gizmos.DrawLine(start, end);
    }
}
    