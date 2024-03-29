using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform initalSpawnPoint;
    public EnemyState state = EnemyState.IDLE;
    private NavMeshAgent agent;

    [Header("Mesh")]
    [SerializeField] private GameObject Mesh;
    [SerializeField] private List<Material> materials;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string animationName;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Mesh.GetComponent<Renderer>().material = materials[Random.Range(0, materials.Count - 1)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAnimation(string animationString)
    {
        animationName = animationString;
        animator.Play(animationName);
    }
}

public enum EnemyState
{
    IDLE = 0,
    AWARE = 1,
}
