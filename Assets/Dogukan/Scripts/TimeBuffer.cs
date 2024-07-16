using UnityEngine;

public class TimeBuffer : MonoBehaviour
{
    public float timeBonus = 10f; // Eklenecek süre

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cat"))
        {
            TimeCounter timeCounter = FindObjectOfType<TimeCounter>();
            if (timeCounter != null)
            {
                timeCounter.AddTime(timeBonus);
            }
            Destroy(gameObject); // Bufferý yok et
        }
    }
}
