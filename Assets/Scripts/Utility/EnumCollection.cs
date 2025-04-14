using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackRange
{
    OneTile,
    TwoTiles,
    Line,
    AllEnemies
}

public enum HeroType
{
    Warrior,
    Archer,
    Mage,
    Wizard,
    EvilWizard
        
}

public enum EnemyType
{
    Skeleton
    // Goblin
}
    
public enum RoomState
{
    Locked,
    Visited,
    Attainable
} 
    
public enum RoomType
{
    Normal,
    Shop,
    Boss 
}

public enum BuffTriggerType
{
    OnPlace,      // 放置角色时触发
    OnAttack,     // 攻击时触发
    OnTurnEnd,    // 回合结束时触发
    OnGetBuff, // 获得Buff时触发
}

public enum BuffMutilAddType
{
    resetTime,              // 重置时间
    multipleLayer,          // 多层叠加
    multipleLayerAndResetTime, // 多层叠加并重置时间
    multipleCount,           // 叠加多个相同 Buff
    unique,               // 唯一 Buff
}