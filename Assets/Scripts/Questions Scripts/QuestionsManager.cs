using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> questionsList;
    public TextMeshProUGUI questionText;
    private int questionIndex;
    void Start()
    {
        // If the list wasn't populated in the inspector, initialize it.
        if (questionsList == null)
            questionsList = new List<string>();
        questionIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

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
}
