using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileStats", menuName = "ScriptableObjects/ProjectileStats")]
public class ProjectileStats : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float lifeTime;
    [SerializeField] private bool hasLifeTime;
    
    public float Speed => speed;
    public float Damage => damage;
    public float LifeTime => lifeTime;
    public bool HasLifeTime => hasLifeTime;
}
