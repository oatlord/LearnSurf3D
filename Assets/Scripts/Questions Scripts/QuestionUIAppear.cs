using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionUIAppear : MonoBehaviour
{
    public CanvasGroup questionCanvas;
    public TextMeshProUGUI questionText;
    public QuestionsManager questionsManager;
    // private int questionNumber;

    // Start is called before the first frame update
    void Start()
    {
        // questionNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            questionCanvas.alpha = 1;
            // questionText.SetText(questionsManager.questionsList[questionNumber]);
            // questionNumber += 1;
            if (questionsManager != null) {
                questionsManager.ShowQuestion();
            } else {
                Debug.LogWarning("QuestionUIAppear: questionsManager reference is not assigned.");
            }
        }
    }
}
