using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPool", menuName = "Character/EnemyPoolSO")]
public class EnemyPoolSO : ScriptableObject
{
    public List<EnemyEntry> enemyList = new();
}

[System.Serializable]
public class EnemyEntry
{
    public Enemy enemy;  // 键
    public int count;  // 值
}