using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : Character
{
    public EnemyType enemyType;
    public Animator anim;
    public TextMeshPro textMeshPro;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        textMeshPro = GetComponentInChildren<TextMeshPro>();
        textMeshPro.text = health.ToString();
    }
    
}
