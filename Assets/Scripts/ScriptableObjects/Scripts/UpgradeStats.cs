using System;
using UnityEngine;

public enum UpgradeType
{
    Health,
    Damage,
    ShootingSpeed,
    Speed
}

[CreateAssetMenu(fileName = "UpgradeCard", menuName = "ScriptableObjects/UpgradeCard")]
public class UpgradeCard : ScriptableObject
{
    [SerializeField] private string upgradeName;
    [SerializeField] private string upgradeDescription;
    [SerializeField] private Texture2D upgradeImage;
    [SerializeField] private float upgradeChance;
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField, Tooltip("Valor em percentagem")] private float upgradeValue;
    
    public string UpgradeName => upgradeName;
    
    public string UpgradeDescription => upgradeDescription;
    
    public Texture2D UpgradeImage => upgradeImage;
    
    public float UpgradeChance => upgradeChance;
    
    public UpgradeType UpgradeType => upgradeType;
    
    public float UpgradeValue => upgradeValue;
}
