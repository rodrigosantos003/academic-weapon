using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private int defaultSpeed;
    [SerializeField] private int speed;
    
    [SerializeField] private int defaultMaxHealth;
    [SerializeField] private int maxHealth;
    
    [SerializeField] private GameObject projectile;
    
    [SerializeField] private int defaultAttack;
    [SerializeField] private int attack;
    
    [SerializeField] private float defaultShootingSpeed;
    [SerializeField] private float shootingSpeed;

    void OnEnable()
    {
        speed = defaultSpeed;
        maxHealth = defaultMaxHealth;
        attack = defaultAttack;
        shootingSpeed = defaultShootingSpeed;
    }
    
    public int Speed
    {
        get => speed;
        set => speed = value;
    }

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public GameObject Projectile
    {
        get => projectile;
        set => projectile = value;
    }

    public int Attack
    {
        get => attack;
        set => attack = value;
    }

    public float ShootingSpeed
    {
        get => shootingSpeed;
        set => shootingSpeed = value;
    }
}
