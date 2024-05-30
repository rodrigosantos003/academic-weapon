using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyStats stats;
    private float maxHealth = 200;
    private float currentHealth;
    
    private static GameObject _healthBarPrefab;
    private static Canvas _healthBarCanvas;
    private ProgressBar _healthBar;
    private GameObject _healthBarObject;

    private static GameObject _player;
    
    public static void SetHealthBarCanvas(Canvas canvas)
    {
        _healthBarCanvas = canvas;
    }
    
    public static void SetHealthBarPrefab(GameObject prefab)
    {
        _healthBarPrefab = prefab;
    }
    
    public static void SetPlayer(GameObject player)
    {
        _player = player;
    }
    
    private void SpawnHealthBar()
    {
        var healthBarPosition = transform.position + new Vector3(0, 1, 0);
        _healthBarObject = Instantiate(_healthBarPrefab, healthBarPosition, Quaternion.identity, _healthBarCanvas.transform);
        _healthBar = _healthBarObject.GetComponent<ProgressBar>();
        _healthBar.SetProgress(currentHealth / maxHealth, 2);
    }
    
    private void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        SpawnHealthBar();
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage * (1 - stats.Defense / 100);
        
        if (currentHealth <= 0){
           Die();
        }
        
        _healthBar.SetProgress(currentHealth / maxHealth, 2);
    }

    public void Die()
    {
        Destroy(gameObject);
        Destroy(_healthBarObject);
    }
    
    private void KeepHealthBarOnTop()
    {
        var healthBarPosition = transform.position + new Vector3(0, 1, 0);
        _healthBarObject.transform.position = healthBarPosition;
    }
    private void FixedUpdate()
    {
        if (_player)
        {
            transform.position += (_player.transform.position - transform.position).normalized * (stats.Speed * Time.deltaTime);
            KeepHealthBarOnTop();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage((int)currentHealth);
            Destroy(gameObject);
            Destroy(_healthBarObject);
        }
    }
}
