using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

[CreateAssetMenu(fileName = "BleedBuff", menuName = "Buff/BleedBuff")]
public class BleedBuff : Buff
{
    [SerializeField]private int bleedDamage = 2;

    public override void Initialize(BuffHandler buffHandler)
    {
        base.Initialize(buffHandler);
        TriggerType = BuffTriggerType.OnTurnEnd;
        isActive = true;
        target = buffHandler;
        mutilAddType = BuffMutilAddType.unique;
        isPermanent = false;
        totalTime = 2;
        id = 2;
    }

    public override void OnTriggerEffect()
    {
        target.GetComponent<Enemy>().TakeDamage(bleedDamage);
        Debug.Log("BleedBuff triggered");
    }
}
