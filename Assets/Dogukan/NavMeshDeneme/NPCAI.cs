using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    public NavMeshAgent agent; // NavMeshAgent bileþeni
    public Transform target; // Kedi hedef noktasý
    public Transform cagePosition; // Kafesin hedef noktasý
    public float detectionRange = 10f; // Kediyi tespit etme mesafesi
    public float roamRadius = 20f; // Rastgele gezinme alaný yarýçapý
    public float idleTime = 2f; // Gezindikten sonra duraklama süresi
    public Animator animator; // Animator bileþeni

    private bool isRoaming = false;
    private Vector3 initialPosition;
    private bool isCatInCage = false;
    private bool isHoldingCat = false;

    private void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (animator == null) animator = GetComponent<Animator>();
        initialPosition = transform.position;
        StartCoroutine(Roam());
    }

    private void Update()
    {
        if (isCatInCage || target == null || Vector3.Distance(transform.position, target.position) > detectionRange)
        {
            if (!isHoldingCat)
            {
                // Kediyi tespit edemezse veya kedi kafeste ise rastgele gezinmeye devam et
                if (!isRoaming)
                {
                    StartCoroutine(Roam());
                }
            }
        }
        else if (isHoldingCat)
        {
            // Kediyi tutarken kafese git
            agent.SetDestination(cagePosition.position);
        }
        else
        {
            // Kediyi tespit ederse kediyi kovala
            StopAllCoroutines(); // Rastgele gezinme durdurulur
            isRoaming = false;
            agent.SetDestination(target.position);

            // Hýz hesaplama ve animasyonlarý ayarlama
            float speed = agent.velocity.magnitude;
            UpdateAnimations(speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cage"))
        {
            isCatInCage = true;
            isHoldingCat = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cage"))
        {
            isCatInCage = false;
        }
    }

    public void PickUpCat()
    {
        isHoldingCat = true;
    }

    private IEnumerator Roam()
    {
        isRoaming = true;
        while (true)
        {
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            randomDirection += initialPosition;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1);
            Vector3 finalPosition = hit.position;

            agent.SetDestination(finalPosition);
            float speed = agent.velocity.magnitude;
            UpdateAnimations(speed);

            // Hedefe ulaþtýktan sonra duraklama
            yield return new WaitForSeconds(idleTime);
        }
    }

    private void UpdateAnimations(float speed)
    {
        if (speed <= 0.1f)
        {
            animator.SetFloat("Speed", 0f);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
        else if (speed > 0.1f && speed <= agent.speed)
        {
            animator.SetFloat("Speed", speed);
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }
        else if (speed > agent.speed)
        {
            animator.SetFloat("Speed", speed);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
