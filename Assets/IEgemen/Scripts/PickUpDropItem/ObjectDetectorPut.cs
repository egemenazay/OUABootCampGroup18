using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetectorPut : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            Debug.Log("İtem yerleştirildi");
        }
    }
}
