using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions playerInput;
    private Rigidbody rb;

    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        if (rb != null)
        {
            Debug.Log("Rigidbody is present");

            if (playerInput.Player.Move.IsPressed())
            {
                Vector2 inputVector = playerInput.Player.Move.ReadValue<Vector2>();
                Debug.Log("Input Vector: " + inputVector);
                // Vector3 movement = new Vector3(inputVector.x, 0, inputVector.y);
                // rb.MovePosition(transform.position + movement * Time.deltaTime * 5f);
            }
        }
    }
}
