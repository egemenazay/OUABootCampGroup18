using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPTItemSystem : MonoBehaviour
{
    public Transform handPosition; // Itemin elde tutulacağı nokta
    private GameObject nearbyItem = null; // Yakındaki item
    private GameObject heldItem = null; // Tutulan item

    void Update()
    {
        // E tuşuna basıldığında item al ya da bırak
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldItem != null)
            {
                DropItem();
            }
            else if (nearbyItem != null)
            {
                PickUpItem();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Eğer bir iteme çarptıysak ve itemi tutmuyoruz
        if (other.CompareTag("Item") && heldItem == null)
        {
            nearbyItem = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Eğer item trigger alanından çıkarsa
        if (other.CompareTag("Item") && nearbyItem == other.gameObject)
        {
            nearbyItem = null;
        }
    }

    void PickUpItem()
    {
        heldItem = nearbyItem;
        heldItem.transform.SetParent(handPosition); // Item'i handPosition'ın çocuğu yap
        heldItem.transform.localPosition = Vector3.zero; // Item'i handPosition ile hizala
        heldItem.transform.localRotation = Quaternion.identity; // Rotation'ı sıfırla
        heldItem.GetComponent<Rigidbody>().isKinematic = true; // Rigidbody'i kinematik yap
        nearbyItem = null; // Yakındaki itemi sıfırla
    }

    void DropItem()
    {
        heldItem.transform.SetParent(null); // Parent'ı sıfırla
        heldItem.GetComponent<Rigidbody>().isKinematic = false; // Rigidbody'i kinematik yapmayı bırak
        heldItem = null; // Tutulan itemi sıfırla
    }
}
