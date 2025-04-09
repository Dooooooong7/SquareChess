using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelProgressData", menuName = "Game/Level Progress Data")]
public class LevelProgressDataSO : ScriptableObject
{
    [Header("关卡信息")]
    public int currentLevel;
    public int totalLevels;
    
    [Header("难度系数")]
    public float difficultyMultiplier = 1.0f;
}
