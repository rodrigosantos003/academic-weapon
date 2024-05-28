using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas healthBarCanvas;
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private GameObject player;
    
    [SerializeField] private EnemySpawner EnemySpawner;
    
    void Start()
    {
        SetVariables();
        EnemySpawner.StartSpawning();
    }

    void SetVariables()
    {
        EnemyController.SetHealthBarCanvas(healthBarCanvas);
        EnemyController.SetHealthBarPrefab(healthBarPrefab);
        EnemyController.SetPlayer(player);
    }
}
