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
        Camera mainCamera = Camera.main;
        
        while (true)
        {
            Vector3 cameraBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            Vector3 cameraTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

            float minX = cameraBottomLeft.x - margin;
            float maxX = cameraTopRight.x + margin;
            float minY = cameraBottomLeft.y - margin;
            float maxY = cameraTopRight.y + margin;

            int side = Random.Range(0, 4);

            float randomX = 0;
            float randomY = 0;

            switch (side)
            {
                case 0: // Esquerda
                    randomX = Random.Range(minX, cameraBottomLeft.x);
                    randomY = Random.Range(minY, maxY);
                    break;
                case 1: // Direita
                    randomX = Random.Range(cameraTopRight.x, maxX);
                    randomY = Random.Range(minY, maxY);
                    break;
                case 2: // Baixo
                    randomX = Random.Range(minX, maxX);
                    randomY = Random.Range(minY, cameraBottomLeft.y);
                    break;
                case 3: // Cima
                    randomX = Random.Range(minX, maxX);
                    randomY = Random.Range(cameraTopRight.y, maxY);
                    break;
            }

            var pos = new Vector2(randomX, randomY);
            
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
            yield return new WaitForSeconds(interval);
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
