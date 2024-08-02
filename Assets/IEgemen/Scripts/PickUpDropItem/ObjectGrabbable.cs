using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class ObjectGrabbable : NetworkBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    private Collider boxCollider;
    private NetworkTransform networkTransform;
    public NetworkVariable<Vector3> NetworkPosition = new NetworkVariable<Vector3>();
    public NetworkVariable<Quaternion> NetworkRotation = new NetworkVariable<Quaternion>();
    
    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<Collider>();
        networkTransform = GetComponent<NetworkTransform>();
        if (networkTransform == null)
        {
            networkTransform = gameObject.AddComponent<NetworkTransform>();
        }
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
        boxCollider.enabled = false;
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
        boxCollider.enabled = true;
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition);
        }
    }
}
