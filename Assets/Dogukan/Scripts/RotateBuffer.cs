using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 150f; // Döndürme hýzý (derece/saniye)

    void Update()
    {
        // Global Y ekseninde dönüþ
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
