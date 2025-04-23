using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "WorrierAtEdgeBuff", menuName = "Buff/WorrierAtEdgeBuff")]
public class WorrierAtEdgeBuff : Buff
{
    [Header("角色位置参数")]
    public int row;
    public int column;
    public Hero hero;
    public int attackIncrease = 2;
    public override void Initialize(BuffHandler buffHandler)
    {
        base.Initialize(buffHandler);
        TriggerType = BuffTriggerType.OnPlace;
        mutilAddType = BuffMutilAddType.unique;
        isPermanent = true;
        isActive = true;
        id = 2;
    }

    public void SetPosition(Hero targetHero)
    {
        hero = targetHero;
        row = hero.row;
        column = hero.column;
    }

    public override void InitializeBeforeTrigger(object value)
    {
        if (value is Hero targetHero)
        {
            SetPosition(targetHero);
        }
    }

    public override void OnTriggerEffect()
    {
        if (row == 1 || row == 5 || column == 1 || column == 5)
        {
            hero.attack += attackIncrease;
            hero.RefreshText();
        }
    }
}
