using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAttack : MonoBehaviour
{
    public float attackRange = 1f; // Saldýrý menzili
    public LayerMask interactableLayer; // Etkileþimli objeleri tanýmlamak için katman

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol mouse týklamasýyla saldýr
        {
            Attack();
        }
    }

    void Attack()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, interactableLayer))
        {
            Debug.Log("Hit " + hit.collider.name);
            // Burada hasar vermeyi kaldýrdýk, sadece vurma iþlemini logluyoruz.
        }
    }
}