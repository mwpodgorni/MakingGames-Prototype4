using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float totalTime = 120.0f; // 2 minutes
    private float currentTime;
    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = totalTime;
        UpdateTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
            {
                currentTime = 0;
            }
            UpdateTimerText();
        }
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = timeString;
        }
    }
}