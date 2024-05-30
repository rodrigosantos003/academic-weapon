using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField, Tooltip("Valor em percentagem")] private float defense;
    
    public float Speed => speed;
    public float Defense => defense;
}
