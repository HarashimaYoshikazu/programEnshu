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

    private void Init()
    {
        _cells = new Cell[_colum, _row];
    }
    void CreateField()
    {
        for (int c = 0;c<_colum;c++)
        {
            for (int r = 0;r<_row;r++)
            {
                //_cells[r,c] = 
            }
        }
    }
}
