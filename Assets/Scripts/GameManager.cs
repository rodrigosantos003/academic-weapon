using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas healthBarCanvas;
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerStats playerStats;
    
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
    
    [SerializeField] private List<UpgradeCard> upgrades;
    [SerializeField] private VisualTreeAsset upgradeCardPrefab;
    [SerializeField] private UIDocument upgradeCardsRoot;
    private VisualElement upgradeCardsContainer;
    
    void Start()
    {
        waveLabel = uiDocument.rootVisualElement.Q<Label>("WaveLabel");
        timerLabel = uiDocument.rootVisualElement.Q<Label>("TimerLabel");
        
        year = 1;
        
        upgradeCardsRoot.gameObject.SetActive(true);
        upgradeCardsContainer = upgradeCardsRoot.rootVisualElement.Q<VisualElement>("UpgradeCardsContainer");
        
        upgradeCardsRoot.rootVisualElement.style.display = DisplayStyle.None;
        
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
        ProjectileController.SetDamage(playerStats.Attack);
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
        ShowUpgrades();
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

    private void ShowUpgrades()
    {
        Time.timeScale = 0;
        upgradeCardsRoot.rootVisualElement.style.display = DisplayStyle.Flex;
        
        upgradeCardsContainer.Clear();
        
        float totalChances = 0;
        foreach (var upgrade in upgrades)
        {
            totalChances += upgrade.UpgradeChance;
        }

        var upgradeCards = new UpgradeCard[3];
        var currentUpgrade = 0;
        
        while(currentUpgrade < 3)
        {
            var randomChance = Random.Range(0, totalChances);
            UpgradeCard upgrade = null;
            
            foreach (UpgradeCard randomUpgrade in upgrades)
            {
                randomChance -= randomUpgrade.UpgradeChance;
                if (randomChance <= 0)
                {
                    upgrade = randomUpgrade;
                    break;
                }
            }
            upgradeCards[currentUpgrade] = upgrade;
            currentUpgrade++;
        }
        
        foreach (var upgrade in upgradeCards)
        {
            var upgradeCard = upgradeCardPrefab.CloneTree();
            upgradeCard.Q<Label>("UpgradeName").text = upgrade.UpgradeName;
            upgradeCard.Q<Label>("UpgradeDescription").text = upgrade.UpgradeDescription;
            upgradeCard.Q<Label>("UpgradeImage").style.backgroundImage = upgrade.UpgradeImage;
            upgradeCard.RegisterCallback<ClickEvent>(UpgradeClicked(upgrade));
            upgradeCardsContainer.Add(upgradeCard);
        }
    }
    
    private EventCallback<ClickEvent> UpgradeClicked(UpgradeCard upgrade)
    {
        return evt =>
        {
            switch (upgrade.UpgradeType)
            {
                case UpgradeType.Damage:
                    float newDamage = playerStats.Attack * (1 + upgrade.UpgradeValue / 100);
                    playerStats.Attack = (int)newDamage;
                    ProjectileController.SetDamage(playerStats.Attack);
                    break;
                
                case UpgradeType.Defense:
                    float newDefense = playerStats.Defense * (1 + upgrade.UpgradeValue / 100);
                    playerStats.Defense = (int)newDefense;
                    break;
                
                case UpgradeType.Health:
                    float newHealth = playerStats.MaxHealth * (1 + upgrade.UpgradeValue / 100);
                    playerStats.MaxHealth = (int)newHealth;
                    player.GetComponent<PlayerController>().UpdateHealthbarScale();
                    break;
                
                case UpgradeType.ShootingSpeed:
                    float newShootingSpeed = playerStats.ShootingSpeed / (1 + upgrade.UpgradeValue / 100);
                    playerStats.ShootingSpeed = newShootingSpeed;
                    break;
                
                case UpgradeType.Speed:
                    float newSpeed = playerStats.Speed * (1 + upgrade.UpgradeValue / 100);
                    playerStats.Speed = (int)newSpeed;
                    break;
            }
            
            Time.timeScale = 1;
            upgradeCardsRoot.rootVisualElement.style.display = DisplayStyle.None;
        };
    }
}
