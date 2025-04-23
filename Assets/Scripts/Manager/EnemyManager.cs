using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyManager : Singleton<EnemyManager>
{
    [FormerlySerializedAs("enemyPrefabList")] public EnemyPoolSO enemyPoolSO;
    public Enemy nowEnemy;
    
    public void ClearChessBoard()
    {
        foreach (var cell in CellManager.Instance.cellList)
        {
            cell.isEmpty = true;

            // 获取 Cell 中所有的子物体（Hero 和 Enemy）
            var children = new List<GameObject>();
            foreach (Transform child in cell.transform)
            {
                children.Add(child.gameObject);
            }

            // 销毁所有子物体
            foreach (var child in children)
            {
                Destroy(child);
            }
        }

        CellManager.Instance.restCell = 25;
    }
    
    public void ProduceEnemy(int cardNum)
    {
        for (int i = 0; i < cardNum; i++)
        {
            if (CellManager.Instance.restCell > 0)
            {
                var randIndex = GetRandomCell();//获取敌人生成的方格
                CellManager.Instance.restCell--;
                CellManager.Instance.cellList[randIndex].GetComponent<Cell>().isEmpty = false;
                var enemy = GetRandomEnemy();
                Enemy enemyPrefab = GetEnemyPrefab(enemy.enemyType);
                if (enemyPrefab != null)
                {
                    // 设置敌人预制体的行和列
                    enemyPrefab.row = CellManager.Instance.cellList[randIndex].row;
                    enemyPrefab.column = CellManager.Instance.cellList[randIndex].column;
                    nowEnemy = Instantiate(enemyPrefab,CellManager.Instance.cellList[randIndex].transform); 
                    nowEnemy.transform.localPosition = new Vector3(0,-0.5f,0);
                }
                
            }
        }
    }
    
    public bool AreAllCellsEmpty()
    {
        foreach (var cell in CellManager.Instance.cellList)
        {
            if (!cell.isEmpty && cell.GetComponentInChildren<Enemy>() != null)
            {
                return false;
            }
        }
        return true;
    }

    private int GetRandomCell()
    {
        int randIndex = Random.Range(0, 24);
        while (!CellManager.Instance.cellList[randIndex].GetComponent<Cell>().isEmpty)
        {
            randIndex = Random.Range(0, 24);
        }
        return randIndex;
    }

    private Enemy GetRandomEnemy()
    {
        if (enemyPoolSO.enemyList == null || enemyPoolSO.enemyList.Count == 0)
        {
            // Debug.Log("敌人列表为空！");
            return null;
        }

        // 计算总权重（所有敌人数量的总和）
        int totalWeight = 0;
        foreach (var entry in enemyPoolSO.enemyList)
        {
            totalWeight += entry.count; // 权重为敌人数量
        }

        // 获取一个 0 到 totalWeight 之间的随机数
        int randomWeight = Random.Range(0, totalWeight);

        // 遍历所有敌人，根据数量（权重）选择一个敌人
        int cumulativeWeight = 0;
        foreach (var entry in enemyPoolSO.enemyList)
        {
            cumulativeWeight += entry.count;

            // 当累计权重大于随机数时，选中当前敌人
            if (randomWeight < cumulativeWeight)
            {
                // // 减少该敌人的数量
                // entry.count--;
                //
                // // 如果数量为0了，移除该敌人
                // if (entry.count == 0)
                // {
                //     enemyPoolSO.enemyList.Remove(entry);
                // }

                // 返回选中的敌人
                return entry.enemy;
            }
        }

        return null; // 如果没有选中敌人，返回空（这种情况不应该发生）
    }

    private Enemy GetEnemyPrefab(EnemyType enemyType)
    {
        foreach (var entry in enemyPoolSO.enemyList)
        {
            if (enemyType == entry.enemy.enemyType)
            {
                return entry.enemy;
            }
        }

        return null;
    }
    
}
