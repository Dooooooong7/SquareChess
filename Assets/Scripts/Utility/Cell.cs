using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isEmpty = true;
    public Hero heroAtCell;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 点击棋盘格触发器时，调用HandManager的OnCellClick函数，并把当前的格子作为参数传入
    /// </summary>
    private void OnMouseDown()
    {
        if (isEmpty)
        {
            if (HandManager.Instance.PlacePrefabInCell(this))
            {
                isEmpty = false;
            }
        }
    }
}
