using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    public static CellManager Instance { get; private set; }
    public List<Cell> cells;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeCells();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeCells()
    {
        cells = new List<Cell>(FindObjectsOfType<Cell>());
    }
}
