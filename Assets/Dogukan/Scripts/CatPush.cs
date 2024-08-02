using UnityEngine;

public class CatPush : MonoBehaviour
{
    public float pushForce = 5f; // �tme kuvveti

    void OnCollisionEnter(Collision collision)
    {
        // E�er �arp�lan obje "Furniture" etiketi ta��yorsa
        if (collision.gameObject.CompareTag("Item"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Objeyi itme kuvveti ile iter
                Vector3 pushDirection = collision.transform.position - transform.position;
                rb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
            }
        }
    }
}
