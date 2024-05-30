using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileStats", menuName = "ScriptableObjects/ProjectileStats")]
public class ProjectileStats : ScriptableObject
{
    [SerializeField] private float speed;
    
    public float Speed => speed;
}
