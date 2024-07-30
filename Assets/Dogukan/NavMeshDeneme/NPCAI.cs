using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    public NavMeshAgent agent; // NavMeshAgent bileþeni
    public Transform target; // Hedef nokta (Kedi veya belirli bir nokta)
    public float walkSpeedThreshold = 1.5f; // Yürüyüþ hýzý eþiði
    public float runSpeedThreshold = 2f; // Koþma hýzý eþiði
    public Animator animator; // Animator bileþeni

    private void Start()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (animator == null) animator = GetComponent<Animator>();

        // Baþlangýçta hedef olarak kedi set edilebilir veya baþka bir hedef verilebilir
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);

            // Hýz hesaplama
            float speed = agent.velocity.magnitude;

            // Animasyon durumlarýný belirleme
            if (speed <= 0.1f)
            {
                animator.SetFloat("Speed", 0f);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
            }
            else if (speed > walkSpeedThreshold && speed <= runSpeedThreshold)
            {
                animator.SetFloat("Speed", speed);
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
            }
            else if (speed > runSpeedThreshold)
            {
                animator.SetFloat("Speed", speed);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
            }
        }
    }
}
