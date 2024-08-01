using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraTakip: MonoBehaviour
{
    public Transform target; // Takip edilecek karakter
    public Vector3 offset; // Kamera ile karakter arasýndaki mesafe

    void Start()
    {
        // Baþlangýçta kamera ve karakter arasýndaki mesafeyi hesapla
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // Karakterin pozisyonunu al ve offset'i ekleyerek kamerayý yeni pozisyona ayarla
        Vector3 newPosition = target.position + offset;
        transform.position = newPosition;
    }
}
