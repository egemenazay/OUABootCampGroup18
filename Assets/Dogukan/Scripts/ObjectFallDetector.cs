using UnityEngine;

public class ObjectFallDetector : MonoBehaviour
{
    public MessBarController messBarController;// Da��n�kl�k bar�n�n artaca�� miktar

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Rigidbody itemRgdbdy = other.gameObject.GetComponent<Rigidbody>(); 
            ItemFallTracker fallTracker = other.GetComponent<ItemFallTracker>();
            if (fallTracker != null)
            {
                if (!fallTracker.hasFallen)
                {
                    messBarController.IncreaseMessBar(itemRgdbdy.mass);
                    fallTracker.hasFallen = true; // E�yan�n yere d��t���n� i�aretle
                    fallTracker.isPlaced = false; // E�yan�n yerle�tirildi�ini resetle
                }
            }
        }
    }
}
