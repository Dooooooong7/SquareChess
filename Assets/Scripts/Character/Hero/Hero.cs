using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    public HeroType heroType;
    public Enemy nowEnemy;
    public bool canAttack = false;
    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public virtual Enemy SearchEnemy()
    {
        return null;
    }

    public virtual void Attack()
    {
        
    }
    
}
