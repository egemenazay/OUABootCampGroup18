using UnityEngine;
using TMPro;
using Unity.Netcode;

public class TimeCounter : NetworkBehaviour
{
    public TextMeshProUGUI timerText;
    public static float timeRemaining = 30; // 3 dakika (180 saniye)
    public static NetworkVariable<float> syncedTimeRemaining = new NetworkVariable<float>(timeRemaining);

    void Start()
    {
        UpdateTimerText();
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            timeRemaining = 0;
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void AddTime(float seconds)
    {
        timeRemaining += seconds;
        UpdateTimerText();
    }
}
