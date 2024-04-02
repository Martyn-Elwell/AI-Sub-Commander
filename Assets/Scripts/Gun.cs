using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Gun : MonoBehaviour
{
    public float damage;
    public bool shooting = false;
    [SerializeField] private ParticleSystem shootingFX;

    private void Start()
    {
        shootingFX = GetComponent<ParticleSystem>();
    }

    public void Shoot()
    {
        shootingFX.Play();
    }
    public void StartShooting()
    {
        if (!shooting) {
            shootingFX.Play();
            shooting = true;
        }
    }

    public void StopShooting()
    {
        if (shooting) {
            shootingFX.Stop();
            shooting = false;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
