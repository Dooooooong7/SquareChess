using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    public RoomSaveData roomSaveData;
    public int currentRound;
    public int totalRounds;
    public List<int> enemiesPerRound;
    public List<int> heroesPerRound;
    public ObjectEventSO loadMapEventSO;
    public ObjectEventSO gameOverEventSO;
    public int enemiesDefeated;
    public int enemiesTotal;
    
    private int GetEnemyNum()
    {
        int total = 0;
        foreach (var num in enemiesPerRound)
        {
            total += num;
        }
        return total;
    }

    public void SetRoomData(RoomSaveData roomData)
    {
        roomSaveData = roomData;
        currentRound = 1;
        totalRounds = roomSaveData.roundNum;
        enemiesPerRound = roomSaveData.enemyPerRound;
        heroesPerRound = roomSaveData.heroPerRound;
        HandManager.Instance.InitializeList();
        enemiesTotal = GetEnemyNum();
        StartCoroutine(StartNewRound());
    }

    public void OnAttackClicked(object value)
    {
        StartCoroutine(StartNewRound());
    }
    
    public IEnumerator StartNewRound()
    {
        // 除第一回合外，开始下一回合前结算攻击
        if (currentRound > 1)
        {
            yield return StartCoroutine(ExecuteAttack());
            // 如果击败全部敌人
            if (enemiesDefeated >= enemiesTotal)
            {
                loadMapEventSO.RaiseEvent(null, this);
                yield break;
            }
        }
        if (currentRound <= totalRounds)
        {
            Debug.Log($"开始第 {currentRound} 回合");

            EnemyManager.Instance.ProduceEnemy(enemiesPerRound[currentRound - 1]);
            HandManager.Instance.CreateHeroesThisRound(heroesPerRound[currentRound - 1]);
            currentRound++;
        }
        else
        {
            Debug.Log("游戏失败");
            gameOverEventSO.RaiseEvent(null,this);
            Time.timeScale = 0;
        }
    }

    private IEnumerator ExecuteAttack()
    {
        // 遍历每个 cell，检查是否有英雄
        foreach (var cell in CellManager.Instance.cellList)  // 假设CellManager是管理所有cell的管理器
        {
            cell.spriteRenderer.enabled = true;
            // 如果 cell 中存在英雄，则执行攻击
            if (cell.heroAtCell != null)
            {
                // 执行攻击操作
                cell.heroAtCell.Attack();  // 假设 Hero 类有一个 Attack 方法
                Debug.Log($"英雄 {cell.heroAtCell.name} 攻击了敌人！");
                yield return new WaitForSeconds(2f);
            }
            else
            {
                yield return new WaitForSeconds(0.05f);
            }
            cell.spriteRenderer.enabled = false; 
        }
    }
    
}