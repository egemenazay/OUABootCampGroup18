using UnityEngine;

public class CatPush : MonoBehaviour
{
    public float pushForce = 5f; // Ýtme kuvveti

    void OnCollisionEnter(Collision collision)
    {
        // Eðer çarpýlan obje "Furniture" etiketi taþýyorsa
        if (collision.gameObject.CompareTag("Furniture"))
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
