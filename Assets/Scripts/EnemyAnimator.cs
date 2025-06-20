using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent; 

    private bool isMoving = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isMoving)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);
        }
    }

    public void AnimTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    public void AnimBool(string animName, bool value)
    {
        animator.SetBool(animName, value);
    }
}
