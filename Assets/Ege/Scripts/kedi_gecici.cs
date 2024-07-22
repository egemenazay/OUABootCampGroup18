using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetKinematic(bool isKinematic)
    {
        if (rb != null)
        {
            rb.isKinematic = isKinematic;
        }
    }
}
