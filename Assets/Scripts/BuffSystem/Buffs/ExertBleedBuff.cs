using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ExertBleedBuff", menuName = "Buff/ExertBleedBuff")]
public class ExertBleedBuff : Buff
{
    public override void Initialize(BuffHandler buffHandler)
    {
        base.Initialize(buffHandler);
        TriggerType = BuffTriggerType.OnAttack;
        mutilAddType = BuffMutilAddType.unique;
        isPermanent = true;
        isActive = true;
        id = 1;
    }
    
    public  void SetTarget(BuffHandler target)
    {
        this.target = target;
    }

    public override void InitializeBeforeTrigger(object value)
    {
        if (value is BuffHandler target)
        {
            SetTarget(target);
        }
    }

    public override void OnTriggerEffect()
    {
        target.AddBuff(ScriptableObject.CreateInstance<BleedBuff>());
        Debug.Log("ExertBleedBuff triggered");
    }
}
