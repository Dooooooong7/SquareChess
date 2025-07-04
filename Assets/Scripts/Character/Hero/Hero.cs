using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Hero : Character
{
    public HeroType heroType;
    public Enemy nowEnemy;
    public bool canAttack = false;
    public Animator anim;
    public List<Enemy> enemiesInRange = new List<Enemy>();
    public List<SpriteRenderer> attackRange;
    public TextMeshPro textMeshPro;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        attackRange = GetComponentsInChildren<SpriteRenderer>(true)
            .Where(sr => sr.gameObject != this.gameObject)
            .ToList();
        textMeshPro = GetComponentInChildren<TextMeshPro>();
        textMeshPro.text = attack.ToString();
    }

    public void RefreshText()
    {
        textMeshPro.text = attack.ToString();
    }
    
    public virtual Enemy SearchEnemy()
    {
        return null;
    }
    
    public virtual bool Attack()
    {
        return true;
    }

    public virtual void CheckEnemyInRange()
    {
        
    }

    private void OnMouseDown()
    {
        foreach (var range in attackRange)
        {
            range.enabled = !range.enabled;
        }
    }
}