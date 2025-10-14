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
    public LayerMask rotationTriggers;

    private Quaternion targetRotation;
    private bool rotationCheck;
    private string lastTriggerTag = "";

    
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

        if (rotationCheck) {
            string currentTag = hit.transform.tag;
            if (currentTag != lastTriggerTag) {
                Debug.Log("Rotation Trigger Hit: " + hit.transform.name);
                switch (currentTag) {
                    case "LeftRotationTrigger":
                        targetRotation = Quaternion.Euler(0, 0, -incline);
                        break;
                    case "RightRotationTrigger":
                        targetRotation = Quaternion.Euler(0, 0, incline);
                        break;
                    case "MiddleRotationTrigger":
                        targetRotation = Quaternion.Euler(0, 0, 0);
                        break;
                }
                lastTriggerTag = currentTag;
            }
        } else {
            lastTriggerTag = "";
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
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
    }
}
