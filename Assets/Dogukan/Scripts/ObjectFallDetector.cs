using UnityEngine;

public class ObjectFallDetector : MonoBehaviour
{
    public MessBarController messBarController; // Daðýnýklýk barý kontrolcüsü
    public float messAmount = 0.1f; // Daðýnýklýk barýnýn artacaðý miktar

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ItemFallTracker fallTracker = other.GetComponent<ItemFallTracker>();
            if (fallTracker != null)
            {
                if (!fallTracker.hasFallen)
                {
                    messBarController.IncreaseMessBar(messAmount);
                    fallTracker.hasFallen = true; // Eþyanýn yere düþtüðünü iþaretle
                    fallTracker.isPlaced = false; // Eþyanýn yerleþtirildiðini resetle
                }
            }
        }
    }
}
