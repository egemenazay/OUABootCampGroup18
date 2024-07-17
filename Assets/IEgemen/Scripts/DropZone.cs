using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        // Eğer bir item doğru bölgeye gelirse
        if (other.CompareTag("Item"))
        {
            Debug.Log("Item doğru yere bırakıldı!");
            // Burada itemin doğru yere bırakıldığına dair işlemler yapabilirsiniz
        }
    }
}
