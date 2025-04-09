using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPool", menuName = "Character/EnemyPoolSO")]
public class EnemyPoolSO : ScriptableObject
{
    public List<Enemy> enemyList = new();
}
