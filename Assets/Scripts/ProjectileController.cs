using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private ProjectileStats stats;
    private void Start()
    {
        if(stats.HasLifeTime) Destroy(gameObject, stats.LifeTime);
    }
    void FixedUpdate()
    {
        transform.position += transform.up * (stats.Speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().TakeDamage(stats.Damage);
            Destroy(gameObject);
        }
    }

}
