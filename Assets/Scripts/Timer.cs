using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float remainingTime = 1400f; // Total time in minutes
    private TextMeshProUGUI timerText;
    private float minutes;
    private float seconds;
    private float milliseconds;

    // Start is called before the first frame update
    void Start()
    {
    minutes = Mathf.Floor(remainingTime / 60);
    seconds = Mathf.Floor(remainingTime % 60);
    milliseconds = (remainingTime % 1) * 1000;

        timerText = GetComponentInChildren<TextMeshProUGUI>();

        timerText.SetText(string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds));
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            Debug.Log("Remaining Time: " + remainingTime);
        }
        else
        {
            Debug.Log("Time's up!");
            remainingTime = 0;
        }

        DisplayTime();
    }

    void DisplayTime()
    {

        minutes = Mathf.Floor(remainingTime / 60);
        seconds = Mathf.Floor(remainingTime % 60);
        milliseconds = Mathf.Floor((remainingTime % 1) * 1000);

        string timeString = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        Debug.Log("Time: " + timeString);
        timerText.SetText(string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds));
    }
}
