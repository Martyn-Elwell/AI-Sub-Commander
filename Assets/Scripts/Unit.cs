using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Animator animator;
    protected UnitSpawner spawnZone;
    public unitEnum type;

    private bool active = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void SetDestination(Vector3 newPos)
    {
        Debug.Log("moving");
        agent.SetDestination(newPos);
    }

    void Update()
    {
        if (!active) { return; }
        float velocity = Mathf.Abs(agent.velocity.x) + Mathf.Abs(agent.velocity.z);
        if (velocity > 0.1) { animator.SetBool("Moving", true); }
        else { animator.SetBool("Moving", false); }
    }

    public void Activate()
    {
        active = true;
    }

    public virtual void DeleteUnit()
    {
        spawnZone.RemoveAndDestroyUnit(transform.gameObject, type);
    }

    public void SetSpawn(UnitSpawner us)
    {
        spawnZone = us;
    }
}
