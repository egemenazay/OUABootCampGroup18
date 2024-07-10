using Cinemachine.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staminaBar : MonoBehaviour
{
    public float maxStamina = 30f;
    public float currentStamina;
    public Slider sliderStamina;

    private CharacterMovement characterMovement;

    void Start()
    {
        characterMovement = FindObjectOfType<CharacterMovement>();
        if (characterMovement != null)
        {
            maxStamina = characterMovement.maxStamina;
            currentStamina = characterMovement.currentStamina;
        }
        else
        {
            Debug.LogWarning("CharacterMovement script not found in the scene.");
        }

        sliderStamina.maxValue = maxStamina;
        sliderStamina.value = currentStamina;
    }

    void Update()
    {
        if (characterMovement != null)
        {
            currentStamina = characterMovement.currentStamina;
            sliderStamina.value = currentStamina;
        }
    }
}
