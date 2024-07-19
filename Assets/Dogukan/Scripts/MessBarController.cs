using UnityEngine;
using UnityEngine.UI;

public class MessBarController : MonoBehaviour
{
    public Slider messBar; // Daðýnýklýk barý UI Slider

    // Daðýnýklýk barýný artýrmak için çaðýrýlacak metot
    public void IncreaseMessBar(float amount)
    {
        messBar.value += amount;
        if (messBar.value > messBar.maxValue)
        {
            messBar.value = messBar.maxValue;
        }
    }
}
