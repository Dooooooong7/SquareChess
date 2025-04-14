using System;
using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    public List<Buff> buffs = new List<Buff>();

    // 添加Buff
    public void AddBuff(Buff buff)
    {
        buff.Initialize(this);  // 初始化Buff
        Buff previous = buffs.Find(p => p.Equals(buff));
        if (previous == null)
        {
            buffs.Add(buff);
            return;
        }
        Debug.Log("找到相同buff");
        switch (previous.mutilAddType)
        {
            case BuffMutilAddType.resetTime:
                previous.ResetTimer();
                break;
            case BuffMutilAddType.multipleLayer:
                previous.AddLayer(1);
                break;
            case BuffMutilAddType.multipleLayerAndResetTime:
                previous.ResetTimer();
                previous.AddLayer(1);
                break;
            case BuffMutilAddType.multipleCount:
                buffs.Add(buff);
                break;
            case BuffMutilAddType.unique:
                break;
            default:
                break;
        }
    }

    // 移除Buff
    public void RemoveBuff(Buff buff)
    {
        buffs.Remove(buff);
    }

    // 执行放置角色事件，触发相应Buff
    public void PlaceCharacter()
    {
        foreach (var buff in buffs)
        {
            if (buff.TriggerType == BuffTriggerType.OnPlace)
            {
                buff.OnTriggerEffect();
            }
        }
    }

    // 执行攻击事件，触发相应Buff
    public void OnAttack(Enemy enemy)
    {
        Debug.Log("开始计算buff");
        foreach (var buff in buffs)
        {
            if (buff.TriggerType == BuffTriggerType.OnAttack)
            {
                buff.SetTarget(enemy.GetComponent<BuffHandler>());
                buff.OnTriggerEffect();
            }
        }
    }

    // 执行回合结束事件，触发相应Buff
    public void EndTurn(object value = null)
    {
        foreach (var buff in buffs)
        {
            if (buff.TriggerType == BuffTriggerType.OnTurnEnd)
            {
                Debug.Log("EndTurn:" + buff.name);
                buff.OnTriggerEffect();
                buff.DecreaseTime();
            }
        }
    }
    
    
}