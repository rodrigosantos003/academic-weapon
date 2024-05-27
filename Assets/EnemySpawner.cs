using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private float margin = 20f;
    [SerializeField] private float spawnSpeed = 3;
    [SerializeField] private int EnemiesToSpawn = 10;
    

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0, spawnSpeed);
    }

    private void SpawnEnemy()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
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
        }
    }
}
