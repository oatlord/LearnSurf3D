using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionUIDisappear : MonoBehaviour
{
    // Start is called before the first frame update
    public CanvasGroup questionCanvas;
    public GameObject player;
    public QuestionsManager questionsManager;
    public LayerMask groundLayer;
    public float groundDistance = 0.5f;
    public Transform groundCheckPosition;
    void Start()
    {

    }

    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
    }

    private void OnTriggerExit(Collider other)
    {
        RaycastHit hit;
        if (Physics.Raycast(groundCheckPosition.position, Vector3.down, out hit, groundDistance, groundLayer))
        {
            var tag = hit.transform.tag;
            // Hide the question UI only if player is on the CorrectPlatform
            if (tag == "CorrectPlatform")
            {
                questionCanvas.alpha = 0;
                questionsManager.CorrectAnswer();
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
    // private void OnDrawGizmos()
    // {
    //     if (player == null)
    //         return;

    //     Gizmos.color = Color.green;
    //     Vector3 start = player.transform.position;
    //     Vector3 end = start + Vector3.down * groundDistance;
    //     Gizmos.DrawLine(start, end);

    //     // If the ray would hit something on the selected layer, draw a yellow marker.
    //     if (Physics.Raycast(start, Vector3.down, out RaycastHit hit, groundDistance, groundLayer))
    //     {
    //         Gizmos.color = Color.yellow;
    //         Gizmos.DrawLine(start, hit.point);
    //         Gizmos.DrawSphere(hit.point, 0.05f);
    //     }
    // }
}
