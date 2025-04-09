using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : Singleton<CellManager>
{
    public List<Cell> cellList = new();
    public int restCell;

    private void OnEnable()
    {
        restCell = cellList.Count;
    }
}
