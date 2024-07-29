using UnityEngine;

namespace Cinemachine.Examples
{

    [AddComponentMenu("")]
    public class CharacterMovement : MonoBehaviour
    {
        Rigidbody rigidBody;
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

        [SerializeField] GameObject altRay;
        [SerializeField] GameObject ustRay;
        [SerializeField] float stepHeight = 10f;
        [SerializeField] float stepSmooth = 10f;

        private void Awake()
        {
            ustRay.transform.position = new Vector3(ustRay.transform.position.x, stepHeight, ustRay.transform.position.z);
            rigidBody = GetComponent<Rigidbody>();
        }



        void Start()
        {
            anim = GetComponent<Animator>();
            mainCamera = Camera.main;
            currentStamina = maxStamina;
        }

        void FixedUpdate()
        {
            stepClimb();
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
        void stepClimb()
        {
            RaycastHit hitLower;
            Debug.DrawRay(altRay.transform.position, transform.TransformDirection(Vector3.forward) * 0.1f, Color.red);
            if (Physics.Raycast(altRay.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
            {
                Debug.Log("Alt ray çarptý: " + hitLower.collider.name);
                if (hitLower.collider.CompareTag("Merdiven"))
                {
                    RaycastHit hitUpper;
                    Debug.DrawRay(ustRay.transform.position, transform.TransformDirection(Vector3.forward) * 0.2f, Color.green);
                    if (!Physics.Raycast(ustRay.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f))
                    {
                        Debug.Log("Üst ray çarpmadý, karakter yükseliyor");
                        Vector3 newPosition = rigidBody.position + new Vector3(0f, stepSmooth * Time.deltaTime, 0f);
                        rigidBody.MovePosition(newPosition);
                    }
                }
            }

            RaycastHit hitLower45;
            Debug.DrawRay(altRay.transform.position, transform.TransformDirection(1.5f, 0, 1) * 0.1f, Color.red);
            if (Physics.Raycast(altRay.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitLower45, 0.1f))
            {
                Debug.Log("Alt 45 ray çarptý: " + hitLower45.collider.name);
                if (hitLower45.collider.CompareTag("Merdiven"))
                {
                    RaycastHit hitUpper45;
                    Debug.DrawRay(ustRay.transform.position, transform.TransformDirection(1.5f, 0, 1) * 0.2f, Color.green);
                    if (!Physics.Raycast(ustRay.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitUpper45, 0.2f))
                    {
                        Debug.Log("Üst 45 ray çarpmadý, karakter 45 derece yükseliyor");
                        Vector3 newPosition = rigidBody.position + new Vector3(0f, stepSmooth * Time.deltaTime, 0f);
                        rigidBody.MovePosition(newPosition);
                    }
                }
            }
        }




    }
}