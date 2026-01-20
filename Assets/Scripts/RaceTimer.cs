using UnityEngine;
using TMPro;

public class RaceTimer : MonoBehaviour
{
    public TMP_Text timerText; // same TMP_Text as countdown
    private float timeElapsed;
    private bool isRunning = false; // start paused

    void Update()
    {
        if (!isRunning) return;

        timeElapsed += Time.deltaTime;
        DisplayTime(timeElapsed);
    }

    void DisplayTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        if (timerText != null)
            timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void StartTimer()
    {
        isRunning = true;
    }
}
