using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum unitState
{
    IDLE,
    MOVING,
    ACTIVE,
    SEARCHING,
    SHOOTING
}

public class Unit : Character
{
    [Header("Core References")]
    protected NavMeshAgent agent;
    protected Animator animator;
    protected UnitSpawner spawnZone;

    [Header("Stats")]
    public unitEnum type;
    public unitState state;
    public bool active = false;
    public float radius = 10f;
    public float rotationSpeed = 2f;
    public Gun gun;
    public LayerMask enemyMask;
    public LayerMask obstructionMask;

    [Header("Task Referencs")]
    public GameObject assignedTask;
    public InteractionType taskType;
    public Vector3 destination;



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        blood = GetComponentInChildren<ParticleSystem>();
    }


    public void SetDestination(Vector3 newPos)
    {
        destination = newPos;
        agent.SetDestination(newPos);
    }

    void Update()
    {
        UpdateMovement();
        if (state == unitState.SEARCHING || state == unitState.SHOOTING)
        {
            UpdateShooting();
        }
        
        
        
    }
    private void UpdateMovement()
    {
        float velocity = Mathf.Abs(agent.velocity.x) + Mathf.Abs(agent.velocity.z);
        if (velocity > 0.1)
        {
            animator.SetBool("Moving", true);
            if (active)
            {
                state = unitState.SEARCHING;
            }
            else
            {
                state = unitState.MOVING;
            }
        }
        else 
        {
            animator.SetBool("Moving", false);
        }
    }

    private void UpdateShooting()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, enemyMask);
        if (rangeChecks.Length != 0)
        {
            bool spotted = false;
            Transform target = null;
            foreach (Collider other in rangeChecks)
            {
                if (other.transform.CompareTag("Enemy"))
                {
                    Vector3 directionToTarget = other.transform.position - transform.position.normalized;
                    float distanceToTarget = Vector3.Distance(transform.position, other.transform.position);
                    if (!Physics.Raycast(transform.position + Vector3.up*1.5f,
                        directionToTarget, distanceToTarget, obstructionMask))
                    {
                        target = other.transform;
                        Debug.Log("Targetting " + target.name);
                        spotted = true;
                        break;
                    }
                        
                }
            }
            if (target != null)
            {
                SetDestination(target.position);
                Vector3 direction = target.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                gun.StartShooting();
                state = unitState.SHOOTING;
            }
            else if (!spotted)
            {
                state = unitState.SEARCHING;
                Debug.Log("Stop");
                gun.StopShooting();
            }
                
        }
    }

    public void Activate()
    {
        active = true;
        state = unitState.SEARCHING;
    }

    public virtual void DeleteUnit()
    {
        spawnZone.RemoveAndDestroyUnit(transform.gameObject, type);
    }

    public void SetSpawn(UnitSpawner us)
    {
        spawnZone = us;
    }

    public void AssignTask(GameObject obj, InteractionType type)
    {
        assignedTask = obj;
        taskType = type;
    }
    
    public void ClearTask()
    {
        assignedTask = null;
        taskType = InteractionType.NONE;
    }
}
