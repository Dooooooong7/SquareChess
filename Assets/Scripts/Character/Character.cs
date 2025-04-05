using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public string characterName;
    public int health;
    public int attack;
    public int defense;

    public virtual void TakeDamage(int damage)
    {
        int damageTaken = Mathf.Max(damage - defense, 0);
        health -= damageTaken;
        if (health <= 0)
        {
            // Die();
        }
    }

    public virtual void Attack(Character target)
    {
        target.TakeDamage(attack);
    }

    protected virtual void Die()
    {
        Debug.Log(characterName + " has died.");
        Destroy(gameObject);
    }
}
