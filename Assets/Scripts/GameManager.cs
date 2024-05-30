using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas healthBarCanvas;
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private GameObject player;
    
    [SerializeField] private EnemySpawner enemySpawner;
    
    private int semester = 0;
    private int year = 0;

    private Label waveLabel;
    private Label timerLabel;

    private int waveDuration = 10;
    private int pauseDuration = 5;
    
    private int waveTime = 0;
    private int pauseTime = 0;
    
    [SerializeField] private UIDocument uiDocument;

    [SerializeField] private EnemySpawningChances spawningChances;
    void Start()
    {
        waveLabel = uiDocument.rootVisualElement.Q<Label>("WaveLabel");
        timerLabel = uiDocument.rootVisualElement.Q<Label>("TimerLabel");

        year = 1;
        
        SetVariables();
        UpdateWaveInfo();
        StartCoroutine(GameLogic());
    }

    void SetVariables()
    {
        EnemyController.SetHealthBarCanvas(healthBarCanvas);
        EnemyController.SetHealthBarPrefab(healthBarPrefab);
        EnemyController.SetPlayer(player);
        EnemySpawner.SetSpawningChances(spawningChances);
    }

    void UpdateWaveInfo()
    {
        semester++;

        if (semester > 2)
        {
            semester = 1;
            year++;
        }
        
        waveLabel.text = year + "º Ano / " + semester + "º Semestre";
        
        waveTime = waveDuration;

        waveDuration += 10;
    }
    
    private IEnumerator GameLogic()
    {
        enemySpawner.StartSpawning(year);
        
        //Timer da ronda
        while (waveTime > 0)
        {
            timerLabel.text = "Tempo restante: " + waveTime + "s";
            
            yield return new WaitForSeconds(1);
            
            waveTime--;
        }

        enemySpawner.StopSpawning();
        enemySpawner.KillEnemies();
        pauseTime = pauseDuration;
        UpdateWaveInfo();
        
        //Timer da pausa entre rondas
        while (pauseTime > 0)
        {
            timerLabel.text = "Próxima ronda em: " + pauseTime + "s";
            
            yield return new WaitForSeconds(1);

            pauseTime--;
        }

        StartCoroutine(GameLogic());
    }
}
