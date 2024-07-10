using System.Collections;
using UnityEngine;

public class JumpBuffer : MonoBehaviour
{
    public float duration = 3f;
    private bool isActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cat") && !isActive)
        {
            StartCoroutine(DoubleJumpPower(other));
        }
    }

    private IEnumerator DoubleJumpPower(Collider player)
    {
        isActive = true;
        CatMovement catMovement = player.GetComponent<CatMovement>();

        if (catMovement != null)
        {
            catMovement.jumpPower *= 2;

            yield return new WaitForSeconds(duration);

            catMovement.jumpPower /= 2;
        }

        Destroy(gameObject);
    }
}
