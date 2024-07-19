using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    public GameObject collisionVFX; // Çarpýþma VFX prefab'i
    public float vfxLifetime = 2f; // VFX'in sahnede kalma süresi

    private void OnCollisionEnter(Collision collision)
    {
        // Çarpýþma noktasýnda VFX oluþturma
        if (collisionVFX != null)
        {
            GameObject vfxInstance = Instantiate(collisionVFX, collision.contacts[0].point, Quaternion.identity);
            Destroy(vfxInstance, vfxLifetime); // VFX'i belirli bir süre sonra yok et
        }

        // Nesneyi yok et
        Destroy(gameObject);
    }
}
