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
        
        int randomIndex = Random.Range(0, enemyPoolSO.enemyList.Count);
        Enemy enemy = enemyPoolSO.enemyList[randomIndex];
        return enemy;
    }

    
    private Enemy GetEnemyPrefab(EnemyType enemyType)
    {
        foreach (var enemyPrefab in enemyPoolSO.enemyList)
        {
            if (enemyType == enemyPrefab.enemyType)
            {
                return enemyPrefab;
            }
        }

        return null;
    }
    
}
