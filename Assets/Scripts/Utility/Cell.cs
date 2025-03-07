using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isEmpty = true;
    
    /// <summary>
    /// 点击棋盘格触发器时，调用HandManager的OnCellClick函数，并把当前的格子作为参数传入
    /// </summary>
    private void OnMouseDown()
    {
        // Debug.Log("2222");
        if (isEmpty)
        {
            // Debug.Log("1111");
            if (HandManager.Instance.OnCellClick(this))
            {
                isEmpty = false;
            }
        }
    }
}
