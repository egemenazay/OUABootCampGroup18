using UnityEngine;

public class ObjectFallDetector : MonoBehaviour
{
    public MessBarController messBarController; // Daðýnýklýk barý kontrolcüsü
    public float messAmount = 0.1f; // Daðýnýklýk barýnýn artacaðý miktar

    void OnTriggerEnter(Collider other)
    {
        // Eðer düþen obje "Furniture" etiketi taþýyorsa
        if (other.CompareTag("Furniture"))
        {
            ItemFallTracker fallTracker = other.GetComponent<ItemFallTracker>();
            if (fallTracker != null && !fallTracker.hasFallen)
            {
                messBarController.IncreaseMessBar(messAmount);
                fallTracker.hasFallen = true; // Eþyanýn yere düþtüðünü iþaretle
            }
        }
    }
}
