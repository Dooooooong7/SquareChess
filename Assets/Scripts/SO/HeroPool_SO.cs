using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroPool", menuName = "ScriptableObjects/HeroPool")]
public class HeroPool_SO : ScriptableObject
{
    public List<Hero> heroPoolList;
}
