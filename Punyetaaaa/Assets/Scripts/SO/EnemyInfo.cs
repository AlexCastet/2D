using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SO", menuName = "SO/EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    public float Damage;
    public float Speed;
    public bool isRanged;
    public float Impuls;
    public Color Color;
    public int Score;
    public Animator Animator;
}
