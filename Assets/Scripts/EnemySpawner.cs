using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float margin;
    [SerializeField] private float spawnSpeed;
    [SerializeField] private int enemiesToSpawn;
    [SerializeField] private Transform player;
    

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemy(enemy, spawnSpeed));
    }

    private IEnumerator SpawnEnemy(GameObject enemy, float interval)
    {
        Camera mainCamera = Camera.main;
        
        while (enemiesToSpawn > 0)
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

            Instantiate(enemy, pos, Quaternion.identity);

            enemiesToSpawn--;

            yield return new WaitForSeconds(interval);
        }
    }
}
