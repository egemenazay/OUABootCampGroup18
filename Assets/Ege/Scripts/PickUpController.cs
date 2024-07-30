using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickUpController : MonoBehaviour
{
    public GameObject holdPosition;
    public GameObject cagePosition; // Kafes pozisyonu
    public float pickUpRange = 2f;  // Kediyi alma mesafesi
    public float placeRange = 2f;   // Yerleþtirme mesafesi
    private GameObject pickableObject;
    private bool isHoldingObject = false;

    void Update()
    {
        // Eðer kedi tutulmuyorsa ve yakýnda bir kedi varsa, onu al
        if (!isHoldingObject)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, pickUpRange);

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Cat"))
                {
                    pickableObject = collider.gameObject;
                    PickUp();
                    break;
                }
            }
        }

        // Eðer bir nesne tutuluyorsa, nesnenin pozisyonunu holdPosition'a eþitle
        if (isHoldingObject && pickableObject != null)
        {
            pickableObject.transform.position = holdPosition.transform.position;
            pickableObject.transform.rotation = holdPosition.transform.rotation;

            // Kafese yakýnsa kediyi býrak
            if (Vector3.Distance(transform.position, cagePosition.transform.position) <= placeRange)
            {
                DropObject();
            }
        }
    }

    public void PickUp()
    {
        if (pickableObject != null)
        {
            isHoldingObject = true;

            // Kediyi oyuncunun eline iliþtir
            pickableObject.transform.SetParent(holdPosition.transform);
            pickableObject.transform.localPosition = Vector3.zero;
            pickableObject.transform.localRotation = Quaternion.identity;

            Debug.Log("Kedi alýndý: " + pickableObject.name);

            // Kediyi kaldýrýrken Collider'ý tetikleyici yap
            Collider objectCollider = pickableObject.GetComponent<Collider>();
            if (objectCollider != null)
            {
                objectCollider.isTrigger = true;
            }

            // Kediyi kaldýrýrken Rigidbody'yi devre dýþý býrak
            Rigidbody rb = pickableObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            // Kediyi yakaladýðýnda hedefi kafes olarak deðiþtir
            GetComponent<NPCAI>().SetTarget(cagePosition.transform);
        }
    }

    void DropObject()
    {
        if (pickableObject != null)
        {
            isHoldingObject = false;

            // Kediyi oyuncunun elinden býrak
            pickableObject.transform.SetParent(null);

            // Kedinin Collider'ýný tetikleyici yapma
            Collider objectCollider = pickableObject.GetComponent<Collider>();
            if (objectCollider != null)
            {
                objectCollider.isTrigger = false;
            }

            // Kafes pozisyonuna kediyi yerleþtir
            if (cagePosition != null && Vector3.Distance(pickableObject.transform.position, cagePosition.transform.position) <= placeRange)
            {
                pickableObject.transform.position = cagePosition.transform.position;
                pickableObject.transform.rotation = cagePosition.transform.rotation;
                Debug.Log("Kedi kafese yerleþtirildi: " + pickableObject.name);
            }
            else
            {
                // Kediyi zemine yerleþtir
                RaycastHit hit;
                if (Physics.Raycast(pickableObject.transform.position, Vector3.down, out hit))
                {
                    float groundDistance = hit.distance;
                    if (groundDistance > 0.5f)
                    {
                        // Kediyi zemine taþý
                        pickableObject.transform.position = new Vector3(pickableObject.transform.position.x, pickableObject.transform.position.y - groundDistance + 0.5f, pickableObject.transform.position.z);
                    }
                }
            }

            // Rigidbody'yi yeniden etkinleþtir
            Rigidbody rb = pickableObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            Debug.Log("Kedi býrakýldý: " + pickableObject.name);
            pickableObject = null;  // Býrakýlan nesneye olan referansý temizle
        }
    }
}
