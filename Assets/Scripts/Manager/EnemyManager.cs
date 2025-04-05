using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<Enemy> enemyPrefabList;
    public EnemyType enemyType { get; set; }
    public Enemy nowEnemy;
    public List<Cell> cellList;
    // public int cardNum { get; set; } = 4;
    public int restCell = 25;
    public int enemyTypeNum;
    
    public void ClearChessBoard()
    {
        foreach (var cell in cellList)
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

        restCell = 25;
    }
    
    public void ProduceEnemy(int cardNum)
    {
        for (int i = 0; i < cardNum; i++)
        {
            if (restCell > 0)
            {
                int randIndex = Random.Range(0, 24);
                while (!cellList[randIndex].GetComponent<Cell>().isEmpty)
                {
                    randIndex = Random.Range(0, 24);
                }
                restCell--;
                cellList[randIndex].GetComponent<Cell>().isEmpty = false;
                var enemyIndex = Random.Range(0, enemyTypeNum);
                enemyType = (EnemyType)enemyIndex;
                // Debug.Log(enemyIndex);
                Enemy enemyPrefab = GetEnemyPrefab();
                if (enemyPrefab != null)
                {
                    nowEnemy = Instantiate(enemyPrefab,cellList[randIndex].transform); 
                    nowEnemy.transform.localPosition = new Vector3(0,-0.5f,0);
                }
                
            }
        }
    }

    public Enemy GetEnemyPrefab()
    {
        foreach (var enemyPrefab in enemyPrefabList)
        {
            if (enemyType == enemyPrefab.enemyType)
            {
                return enemyPrefab;
            }
        }

        return null;
    }
    
    public bool AreAllCellsEmpty()
    {
        foreach (var cell in cellList)
        {
            if (!cell.isEmpty && cell.GetComponentInChildren<Enemy>() != null)
            {
                return false;
            }
        }
        return true;
    }
    
}
