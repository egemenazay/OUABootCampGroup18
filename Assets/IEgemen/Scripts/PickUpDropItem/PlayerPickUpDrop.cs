using UnityEngine;
using UnityEngine.UI;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    //[SerializeField] private Marker marker;
    // [SerializeField] private Image crosshairImage; // Crosshair UI element
    //[SerializeField] private Color defaultCrosshairColor = Color.white;
    //[SerializeField] private Color interactableCrosshairColor = Color.green;

    private ObjectGrabbable objectGrabbable;
    private Transform initialPosition;
    private Transform placementTarget;

    private void Update()
    {
        // UpdateCrosshair();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabbable == null)
            {
                // Not carrying an object, try to grab
                float pickUpDistance = 10f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        initialPosition = raycastHit.transform;
                        objectGrabbable.Grab(objectGrabPointTransform);
                        DetectPlacementTarget();
                    }
                }
            }
            else
            {
                // Currently carrying something, drop
                objectGrabbable.Drop();
                objectGrabbable = null;
                placementTarget = null;
            }
        }
    }

    /*private void UpdateCrosshair()
    {
        float pickUpDistance = 10f;
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
        {
            // Object in range to be picked up
            crosshairImage.color = interactableCrosshairColor;
        }
        else
        {
            // No object in range
            crosshairImage.color = defaultCrosshairColor;
        }
    }*/

    private void DetectPlacementTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("placement"))
            {
                placementTarget = hitCollider.transform;
                return;
            }
        }
    }
}