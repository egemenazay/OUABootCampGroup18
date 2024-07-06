using UnityEngine;

public class TimeBufferInteraction : MonoBehaviour
{
    private TimeCounter countdownTimer;

    void Start()
    {
        countdownTimer = FindObjectOfType<TimeCounter>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("timeBuffer"))
        {
            countdownTimer.AddTime(10); // 10 saniye ekle
            Destroy(other.gameObject); // Buffer nesnesini yok et
        }
    }
}
