using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Hero
{
    public List<Enemy> enemiesInRange = new List<Enemy>();

    private void Update()
    {
        if (canAttack)
        {
            nowEnemy = SearchEnemy();
            if (nowEnemy)
            {
                Attack();
            }

            canAttack = false;
        }
    }

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
        // 实现攻击逻辑
        if (nowEnemy != null)
        {
            Debug.Log("attack");
            nowEnemy.TakeDamage(attack);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Add(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Remove(enemy);
            }
        }
    }

    // private void OnCollisionStay2D(Collision2D other)
    // {
    //     Debug.Log(22222);
    // }
}
