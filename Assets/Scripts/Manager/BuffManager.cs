using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : Singleton<BuffManager>
{
    public BuffPoolSO buffPoolSO; // Buff池
    public BuffPoolSO availableBuffPoolSO; // 可用的 Buff
    public Transform generateBuffCardParent; // Buff卡片生成位置
    public BuffCard buffCardPrefab; // Buff卡片预制体
    
    //开始新游戏时初始化
    public void Initialize()
    {
        foreach (var characterBuff in buffPoolSO.buffPool)
        {
            availableBuffPoolSO.buffPool.Add(new BuffOfCharacter
            {
                character = characterBuff.character,
                buffs = new List<Buff>(characterBuff.buffs)
            });
        }
    }

    public void GenerateBuffCard()
    {
        for (int i = 0; i < 3; i++)
        {
            var buff = GenerateRandomBuff();
            if (buff == null)
            {
                Debug.LogError("Buff is null!");
                return;
            }
            var newBuffCard =  Instantiate(buffCardPrefab, generateBuffCardParent);
            newBuffCard.Initialize(buff);
        }
    }
    
    public Buff GenerateRandomBuff()
    {
        // 随机选择一个角色
        int randomIndex = Random.Range(0, availableBuffPoolSO.buffPool.Count);
        BuffOfCharacter selectedCharacterBuff = availableBuffPoolSO.buffPool[randomIndex];

        // 随机选择一个 Buff
        int buffIndex = Random.Range(0, selectedCharacterBuff.buffs.Count);
        Buff selectedBuff = selectedCharacterBuff.buffs[buffIndex];

        // 从可用 Buff 列表中移除已选 Buff
        selectedCharacterBuff.buffs.RemoveAt(buffIndex);
        
        Debug.Log("生成 Buff: " + selectedBuff.buffName);
        
        return selectedBuff;
    }
    
}
