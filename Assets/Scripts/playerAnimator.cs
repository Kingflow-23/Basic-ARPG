  using UnityEngine;
using UnityEngine.AI;

public class playerAnimator : MonoBehaviour
{

    private Animator animator;
    private NavMeshAgent agent; 

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }

        // Check for manual movement input:
        bool manualMovement = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || 
                           Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if (Input.GetKey(KeyCode.LeftShift) && manualMovement)
        {
            animator.SetFloat("Speed", 3f);
        }
        else if (manualMovement)
        {
            animator.SetFloat("Speed", 1.5f);
        }
        else
        {
            float speedValue = agent.velocity.magnitude / agent.speed * 3f;
            animator.SetFloat("Speed", speedValue);
            // Debug.Log("Calculated speed parameter: " + speedValue);
        }
    }

    public void setTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

    public void animBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }

}
