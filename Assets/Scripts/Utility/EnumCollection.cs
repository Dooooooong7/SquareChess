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