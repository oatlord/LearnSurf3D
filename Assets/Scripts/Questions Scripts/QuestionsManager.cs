using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> questionsList;
    public List<string> choice1List;
    public List<string> choice2List;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI choice1Text;
    public TextMeshProUGUI choice2Text;
    public GameObject player;
    private int questionIndex;
    private int choice1Index;
    private int choice2Index;
    void Start()
    {
        // If the list wasn't populated in the inspector, initialize it.
        if (questionsList == null)
            questionsList = new List<string>();

        if (choice1List == null)
            choice1List = new List<string>();

        if (choice2List == null)
            choice2List = new List<string>();

        questionIndex = 0;
        choice1Index = 0;
        choice2Index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // RaycastHit hit;
        // if (Physics.Raycast(player.transform.position, Vector3.down, out hit, groundDistance, groundLayer))
        // {
        //     Debug.Log("Player is grounded on: " + hit.transform.name);
        // }

    }

    public void ShowQuestion()
    {
        if (questionsList == null || questionsList.Count == 0)
        {
            Debug.LogWarning("QuestionsManager: questionsList is empty or null.");
            return;
        }

        if (questionText == null)
        {
            Debug.LogWarning("QuestionsManager: questionText reference is not assigned.");
            return;
        }

        // Ensure index is in range
        if (questionIndex < 0 || questionIndex >= questionsList.Count)
            questionIndex = 0;

        questionText.SetText(questionsList[questionIndex]);

        // Advance index and wrap around so subsequent calls show next question
        questionIndex = (questionIndex + 1) % questionsList.Count;
    }

    public void ShowChoices()
    {
        // Similar implementation can be done for choices if needed
        if (choice1List == null || choice1List.Count == 0)
        {
            Debug.LogWarning("QuestionsManager: choice1List is empty or null.");
            return;
        }

        if (choice2List == null || choice2List.Count == 0)
        {
            Debug.LogWarning("QuestionsManager: choice2List is empty or null.");
            return;
        }

        if (choice1Index < 0 || choice1Index >= choice1List.Count)
            choice1Index = 0;

        if (choice2Index < 0 || choice2Index >= choice2List.Count)
            choice2Index = 0;

        choice1Text.SetText(choice1List[choice1Index]);
        choice2Text.SetText(choice2List[choice2Index]);

        choice1Index = (choice1Index + 1) % choice1List.Count;
        choice2Index = (choice2Index + 1) % choice2List.Count;
    }

    public void CorrectAnswer() {
        player.GetComponent<PlayerMovement>().playerSpeed += 2f;
    }
}
