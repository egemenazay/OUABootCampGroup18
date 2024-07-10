using System.Collections;
using UnityEngine;

public class InvisibilityBuffer : MonoBehaviour
{
    public float duration = 3f;
   // private bool isActive = false;
    public GameObject catbody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cat"))
        {
            StartCoroutine(MakeInvisible(other));
        }
    }

    private IEnumerator MakeInvisible(Collider player)
    {
        //isActive = true;
        this.gameObject.SetActive(false);
        MeshRenderer playerRenderer = catbody.GetComponentInChildren<MeshRenderer>();

        if (playerRenderer != null)
        {
            playerRenderer.enabled = false;

            yield return new WaitForSeconds(duration);

            playerRenderer.enabled = true;
        }
        else
        {
            Debug.LogWarning("Player does not have Renderer component.");
        }

       
    }
}
