using System.Collections.Generic;
using UnityEngine;


public class HandManager : Singleton<HandManager>
{
    public HeroPoolSO heroPoolSO;
    public List<Hero> availableHeroes = new();//可选英雄列表
    private List<Hero> heroesInBattle = new();//或可删除
    public HeroType heroType { get; set; }
    public Hero nowHero;
    public Cards nowCard;
    public bool hasHero = false;
    
    public List<Cards> cardsPrefabList;
    public Transform cardBar;
    
    public void InitializeList()
    {
        availableHeroes = new List<Hero>(heroPoolSO.heroList);
    }
    
    public void CreateHeroesThisRound(int num)
    {
        for (int i = 0; i < num; i++)
        {
          if (availableHeroes.Count > 0)
          { 
              var hero = GetRandomHero();
              var cardPrefab = GetHeroCardPrefab(hero.heroType);
              Instantiate(cardPrefab, cardBar);
              heroesInBattle.Add(hero);
          }
        }  
    }
    
    // 获取随机英雄
    private Hero GetRandomHero()
    {
        int randIndex = Random.Range(0, availableHeroes.Count);
        var hero = availableHeroes[randIndex];
        availableHeroes.RemoveAt(randIndex);
        return hero;
    }
    
    //获取手牌栏图标预制体
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
        if (hasHero)//当前已经选中了英雄
        {
            Hero heroPrefab = GetHeroPrefab(heroType);//获取预制体

            if (heroPrefab == null)//预制体为空时
            {
                Debug.Log("hero is null");
                return false;
            }
            else//成功找到对应预制体
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
            if (heroType == heroPrefab.heroType)
            {
                return heroPrefab;
            }
        }

        return null;
    }

    /// <summary>
    /// 将英雄添加到战斗列表中
    /// </summary>
    /// <param name="hero"></param>
    public void RegisterHero(Hero hero)
    {
        heroesInBattle.Add(hero);
    }

    /// <summary>
    /// 开启新回合，将所有战斗列表中的英雄设为可攻击状态
    /// </summary>
    public void StartNewTurn()
    {
        foreach (var hero in heroesInBattle)
        {
            hero.canAttack = true;
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