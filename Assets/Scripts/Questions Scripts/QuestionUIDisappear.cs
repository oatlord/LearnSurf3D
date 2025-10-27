using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionUIDisappear : MonoBehaviour
{
    // Start is called before the first frame update
    public CanvasGroup questionCanvas;
    public GameObject player;
    public LayerMask groundLayer;
    public float groundDistance = 0.5f;
    public Transform groundCheckPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Visualize the ground raycast in play mode
        DrawGroundRaycast();
    }

    // Draw the raycast from the player's position downward for debug visualization (play mode)
    private void DrawGroundRaycast()
    {
        // if (player == null)
        //     return;

        // Vector3 start = player.transform.position;
        // Vector3 dir = Vector3.down;
        // Vector3 end = start + dir * groundDistance;

        // // Draw main line (visible in Scene and Game view during Play)
        // Debug.DrawLine(start, end, Color.green);

        // // If it hits something, draw to the hit point in yellow
        // if (Physics.Raycast(start, dir, out RaycastHit hit, groundDistance, groundLayer))
        // {
        //     Debug.DrawLine(start, hit.point, Color.yellow);
        //     // draw a small marker at the hit point
        //     Debug.DrawLine(hit.point + Vector3.up * 0.1f, hit.point - Vector3.up * 0.1f, Color.yellow);
        //     Debug.DrawLine(hit.point + Vector3.right * 0.1f, hit.point - Vector3.right * 0.1f, Color.yellow);
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other.CompareTag("Player"))
        // {
        //     if (questionCanvas == null || player == null)
        //     {
        //         Debug.LogWarning("QuestionUIDisappear: questionCanvas or player reference is missing.");
        //         return;
        //     }
        //     // Raycast down from the player's position to determine which platform they're standing on
        // }
    }

    private void OnTriggerExit(Collider other) {
        RaycastHit hit;
            if (Physics.Raycast(groundCheckPosition.position, Vector3.down, out hit, groundDistance, groundLayer))
            {
                var tag = hit.transform.tag;
                // Hide the question UI only if player is on the CorrectPlatform
                if (tag == "CorrectPlatform")
                {
                    questionCanvas.alpha = 0;
                    Debug.Log("correct answer");
                }
                else if (tag == "WrongPlatform")
                {
                    // On wrong platform: ensure question UI stays visible
                    questionCanvas.alpha = 0;
                    Debug.Log("wrong answer");
                }
                else
                {
                    // Default: keep the canvas visible
                    questionCanvas.alpha = 1;
                }
            }
            else
        {
                // If we can't detect ground, keep UI visible to be safe
                Debug.Log("ground not detected");
                questionCanvas.alpha = 1;
            }
    }

    // Editor visualization: draws the same ray and hit marker in the Scene view while editing
    private void OnDrawGizmos()
    {
        if (player == null)
            return;

        Gizmos.color = Color.green;
        Vector3 start = player.transform.position;
        Vector3 end = start + Vector3.down * groundDistance;
        Gizmos.DrawLine(start, end);

        // If the ray would hit something on the selected layer, draw a yellow marker.
        if (Physics.Raycast(start, Vector3.down, out RaycastHit hit, groundDistance, groundLayer))
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(start, hit.point);
            Gizmos.DrawSphere(hit.point, 0.05f);
        }
    }
}
