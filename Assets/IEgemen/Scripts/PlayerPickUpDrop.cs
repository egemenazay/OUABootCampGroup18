using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerPickUpTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private LayerMask dropLayerMask;
    private ObjectGrabable _objectGrabable;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float pickUpDistance = 2f;
            if (_objectGrabable == null)
            {
                //obje elimizde yok ise almaya çalışıyor
                if (Physics.Raycast(playerPickUpTransform.position,playerPickUpTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    Debug.Log(raycastHit.transform);
                    if (raycastHit.transform.TryGetComponent(out _objectGrabable))
                    {
                        _objectGrabable.Grab(objectGrabPointTransform);
                    }
                }
            }//elimizde obje varken doğru yere koymaya çalışıyor
            else
            {
                if (Physics.Raycast(playerPickUpTransform.position,playerPickUpTransform.forward, out RaycastHit raycastHit, pickUpDistance, dropLayerMask))
                {
                    _objectGrabable.Drop();
                }
            }
            
        }
    }
}
