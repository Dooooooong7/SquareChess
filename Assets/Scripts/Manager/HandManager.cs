using System.Collections.Generic;
using UnityEngine;

public class HandManager : Singleton<HandManager>
{
    public HeroPoolSO heroPoolSO;
    public List<HeroEntry> availableHeroes = new List<HeroEntry>(); // 可选英雄列表
    private List<HeroEntry> heroesInBattle = new List<HeroEntry>(); // 战斗中的英雄
    public HeroType heroType { get; set; }
    public Hero nowHero;
    public Cards nowCard;
    public bool hasHero = false;
    
    public List<Cards> cardsPrefabList;
    public Transform cardBar;

    // 初始化英雄列表
    public void InitializeList()
    {
        availableHeroes.Clear();
        foreach (var hero in heroPoolSO.heroList)
        {
            availableHeroes.Add(new HeroEntry { hero = hero.hero, count = hero.count });
        }
    }
    
    // 创建本回合的英雄
    public void CreateHeroesThisRound(int num)
    {
        for (int i = 0; i < num; i++)
        {
            if (availableHeroes.Count > 0)
            {
                var hero = GetRandomHero();
                var cardPrefab = GetHeroCardPrefab(hero.heroType);
                Instantiate(cardPrefab, cardBar);
                RegisterHero(hero);
            }
        }
    }
    
    // 获取随机英雄
    private Hero GetRandomHero()
    {
        // 计算总权重
        int totalWeight = 0;
        foreach (var entry in availableHeroes)
        {
            totalWeight += entry.count; 
        }

        // 获取一个 0 到 totalWeight 之间的随机数
        int randomWeight = Random.Range(0, totalWeight);

        // 遍历所有英雄，根据数量（权重）选择一个英雄
        int cumulativeWeight = 0;
        foreach (var entry in availableHeroes)
        {
            cumulativeWeight += entry.count;

            // 当累计权重大于随机数时，选中当前英雄
            if (randomWeight < cumulativeWeight)
            {
                // 减少该英雄数量
                entry.count--;
            
                // 如果数量为0了，移除该英雄
                if (entry.count == 0)
                {
                    availableHeroes.Remove(entry);
                }

                // 返回选中的英雄
                return entry.hero;
            }
        }
        return null; 
    }

    // 获取手牌栏图标预制体
    private Cards GetHeroCardPrefab(HeroType heroType)
    {
        foreach (var cardPrefab in cardsPrefabList)
        {
            if (heroType == cardPrefab.heroType)
            {
                return cardPrefab;
            }
        }
        return null;
    }

    /// <summary>
    /// 将prefab放置在cell对应位置
    /// </summary>
    /// <param name="cell">一个棋盘格</param>
    /// <returns></returns>
    public bool PlacePrefabInCell(Cell cell)
    {
        if (hasHero) // 当前已经选中了英雄
        {
            Hero heroPrefab = GetHeroPrefab(heroType); // 获取预制体

            if (heroPrefab == null) // 预制体为空时
            {
                Debug.Log("hero is null");
                return false;
            }
            else // 成功找到对应预制体
            {
                nowHero = Instantiate(heroPrefab, cell.transform);
                RegisterHero(nowHero);
                Destroy(nowCard.gameObject);
                
                hasHero = false;
                cell.isEmpty = false;
                CellManager.Instance.restCell--;
                cell.heroAtCell = nowHero;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 获取当前英雄类型对应的prefab
    /// </summary>
    /// <returns></returns>
    public Hero GetHeroPrefab(HeroType heroType)
    {
        foreach (var heroPrefab in heroPoolSO.heroList)
        {
            if (heroType == heroPrefab.hero.heroType)
            {
                return heroPrefab.hero;
            }
        }

        return null;
    }

    /// <summary>
    /// 将英雄添加到战斗列表中，如果该英雄已经存在，则增加其计数
    /// </summary>
    /// <param name="hero">要添加的英雄</param>
    public void RegisterHero(Hero hero)
    {
        // 查找是否已经有该英雄
        var existingHero = heroesInBattle.Find(entry => entry.hero == hero);
        if (existingHero != null)  // 如果已经存在，增加该英雄的计数
        {
            existingHero.count++;
        }
        else
        {
            // 如果不存在，创建一个新的 HeroEntry，并添加到战斗列表
            heroesInBattle.Add(new HeroEntry { hero = hero, count = 1 });
        }
    }

    /// <summary>
    /// 开启新回合，将所有战斗列表中的英雄设为可攻击状态
    /// </summary>
    public void StartNewTurn()
    {
        foreach (var entry in heroesInBattle)
        {
            entry.hero.canAttack = true;
        }
    }

    public void EndTurn()
    {
        // 清理或重置需要在新回合开始前的操作
    }

    /// <summary>
    /// 结束本场游戏时清除战斗列表
    /// </summary>
    public void ClearHeroes()
    {
        heroesInBattle.Clear();
    }
}
