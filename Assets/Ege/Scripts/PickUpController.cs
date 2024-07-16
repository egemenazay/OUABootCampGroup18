using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public GameObject holdPosition;
    public float pickUpRange = 2f;  // Bu mesafeyi gerektiði gibi ayarlayýn
    private GameObject pickableObject;
    private Collider[] colliders;
    private bool isHoldingObject = false;

    void Update()
    {
        // 'E' tuþuna basýlýp basýlmadýðýný kontrol et
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHoldingObject)
            {
                // Eðer bir nesne tutuluyorsa, býrak
                DropObject();
            }
            else
            {
                // pickUpRange içindeki tüm collider'larý bul
                colliders = Physics.OverlapSphere(transform.position, pickUpRange);

                // Tüm collider'lar üzerinde dolaþ
                foreach (Collider collider in colliders)
                {
                    // Collider'ýn "Pickable" veya "Cat" tag'ine sahip olup olmadýðýný kontrol et
                    if (collider.CompareTag("Pickable") || collider.CompareTag("Cat"))
                    {
                        pickableObject = collider.gameObject;
                        PickUp();
                        break;  // Bir nesne alýndýðýnda kontrol etmeyi durdur
                    }
                }
            }
        }

        // Eðer bir nesne tutuluyorsa, nesnenin pozisyonunu holdPosition'a eþitle
        if (isHoldingObject && pickableObject != null)
        {
            pickableObject.transform.position = holdPosition.transform.position;
            pickableObject.transform.rotation = holdPosition.transform.rotation;
        }
    }

    public void PickUp()
    {
        if (pickableObject != null)
        {
            isHoldingObject = true;

            // Pickable nesnesini oyuncunun eline iliþtir
            pickableObject.transform.SetParent(holdPosition.transform);
            pickableObject.transform.localPosition = Vector3.zero;  // Nesneyi elde doðru konumlandýr
            pickableObject.transform.localRotation = Quaternion.identity;  // Gerekirse rotasyonu sýfýrla

            Debug.Log("Nesne alýndý: " + pickableObject.name);

            // Eðer nesne bir kedi ise, BoxCollider'ýnýn isTrigger'ýný true yap
            if (pickableObject.CompareTag("Cat"))
            {
                BoxCollider catCollider = pickableObject.GetComponent<BoxCollider>();
                if (catCollider != null)
                {
                    catCollider.isTrigger = true;
                }
                Invoke("DropObject", 5f);
            }

            // Nesne tutulurken Rigidbody'yi devre dýþý býrak
            Rigidbody rb = pickableObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }

    void DropObject()
    {
        if (pickableObject != null)
        {
            isHoldingObject = false;

            // Nesneyi oyuncunun elinden býrak
            pickableObject.transform.SetParent(null);

            // Eðer nesne bir kedi ise, BoxCollider'ýnýn isTrigger'ýný false yap
            if (pickableObject.CompareTag("Cat"))
            {
                BoxCollider catCollider = pickableObject.GetComponent<BoxCollider>();
                if (catCollider != null)
                {
                    catCollider.isTrigger = false;
                }
            }

            // Rigidbody'yi yeniden etkinleþtir
            Rigidbody rb = pickableObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

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

            Debug.Log("Nesne býrakýldý: " + pickableObject.name);
            pickableObject = null;  // Býrakýlan nesneye olan referansý temizle
        }
    }
}
