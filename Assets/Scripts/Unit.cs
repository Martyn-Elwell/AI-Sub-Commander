using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

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
        float velocity = Mathf.Abs(agent.velocity.x) + Mathf.Abs(agent.velocity.z);
        Debug.Log(velocity);
        if (velocity > 0.1) { animator.SetBool("Moving", true); }
        else { animator.SetBool("Moving", false); }
    }
}
