using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [Header("Health")]
    public float health = 100f;
    public ParticleSystem blood;

    public void TakeDamage(float damage)
    {
        health -= damage;
        blood.Play();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
