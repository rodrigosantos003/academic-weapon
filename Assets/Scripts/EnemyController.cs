using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int speed;
    private float maxHealth;
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
        currentHealth -= damage;
        if (currentHealth <= 0) Destroy(gameObject);
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
            transform.position += (_player.transform.position - transform.position).normalized * (speed * Time.deltaTime);
            KeepHealthBarOnTop();
        }
    }
}
