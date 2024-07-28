using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public GameObject holdPosition;
    public GameObject targetPosition; // Boþ GameObject referansý
    public float pickUpRange = 2f;  // Bu mesafeyi gerektiði gibi ayarlayýn
    public float placeRange = 2f;   // Yerleþtirme mesafesi
    private GameObject pickableObject;
    private GameObject initialPositionObject; // Nesnenin ilk konumunu saklayan boþ obje
    private Collider[] colliders;
    private bool isHoldingObject = false;
    private Marker marker; // Marker script'ine referans

    void Start()
    {
        // Marker referansýný al
        marker = FindObjectOfType<Marker>();

        if (marker == null)
        {
            Debug.LogError("Marker script not found in the scene.");
        }
    }

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
                        initialPositionObject = GameObject.Find(pickableObject.tag + "Konum"); // Ýlk konumu saklayan objeyi bul

                        if (initialPositionObject == null)
                        {
                            Debug.LogError("Initial position object not found for tag: " + pickableObject.tag);
                        }
                        else
                        {
                            Debug.Log("Initial position object found: " + initialPositionObject.name);
                        }

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

            // Eðer nesne bir kedi veya "Pickable" ise, BoxCollider'ýnýn isTrigger'ýný true yap
            if (pickableObject.CompareTag("Cat") || pickableObject.CompareTag("Pickable") || pickableObject.CompareTag("mug"))
            {
                BoxCollider objectCollider = pickableObject.GetComponent<BoxCollider>();
                if (objectCollider != null)
                {
                    objectCollider.isTrigger = true;
                }

                // Eðer nesne bir kedi ise, 5 saniye sonra býrak
                if (pickableObject.CompareTag("Cat"))
                {
                    Invoke("DropObject", 5f);
                }
            }

            // Nesne tutulurken Rigidbody'yi devre dýþý býrak
            Rigidbody rb = pickableObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            // Marker script'ine initialPosition'ý ayarla
            if (marker != null)
            {
                marker.initialPosition = initialPositionObject?.transform;
                Debug.Log("Marker initialPosition set to: " + marker.initialPosition?.name);
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

            // Eðer nesne bir kedi veya "Pickable" ise, BoxCollider'ýnýn isTrigger'ýný false yap
            if (pickableObject.CompareTag("Cat") || pickableObject.CompareTag("Pickable") || pickableObject.CompareTag("mug"))
            {
                BoxCollider objectCollider = pickableObject.GetComponent<BoxCollider>();
                if (objectCollider != null)
                {
                    objectCollider.isTrigger = false;
                }

                // targetPosition'a olan mesafeyi kontrol et
                if (targetPosition != null && Vector3.Distance(pickableObject.transform.position, targetPosition.transform.position) <= placeRange)
                {
                    pickableObject.transform.position = targetPosition.transform.position;
                    pickableObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    Debug.Log("Nesne targetPosition'a yerleþtirildi: " + pickableObject.name);
                }
                else
                {
                    // Nesneyi zemine yerleþtir
                    RaycastHit hit;
                    if (Physics.Raycast(pickableObject.transform.position, Vector3.down, out hit))
                    {
                        float groundDistance = hit.distance;
                        if (groundDistance > 0.5f)
                        {
                            // Nesneyi zemine taþý
                            pickableObject.transform.position = new Vector3(pickableObject.transform.position.x, pickableObject.transform.position.y - groundDistance + 0.5f, pickableObject.transform.position.z);
                        }
                    }
                }
            }

            // Rigidbody'yi yeniden etkinleþtir
            Rigidbody rb = pickableObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            Debug.Log("Nesne býrakýldý: " + pickableObject.name);
            pickableObject = null;  // Býrakýlan nesneye olan referansý temizle
            initialPositionObject = null; // Ýlk konum referansýný temizle
        }
    }
}
