using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour

    // Visualize the SphereCast in the Scene view
{
    public float rotationSpeed = 5f;
    public float incline;
    public float rotationCheckRadius;
    public float rotationCheckDistance;
    public Transform rotationCheckPoint;
    public Transform groundCheckPoint;
    public float groundDistance = 0.5f;
    public LayerMask groundLayer;
    public LayerMask rotationTriggers;

    private Quaternion targetRotation;
    private bool rotationCheck;
    private bool groundCheck;
    // private bool rotationChanged;
    // private string lastTriggerTag = "";

    
    // Start is called before the first frame update
    void Start()
    {
        targetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Smoothly interpolate to the target rotation
        RaycastHit hit;
        rotationCheck = Physics.SphereCast(rotationCheckPoint.position, rotationCheckRadius, Vector3.down, out hit, rotationCheckDistance, rotationTriggers, QueryTriggerInteraction.Collide);

        RaycastHit groundHit;
        groundCheck = Physics.Raycast(groundCheckPoint.position, Vector3.down, out groundHit, groundDistance, groundLayer);

        // if (rotationCheck)
        // {
        //     string currentTag = hit.transform.tag;
        //     if (currentTag != lastTriggerTag)
        //     {
        //         Debug.Log("Rotation Trigger Hit: " + hit.transform.name);
        //         switch (currentTag)
        //         {
        //             case "LeftRotationTrigger":
        //                 targetRotation = Quaternion.Euler(0, 0, -incline);
        //                 rotationChanged = true;
        //                 break;
        //             case "RightRotationTrigger":
        //                 targetRotation = Quaternion.Euler(0, 0, incline);
        //                 rotationChanged = true;
        //                 break;
        //             case "MiddleRotationTrigger":
        //                 targetRotation = Quaternion.Euler(0, 0, 0);
        //                 rotationChanged = true;
        //                 break;
        //         }
        //         lastTriggerTag = currentTag;
        //     } else {
        //         rotationChanged = false;
        //     }
        // }
        // else
        // {
        //     lastTriggerTag = "";
        // }
        // Debug.Log("Rotation Check: " + rotationCheck);

        // if (rotationChanged)
        // {
        //     transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        // }

        if (rotationCheck)
        {
            // Smoothly rotate player to match the platform normal
            // if (hit.transform.tag.CompareTo("MiddleRotationTrigger") == 0)
            // {
            //     targetRotation = Quaternion.Euler(0, 0, 0);
            // } else {
            //     targetRotation = Quaternion.FromToRotation(transform.up, groundHit.normal) * transform.rotation;
            // }
            // if (hit.transform.tag == "LeftRotationTrigger")
            // {
            //     targetRotation = Quaternion.Euler(0, 0, -incline);
            //     Debug.Log("Left Trigger");
            // }
            // else if (hit.transform.tag == "RightRotationTrigger")
            // {
            //     targetRotation = Quaternion.Euler(0, 0, incline);
            // }
            // else if (hit.transform.tag == "MiddleRotationTrigger")
            // {
            //     targetRotation = Quaternion.Euler(0, 0, 0);
            // }
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, groundHit.normal);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
            // transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject.CompareTag("LeftRotationTrigger"))
        // {
        //     targetRotation = Quaternion.Euler(0, 0, -25);
        //     Debug.Log("Left Rotation Triggered");
        // }
        // else if (other.gameObject.CompareTag("RightRotationTrigger"))
        // {
        //     targetRotation = Quaternion.Euler(0, 0, 25);
        //     Debug.Log("Right Rotation Triggered");
        // }
        // else if (other.gameObject.CompareTag("MiddleRotationTrigger"))
        // {
        //     targetRotation = Quaternion.Euler(0, 0, 0);
        //     Debug.Log("Middle Rotation Triggered");
        // }
    }

    private void OnDrawGizmos()
    {
        if (rotationCheckPoint == null)
            return;
        Gizmos.color = Color.cyan;
        Vector3 start = rotationCheckPoint.position;
        Vector3 end = start + Vector3.down * rotationCheckDistance;
        // Draw the path of the spherecast
        Gizmos.DrawLine(start, end);
        // Draw spheres at start and end
        Gizmos.DrawWireSphere(start, rotationCheckRadius);
        Gizmos.DrawWireSphere(end, rotationCheckRadius);

        if (groundCheckPoint == null)
            return;
        Gizmos.color = Color.black;
        Vector3 groundstart = groundCheckPoint.position;
        Vector3 groundend = groundstart + Vector3.down * groundDistance;
        // Draw the path of the spherecast
        Gizmos.DrawLine(groundstart, groundend);
    }
}
