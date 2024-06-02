using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

//lista de inimigos
[Serializable]
public class EnemyList
{
    public GameObject easyEnemy;
    public GameObject mediumEnemy;
    public GameObject hardEnemy;
}

//chances de spawn de um ano
[Serializable]
public class YearSpawningChances
{
    public float easyChance;
    public float mediumChance;
    public float hardChance;

    public float spawnSpeed;
}

//todas as informações do spawn
[Serializable]
public class EnemySpawningChances
{
    public YearSpawningChances firstYear;
    public YearSpawningChances secondYear;
    public YearSpawningChances thirdYear;
    
    public EnemyList enemyList;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float margin;
    [SerializeField] private Transform player;
    
    private List<GameObject> enemies = new List<GameObject>();

    [SerializeField] private float interval;
    static EnemySpawningChances chances;

    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject downWall;
    
    
    public static void SetSpawningChances(EnemySpawningChances EnemySpawningChances)
    {
        chances = EnemySpawningChances;
    }

    public void StartSpawning(float year)
    {
        YearSpawningChances yearChances = null;
        switch (year)
        {
            case 1:
                yearChances = chances.firstYear;
                break;
            
            case 2:
                yearChances = chances.secondYear;
                break;
            
            case 3:
                yearChances = chances.thirdYear;
                break;
        }
        
        StartCoroutine(SpawnEnemy(yearChances));
    }
    
    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnEnemy(YearSpawningChances yearChances)
    {
        while (true)
        {
            //escolhe um de 3 lados para spawnar
            int side = Random.Range(0, 3);
            
            Vector3 pos = Vector3.zero;
            
            switch (side)
            {
                case 0: //esquerda
                    pos = new Vector3(leftWall.transform.position.x - margin, Random.Range(downWall.transform.position.y, leftWall.transform.position.y), 0);
                    break;
                
                case 1: //direita
                    pos = new Vector3(rightWall.transform.position.x + margin, Random.Range(downWall.transform.position.y, rightWall.transform.position.y), 0);
                    break;
                
                case 2: //baixo
                    pos = new Vector3(Random.Range(leftWall.transform.position.x, rightWall.transform.position.x), downWall.transform.position.y - margin, 0);
                    break;
            }
            
            GameObject enemy = null;
            float totalChances = yearChances.easyChance + yearChances.mediumChance + yearChances.hardChance;
            float randomChance = Random.Range(0, totalChances);
            
            if(randomChance < chances.firstYear.easyChance)
            {
                enemy = chances.enemyList.easyEnemy;
            }
            else if(randomChance < chances.firstYear.easyChance + chances.firstYear.mediumChance)
            {
                enemy = chances.enemyList.mediumEnemy;
            }
            else
            {
                enemy = chances.enemyList.hardEnemy;
            }

            GameObject instance = Instantiate(enemy, pos, Quaternion.identity);
            
            enemies.Add(instance);
            yield return new WaitForSeconds(yearChances.spawnSpeed);
        }
    }

    public void KillEnemies()
    {
        foreach(var enemy in enemies)
        {
            if(enemy == null) continue;
            enemy.GetComponent<EnemyController>().Die();
        }
        
        enemies.Clear();
    }
}
