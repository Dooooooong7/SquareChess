using System.Collections.Generic;
using UnityEngine;


public class HandManager : Singleton<HandManager>
{
    
    public List<Hero> heroPrefabList;
    public HeroType heroType { get; set; }
    public Hero nowHero;
    public Cards nowCard;
    public bool hasHero = false;
    private List<Hero> heroesInBattle = new List<Hero>();//战斗列表

    public HeroPool_SO heroPool;
    public List<Cards> cardsPrefabList;
    private int[] _isUsed = new int[100];
    private int _restHeroes;
    public Transform cardBar;

    protected override void Awake()
    {
        base.Awake();
        heroPool.heroPoolList.Clear();
        CreateInitialHeroes(7);
    }

    public void CreateInitialHeroes(int num)
    {
        for (int i = 0; i < num; i++)
        {
            var index = Random.Range(0, heroPrefabList.Count);
            Debug.Log(index);
            heroPool.heroPoolList.Add(heroPrefabList[index]);
        }
    }

    public void ClearHeroUsed()
    {
        Debug.Log(heroPool.heroPoolList.Count);
        for (int i = 0; i < heroPool.heroPoolList.Count; i++)
        {
            _isUsed[i] = 0;
        }
        _restHeroes = heroPool.heroPoolList.Count;
    }
    
    
    public void CreateHeroesThisRound(int num)
    {
        for (int i = 0; i < num; i++)
        {
          if (_restHeroes > 0)
          { 
              var index = Random.Range(0, heroPool.heroPoolList.Count);
              while (_isUsed[index] > 0)
              {
                  index = Random.Range(0, heroPool.heroPoolList.Count);
              }
              _isUsed[index] = 1;
              _restHeroes--;
              for (int j = 0; j < cardsPrefabList.Count; j++)
              {
                if (heroPool.heroPoolList[index].heroType == cardsPrefabList[j].heroType)
                {
                    Instantiate(cardsPrefabList[j], cardBar);
                    break;
                }
              }    
          }
        }  
    }
    
    /// <summary>
    /// 将prefab放置在cell对应位置
    /// </summary>
    /// <param name="cell">一个棋盘格</param>
    /// <returns></returns>
    public bool OnCellClick(Cell cell)
    {
        if (hasHero)//当前已经选中了英雄
        {
            Hero heroPrefab = GetHeroPrefab();//获取预制体

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
                EnemyManager.Instance.restCell--;
                return true;
            }
        }

        return false;
    }

    
    /// <summary>
    /// 获取当前英雄类型对应的prefab
    /// </summary>
    /// <returns></returns>
    public Hero GetHeroPrefab()
    {
        foreach (var heroPrefab in heroPrefabList)
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