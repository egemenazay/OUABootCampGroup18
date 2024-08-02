using UnityEngine;

public class ObjectPlacementDetector : MonoBehaviour
{
    public MessBarController messBarController;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ItemFallTracker fallTracker = other.GetComponent<ItemFallTracker>();
            Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (fallTracker != null && fallTracker.hasFallen && !fallTracker.isPlaced)
            {
                messBarController.DecreaseMessBar(rigidbody.mass);
                fallTracker.isPlaced = true; // E�yan�n yerle�tirildi�ini i�aretle
                fallTracker.hasFallen = false; // E�yan�n d��t���n� resetle
            }
        }
    }
}
