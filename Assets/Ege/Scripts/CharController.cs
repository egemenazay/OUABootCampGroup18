using UnityEngine;

namespace Cinemachine.Examples
{

    [AddComponentMenu("")]
    public class CharacterMovement : MonoBehaviour
    {
        public bool useCharacterForward = false;
        public bool lockToCameraForward = false;
        public float turnSpeed = 10f;
        public KeyCode sprintJoystick = KeyCode.JoystickButton2;
        public KeyCode sprintKeyboard = KeyCode.Space;

        public float maxStamina = 30f;
        public float currentStamina;
        public float staminaDrainRate = 10f;
        public float staminaRegenRate = 5f;

        private float turnSpeedMultiplier;
        private float speed = 0f;
        private float direction = 0f;
        private bool isSprinting = false;
        private Animator anim;
        private Vector3 targetDirection;
        private Vector2 input;
        private Quaternion freeRotation;
        private Camera mainCamera;
        private float velocity;


        void Start()
        {
            anim = GetComponent<Animator>();
            mainCamera = Camera.main;
            currentStamina = maxStamina;
        }

        void FixedUpdate()
        {
#if ENABLE_LEGACY_INPUT_MANAGER
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            if (useCharacterForward)
                speed = Mathf.Abs(input.x) + input.y;
            else
                speed = Mathf.Abs(input.x) + Mathf.Abs(input.y);

            speed = Mathf.Clamp(speed, 0f, 1f);
            speed = Mathf.SmoothDamp(anim.GetFloat("Speed"), speed, ref velocity, 0.1f);
            anim.SetFloat("Speed", speed);

            if (input.y < 0f && useCharacterForward)
                direction = input.y;
            else
                direction = 0f;

            anim.SetFloat("Direction", direction);

            if ((Input.GetKey(sprintJoystick) || Input.GetKey(sprintKeyboard)) && input != Vector2.zero && direction >= 0f)
            {
                if (currentStamina > 0)
                {
                    isSprinting = true;
                    currentStamina -= staminaDrainRate * Time.deltaTime;
                }
                else
                {
                    isSprinting = false;
                }
            }
            else
            {
                isSprinting = false;
                if (currentStamina < maxStamina)
                {
                    currentStamina += staminaRegenRate * Time.deltaTime;
                }
            }

            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            anim.SetBool("isSprinting", isSprinting);

            UpdateTargetDirection();
            if (input != Vector2.zero && targetDirection.magnitude > 0.1f)
            {
                Vector3 lookDirection = targetDirection.normalized;
                freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
                var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
                var eulerY = transform.eulerAngles.y;

                if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
                var euler = new Vector3(0, eulerY, 0);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * turnSpeedMultiplier * Time.deltaTime);
            }
#else
        InputSystemHelper.EnableBackendsWarningMessage();
#endif
        }

        public virtual void UpdateTargetDirection()
        {
            if (!useCharacterForward)
            {
                turnSpeedMultiplier = 1f;
                var forward = mainCamera.transform.TransformDirection(Vector3.forward);
                forward.y = 0;


                var right = mainCamera.transform.TransformDirection(Vector3.right);


                targetDirection = input.x * right + input.y * forward;
            }
            else
            {
                turnSpeedMultiplier = 0.2f;
                var forward = transform.TransformDirection(Vector3.forward);
                forward.y = 0;


                var right = transform.TransformDirection(Vector3.right);
                targetDirection = input.x * right + Mathf.Abs(input.y) * forward;
            }
        }
    }

}