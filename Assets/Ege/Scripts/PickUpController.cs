using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public GameObject holdPosition;  // Bu deðiþkeni Inspector'da atayýn
    public float pickUpRange = 2f;  // Bu mesafeyi gerektiði gibi ayarlayýn
    private GameObject pickableObject;
    private Collider[] colliders;

    void Update()
    {
        // 'E' tuþuna basýlýp basýlmadýðýný kontrol et
        if (Input.GetKeyDown(KeyCode.E))
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

    public void PickUp()
    {
        if (pickableObject != null)
        {
            // Pickable nesnesini oyuncunun eline iliþtir
            pickableObject.transform.SetParent(holdPosition.transform);
            pickableObject.transform.localPosition = Vector3.zero;  // Nesneyi elde doðru konumlandýr
            pickableObject.transform.localRotation = Quaternion.identity;  // Gerekirse rotasyonu sýfýrla

            Debug.Log("Nesne alýndý: " + pickableObject.name);
        }
    }
}
