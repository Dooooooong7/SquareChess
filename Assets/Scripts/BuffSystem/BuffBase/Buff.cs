using System;
using UnityEngine;

public abstract class Buff : ScriptableObject,IComparable<Buff>
{
    public int id;
    public BuffTriggerType TriggerType;  // Buff触发类型
    public bool isActive; // Buff是否处于激活状态
    public BuffHandler target;// Buff目标
    public GameObject caster; // Buff施加者
    public BuffMutilAddType mutilAddType; // Buff重复添加方式
    
    public bool isPermanent; // Buff是否永久存在
    public int totalTime; // Buff持续时间
    public int restTime; // Buff剩余时间
    public int layer; // Buff层数
    public int triggerInterval; // Buff触发间隔

    public virtual void ResetTimer()
    {
        restTime = totalTime;
    }
    
    public virtual void AddLayer(int layer)
    {
        this.layer += layer;
    }
    
    public virtual void Initialize(BuffHandler buffHandler)
    {
        // 初始化时的一些操作
    }
    
    public virtual void SetTarget(BuffHandler target)
    {
        this.target = target;
    }
    
    // 触发效果的具体实现
    public virtual void OnTriggerEffect()
    {
        
    }
    
    public virtual void DecreaseTime()
    {
        if(isPermanent) return;
        if (restTime > 0)
        {
            restTime--;  // 每回合减少 Buff 的剩余时间
            if (restTime <= 0)
            {
                Deactivate();  // 如果剩余时间为 0，则激活移除逻辑
            }
        }
    }
    
    // Buff 结束时的处理逻辑
    public virtual void Deactivate()
    {
        // Buff 移除的逻辑，如销毁或者禁用 Buff
        Debug.Log($"{this.name} Buff has expired.");
    }

    public int CompareTo(Buff other)
    {
        return id.CompareTo(other.id);
    }
    
    public override bool Equals(object other)
    {
        if (!(other is Buff)) return false;
        return id == ((Buff)other).id;
    }

}