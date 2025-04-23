using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public string characterName;
    public int health;
    public int attack;
    public int defense;
    public BuffHandler buffHandler;
    public int row;
    public int column;

    protected virtual void Awake()
    {
        buffHandler = GetComponent<BuffHandler>();
    }

    public virtual void TakeDamage(int damage)
    {
        int damageTaken = Mathf.Max(damage - defense, 0);
        health = Mathf.Max( health - damageTaken, 0);
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual bool Attack(Character target)
    {
        target.TakeDamage(attack);
        return true;
    }

    protected virtual void Die()
    {
        Debug.Log(characterName + " has died.");
        BattleManager.Instance.enemiesDefeated++;
    }
}