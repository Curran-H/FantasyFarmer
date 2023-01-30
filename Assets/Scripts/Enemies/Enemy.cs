using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Enemies/New Enemy", fileName="New Enemy")]
public class Enemy : ScriptableObject {

    public string EnemyName;
    public int EnemyHealth;
    /// <summary> How fast the enemy moves </summary>
    public float EnemySpeed;
    /// <summary> Time between enemy attacks </summary>
    public float EnemyAttackTimer;
    /// <summary> Range of enemy attacks </summary>
    public float EnemyAttackRange;
    public int EnemyAttackDamage;
}
