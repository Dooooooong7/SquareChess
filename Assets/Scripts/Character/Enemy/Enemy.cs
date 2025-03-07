using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class Enemy : Character
{
    public EnemyType enemyType;
    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }
}
