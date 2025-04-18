using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Hero
{
    
    public override Enemy SearchEnemy()
    {
        if (enemiesInRange.Count > 0)
        {
            // 随机选择一个敌人
            int randomIndex = UnityEngine.Random.Range(0, enemiesInRange.Count);
            return enemiesInRange[randomIndex];
        }
        return null;
    }

    public override void Attack()
    {
        nowEnemy = SearchEnemy();
        // 实现攻击逻辑
        if (nowEnemy != null)
        {
            Debug.Log("attack");
            nowEnemy.TakeDamage(attack);
            buffHandler.OnAttack(nowEnemy);
        }
    }

    private void OnTrigger2DEnter(Collider other)
    {
        Debug.Log(2);
    }
}
