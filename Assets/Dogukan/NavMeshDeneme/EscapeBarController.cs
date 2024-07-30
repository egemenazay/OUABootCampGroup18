using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI elemanlarýný kullanmak için gerekli

public class EscapeBarController : MonoBehaviour
{
    public Slider escapeBar; // Basma barýný temsil eden UI Slider
    public float maxFillTime = 3f; // Barýn tamamen dolmasý için gerekli süre
    private float currentFillTime = 0f;
    private bool isInCage = false;
    private GameObject catObject;

    void Update()
    {
        if (isInCage && Input.GetKey(KeyCode.E))
        {
            // E tuþuna basýldýkça barý doldur
            currentFillTime += Time.deltaTime;
            escapeBar.value = currentFillTime / maxFillTime;

            // Bar tamamen dolduysa kediyi serbest býrak
            if (currentFillTime >= maxFillTime)
            {
                ReleaseCat();
            }
        }
        else
        {
            // E tuþu býrakýldýðýnda veya kedi kafeste deðilse barý sýfýrla
            currentFillTime = 0f;
            escapeBar.value = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cat"))
        {
            isInCage = true;
            catObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cat"))
        {
            isInCage = false;
            catObject = null;
        }
    }

    private void ReleaseCat()
    {
        // Kediyi serbest býrak
        if (catObject != null)
        {
            // Kediyi kafesten çýkar
            catObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            isInCage = false;
            currentFillTime = 0f;
            escapeBar.value = 0f;
            catObject = null;

            Debug.Log("Kedi kafesten kaçtý!");
        }
    }
}
