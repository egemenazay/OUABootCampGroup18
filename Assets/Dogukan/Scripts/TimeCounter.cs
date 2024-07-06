using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timeRemaining = 180; // 3 dakika (180 saniye)

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
            // Süre bittiðinde yapýlacak iþlemler
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
