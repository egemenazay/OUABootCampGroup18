using System.Collections;
using UnityEngine;

public class FastBuffer : MonoBehaviour
{
    public float duration = 3f;
    private bool isActive = false;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Cat") && !isActive)
        {
            StartCoroutine(DoubleSpeed(other));
        }
        
    }

    private IEnumerator DoubleSpeed(Collider player)
    {
        isActive = true;
        CatMovement catMovement = player.GetComponent<CatMovement>();


        if (catMovement != null)
        {
            catMovement.walkSpeed *= 2;
            catMovement.runSpeed *= 2;

            yield return new WaitForSeconds(duration);

            catMovement.walkSpeed /= 2;
            catMovement.runSpeed /= 2;
        }
        Destroy(gameObject);

    }
}
