using UnityEngine;
using UnityEngine.AI;

public class EnemyMotor : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    private Transform target;
    public float rotationSpeed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void Update()
    {
        if (target)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            Quaternion lerpRot = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = lerpRot; 
        }   
    }

    public void move(Vector3 location)
    {
        nullTarget();
        agent.isStopped = false;
        agent.SetDestination(location);
    }

    public void moveToTarget()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            Debug.Log("Target is null in moveToTarget()");
        }
    }

    public void stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    public void setTarget(Transform t)
    {
        target = t;
    }

    public void nullTarget()
    {
        target = null;
    }

}

