using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "HeroPool", menuName = "Character/HeroPool")]
public class HeroPoolSO : ScriptableObject
{
    public List<HeroEntry> heroList = new();
}
[System.Serializable]
public class HeroEntry
{
    public Hero hero;  // 键
    public int count;  // 值
}