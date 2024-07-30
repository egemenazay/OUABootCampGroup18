using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private Marker marker; // Marker referansý

    private ObjectGrabbable objectGrabbable;
    private Transform initialPosition; // Nesnenin ilk konumunu saklayacak
    private Transform placementTarget; // Placement target for the marker

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabbable == null)
            {
                // Not carrying an object, try to grab
                float pickUpDistance = 4f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        initialPosition = raycastHit.transform; // Ýlk konumu sakla
                        objectGrabbable.Grab(objectGrabPointTransform);
                        DetectPlacementTarget(); // Detect the placement target
                    }
                }
            }
            else
            {
                // Currently carrying something, drop
                objectGrabbable.Drop();
                marker.ClearTarget(); // Marker'ý temizle
                objectGrabbable = null;
                placementTarget = null; // Clear the placement target
            }
        }
    }

    private void DetectPlacementTarget()
    {
        // Detect the nearest "placement" tagged object
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("placement"))
            {
                placementTarget = hitCollider.transform;
                marker.SetTarget(placementTarget, initialPosition); // Set the marker to the placement target
                return;
            }
        }
    }
}
