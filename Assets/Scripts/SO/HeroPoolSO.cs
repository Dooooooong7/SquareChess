using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "HeroPool", menuName = "Character/HeroPool")]
public class HeroPoolSO : ScriptableObject
{
    public List<Hero> heroList = new();
}
