using System.Collections;
using UnityEngine;

public class FastBuffer : MonoBehaviour
{
    private CatMovement catMovement;
    public float speedMultiplier = 2f;
    public float duration = 5f;

    void Start()
    {
        catMovement = GetComponent<CatMovement>();
        if (catMovement == null)
        {
            Debug.LogError("CatMovement component is missing on Cat object.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fastBuffer"))
        {
            StartCoroutine(BoostSpeed());
            Destroy(other.gameObject);
        }
    }

    private IEnumerator BoostSpeed()
    {
        if (catMovement != null)
        {
            float originalWalkSpeed = catMovement.walkSpeed;
            float originalRunSpeed = catMovement.runSpeed;

            catMovement.walkSpeed *= speedMultiplier;
            catMovement.runSpeed *= speedMultiplier;

            yield return new WaitForSeconds(duration);

            catMovement.walkSpeed = originalWalkSpeed;
            catMovement.runSpeed = originalRunSpeed;
        }
    }
}
