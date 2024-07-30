using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider sliderStamina;

    private FPSCharController characterController;

    void Start()
    {
        characterController = FindObjectOfType<FPSCharController>();
        if (characterController != null)
        {
            sliderStamina.maxValue = characterController.maxStamina;
            sliderStamina.value = characterController.currentStamina;
        }
        else
        {
            Debug.LogWarning("FPSCharController script not found in the scene.");
        }
    }

    void Update()
    {
        if (characterController != null)
        {
            sliderStamina.value = characterController.currentStamina;
        }
    }
}
