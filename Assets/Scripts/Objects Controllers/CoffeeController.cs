using System;
using UnityEngine;

public class CoffeeManager : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().Heal(200))
                Destroy(gameObject);
        }
    }
}
