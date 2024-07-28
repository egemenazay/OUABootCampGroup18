using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class TPSController : NetworkBehaviour
{
    
    public float speed = 5.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float rotationSpeed = 720.0f; // Rotation speed in degrees per second
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    [SerializeField] private Camera _camera;
    void Start()
    {
        if (!isOwned)
        {
            this.enabled = false;
            _camera.enabled = false;
        }
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            // Karakter yere basıyorsa hareket et
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 forward = _camera.transform.forward;
            Vector3 right = _camera.transform.right;
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            moveDirection = forward * vertical + right * horizontal;
            moveDirection *= speed;

            if (moveDirection != Vector3.zero)
            {
                // Karakteri hareket yönüne doğru döndür
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Yerçekimi uygula
        moveDirection.y -= gravity * Time.deltaTime;

        // Karakteri hareket ettir
        controller.Move(moveDirection * Time.deltaTime);
    }
}
