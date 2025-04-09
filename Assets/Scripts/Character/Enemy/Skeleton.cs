using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        textMeshPro.text = health.ToString();
        if (health > 0)
        {
            anim.SetTrigger("IsHitted");
            
        }
        else
        {
            anim.SetTrigger("IsDied");
        }
    }
}
