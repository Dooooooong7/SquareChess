using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "BuffPoolSO", menuName = "Buff/BuffPoolSO", order = 1)]
public class BuffPoolSO : ScriptableObject
{
    public List<BuffOfCharacter> buffPool = new();  

    // 获取对应角色的 Buff 列表
    public List<Buff> GetBuffsForCharacter(Character character)
    {
        foreach (var characterBuff in buffPool)
        {
            if (characterBuff.character == character)
            {
                return characterBuff.buffs;
            }
        }
        return null;  
    }
}

[System.Serializable]
public class BuffOfCharacter
{
    public Character character;  // 角色
    public List<Buff> buffs;     // 该角色可有的 Buff 列表
}