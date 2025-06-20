using UnityEngine;
using UnityEngine.AI;

public class playerMotor : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    public float rotationSpeed;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();   
    }

    public void move(Vector3 location)
    {
        nullTarget();
        agent.isStopped = false;
        agent.SetDestination(location);
    }

    public void stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    public void nullTarget()
    {
        target = null;
    }

    public void setTarget(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (target)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            Quaternion lerpRot = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = lerpRot;
        }
    }
}
