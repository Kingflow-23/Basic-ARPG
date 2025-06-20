using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float health = 50f;
    public float patrolRadius;
    public float agroRadius;
    public float attackRadius;
    public float minPatrolTime, maxPatrolTime;
    public float attackCooldown = 1.5f;
    [SerializeField] private LayerMask playerMask;

    private NavMeshAgent agent;
    private EnemyMotor motor;
    private EnemyAnimator animator;
    private EnemyHealth enemyHealth;
    private EnemySpawner spawner;

    private Transform target;
    private bool isAlive = true;
    private bool isPatrolling = false;
    private bool isAgroed = false;
    private bool isAttacking = false;

    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        motor = GetComponent<EnemyMotor>();
        animator = GetComponent<EnemyAnimator>();
        enemyHealth = GetComponent<EnemyHealth>();
        spawner = FindFirstObjectByType<EnemySpawner>();
    }

    void Update()
    {
        if (!isAlive) return;

        CheckAgro();

        if (isAgroed)
        {
            HandleChaseAndAttack();
        }
        else if (!isPatrolling)
        {
            StartCoroutine(ChoosePatrolLocation());
        }
    }

    IEnumerator ChoosePatrolLocation()
    {
        isPatrolling = true;

        Vector3 randomPoint = transform.position + Random.insideUnitSphere * patrolRadius;
        randomPoint.y = 0;
        motor.move(randomPoint);

        float waitTime = Random.Range(minPatrolTime, maxPatrolTime);
        float timer = 0f;

        while (timer < waitTime)
        {
            if (isAgroed)
            {
                isPatrolling = false;
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        isPatrolling = false;
        StartCoroutine(ChoosePatrolLocation());
    }

    void CheckAgro()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, agroRadius, playerMask);
        bool foundPlayer = false;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                target = hit.transform;
                motor.setTarget(target);
                isAgroed = true;
                foundPlayer = true;
                Debug.Log("Player detected: " + target.name);
                break;
            }
        }

        if (!foundPlayer)
        {
            isAgroed = false;
            target = null;
            motor.nullTarget();
        }
    }

    void HandleChaseAndAttack()
    {
        if (target == null) return;

        motor.moveToTarget();

        if (CanAttack())
        {
            isAttacking = true;
            animator.AnimTrigger("Attack");
            lastAttackTime = Time.time + attackCooldown;
        }
    }

    bool CanAttack()
    {
        return !isAttacking && Time.time > lastAttackTime && Vector3.Distance(transform.position, target.position) <= agent.stoppingDistance;
    }

    void Attack()  // Called from animation event
    {
        if (target == null || !target.gameObject.activeInHierarchy) return;

        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(10f);
        Debug.Log("Player attacked!");
    }

    public void resetAttack()  // Called from animation event
    {
        isAttacking = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Spell"))
        {
            enemyHealth.TakeDamage(3);
            OnDestroy();  // Notify spawner
        }
    }

    void Death()
    {
        isAlive = false;
        animator.AnimTrigger("Death");
        motor.nullTarget();
        Debug.Log("Enemy died.");
    }

    void OnDestroy()
    {
        spawner.OnEnemyDestroyed();
    }
}
