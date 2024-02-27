using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    private bool active = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void SetDestination(Vector3 newPos)
    {
        agent.SetDestination(newPos);
    }

    void Update()
    {
        if (!active) { return; }
        float velocity = Mathf.Abs(agent.velocity.x) + Mathf.Abs(agent.velocity.z);
        if (velocity > 0.1) { animator.SetBool("Moving", true); }
        else { animator.SetBool("Moving", false); }
    }

    public void activate()
    {
        active = true;
    }
}
