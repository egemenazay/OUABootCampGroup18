using UnityEngine;

public class ObjectPlacementDetector : MonoBehaviour
{
    public MessBarController messBarController; // Daðýnýklýk barý kontrolcüsü
    public float messAmount = 0.1f; // Daðýnýklýk barýnýn azalacaðý miktar

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ItemFallTracker fallTracker = other.GetComponent<ItemFallTracker>();
            if (fallTracker != null && fallTracker.hasFallen && !fallTracker.isPlaced)
            {
                messBarController.DecreaseMessBar(messAmount);
                fallTracker.isPlaced = true; // Eþyanýn yerleþtirildiðini iþaretle
                fallTracker.hasFallen = false; // Eþyanýn düþtüðünü resetle
            }
        }
    }
}
