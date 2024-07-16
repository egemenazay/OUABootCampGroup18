using System.Collections;
using UnityEngine;

public class InvisibilityBuffer : MonoBehaviour
{
    private Renderer catRenderer;
    private Collider catCollider;

    void Start()
    {
        catRenderer = GetComponent<Renderer>();
        catCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("invisibilityBuffer"))
        {
            Destroy(other.gameObject);
            StartCoroutine(BecomeInvisible());
        }
    }

    private IEnumerator BecomeInvisible()
    {
        catRenderer.enabled = false;  // Cat objesini görünmez yap
        catCollider.enabled = false;  // Cat objesinin collider'ýný devre dýþý býrak
        yield return new WaitForSeconds(5);  // 5 saniye bekle
        catRenderer.enabled = true;  // Cat objesini tekrar görünür yap
        catCollider.enabled = true;  // Cat objesinin collider'ýný tekrar etkinleþtir
    }
}
