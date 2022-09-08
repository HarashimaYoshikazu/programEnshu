using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Reverce;

public class ReverceManager : MonoBehaviour
{
    [SerializeField]
    int _colum = 8;
    [SerializeField]
    int _row = 8;

    Cell[,] _cells;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _cells = new Cell[_colum, _row];
        CreateField();
    }
    void CreateField()
    {
        Cell cellPrefab = Resources.Load<Cell>("Reverce/Cell");
        for (int c = 0;c<_colum;c++)
        {
            for (int r = 0;r<_row;r++)
            {
                var cell = Instantiate(cellPrefab,this.transform);
                _cells[r, c] = cell;
            }
        }
    }
}
