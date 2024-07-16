using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectGrabable : MonoBehaviour
{
    private Rigidbody rgbdy;
    private Transform objectGrabPointTransform;
    public Transform objectPutPointTransform;
    private void Awake()
    {
        rgbdy = GetComponent<Rigidbody>();
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        rgbdy.useGravity = false;
    }
    public void Drop()
    {
        objectGrabPointTransform = objectPutPointTransform;
        rgbdy.useGravity = true;
    }
    

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            rgbdy.MovePosition(newPosition);
        }
    }

  
}
