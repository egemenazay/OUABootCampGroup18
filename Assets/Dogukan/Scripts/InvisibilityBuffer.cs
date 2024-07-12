using System.Collections;
using UnityEngine;

public class InvisibilityBuffer : MonoBehaviour
{
    private Renderer catRenderer;
    private bool isInvisible = false;

    void Start()
    {
        catRenderer = GetComponent<Renderer>();
        if (catRenderer == null)
        {
            Debug.LogError("Renderer component is missing on Cat object.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("invisibilityBuffer"))
        {
            StartCoroutine(TurnInvisible());
            Destroy(other.gameObject);
        }
    }

    private IEnumerator TurnInvisible()
    {
        if (catRenderer != null)
        {
            isInvisible = true;
            catRenderer.enabled = false;

            yield return new WaitForSeconds(3);

            catRenderer.enabled = true;
            isInvisible = false;
        }
    }
}
