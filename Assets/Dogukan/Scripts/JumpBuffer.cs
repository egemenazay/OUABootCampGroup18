using System.Collections;
using UnityEngine;

public class JumpBuffer : MonoBehaviour
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
        if (other.CompareTag("jumpBuffer"))
        {
            StartCoroutine(BoostSpeed());
            Destroy(other.gameObject);
        }
    }

    private IEnumerator BoostSpeed()
    {
        if (catMovement != null)
        {
            float originalJumpPower = catMovement.jumpPower;

           catMovement.jumpPower *= speedMultiplier;

            yield return new WaitForSeconds(duration);

            catMovement.jumpPower = originalJumpPower;
        }
    }
}