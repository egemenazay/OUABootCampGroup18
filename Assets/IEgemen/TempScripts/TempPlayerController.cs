using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TempPlayerController : MonoBehaviour
{
    private CharacterController controler;
    public Transform cam;
    public Transform playerTransform;
    public bool pickUpAvailable = true;       //pickup yapılabilir mi check

    public GameObject hand;
    public HUD hud; 
    
    public float speed = 6f;
    
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private void Start()
    {
        controler = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        Vector3 diretcion = new Vector3(Horizontal, 0f, Vertical).normalized;

        if (diretcion.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(diretcion.x, diretcion.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controler.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
    
    //PICKUP SCRIPTLERI ASIL PLAYERCONTROLLER'A EKLENİCEK

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickable") && pickUpAvailable)
        {
            hud.OpenMassagePanel();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Pickable"))
        {
            hud.CloseMassagePanel();
        }
    }
}
