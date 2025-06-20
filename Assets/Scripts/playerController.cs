using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class playerController : MonoBehaviour
{
    private Camera cam;
    private playerMotor motor;
    private NavMeshAgent agent; 
    [SerializeField] public GameObject onClickSpawn;
    public GameObject[] spellPrefabs;   
    private int currentSpell = 0;
    public float castSpeed = .75f;
    public Transform spawnPoint;
    public float castheight = 0.5f;
    private bool canCast = true;
    private Transform target;
    private PlayerAttack attack;
    private playerAnimator animator;
    public LayerMask groundmask; // Layer of the ground
    private float speed = 4f;
    private float rotationSpeed = 2f;
    [SerializeField] private float sprintMultiplier = 1.5f;

    void Start()
    {
        cam = Camera.main; 
        motor = transform.GetComponent<playerMotor>();
        attack = gameObject.GetComponent<PlayerAttack>();
        animator = GetComponent<playerAnimator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void movePlayer(RaycastHit hit)
    {
        motor.move(hit.point);
        Instantiate(onClickSpawn, hit.point + Vector3.up * .2f, Quaternion.Euler(-90, 0, 0));
        // Instantiate(onClickSpawn, hit.point + Vector3.up * .2f, Quaternion.LookRotation (hit.normal));
    }

    void Update()
    {

        for (int i = 0; i < spellPrefabs.Length && i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                currentSpell = i;
            }
        }

        Vector3 move = Vector3.zero;
        bool manualInput = false;
        if (Input.GetKey(KeyCode.W))
        {
            move += Vector3.forward;
            manualInput = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move += Vector3.back;
            manualInput = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move += Vector3.left;
            manualInput = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += Vector3.right;
            manualInput = true;
        }
        
        float currentSpeed = speed;
        if (manualInput && Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= sprintMultiplier;
        }

        move = move.normalized * currentSpeed * Time.deltaTime;

        // If manual WASD input is detected, cancel click movement and move manually.
        if (manualInput)
        {
            // Cancel any existing destination from click movement.
            motor.nullTarget();
            agent.ResetPath();
            agent.Move(move);

            // Rotation Handling
            Vector3 newForward = move.normalized;
            if(newForward != Vector3.zero)
            {
                // Smoothly rotate towards the new direction
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, newForward, rotationSpeed * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }

        if (Input.GetMouseButtonDown(0) && canCast)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, groundmask))
            {
                if ((hit.transform.CompareTag("Enemy") || hit.transform.CompareTag("Box") || hit.transform.CompareTag("GoalBox")) && canCast)
                {
                    target = hit.transform;

                    motor.stop();
                    motor.setTarget(hit.transform);
                    animator.setTrigger("Attack");  
                }
                else
                {
                    movePlayer(hit);
                }
            }
        }   

        if (Input.GetKey(KeyCode.Mouse1))    
        {
            animator.animBool("Cast", true);
            attack.ActivateSpell();
        }
        else
        {
            animator.animBool("Cast", false );
            attack.DeactivateSpell();
        }
    }

    void Cast()
    {
        if (target == null) return;

        canCast = false;

        Vector3 aimpoint = target.position;
        aimpoint.y = castheight;
        
        var prefab = spellPrefabs[currentSpell];
        var go = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        go.transform.LookAt(aimpoint);

        StartCoroutine(resetCastTimer());
    }

    public IEnumerator resetCastTimer()
    {
        canCast = false;
        yield return new WaitForSeconds(castSpeed);
        canCast = true;
    }
}
