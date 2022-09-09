using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Reversi;
using UnityEngine.EventSystems;

public class ReversiManager : MonoBehaviour
{
    [SerializeField]
    int _colum = 8;
    [SerializeField]
    int _row = 8;

    [SerializeField]
    CellState _firstTurnPlayer;

    Cell[,] _cells;

    CellState _curentTurnPlayer = CellState.Black;
    public CellState GetCurrentTurnPlayer => _curentTurnPlayer;

    private void Awake()
    {     
        Init();
    }

    private void Init()
    {
        _curentTurnPlayer = _firstTurnPlayer;
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
    public void NextTurn()
    {
        switch (_curentTurnPlayer)
        {
            case CellState.Black:
                _curentTurnPlayer = CellState.White;
                break;
            case CellState.White:
                _curentTurnPlayer = CellState.Black;
                break;
            case CellState.None:
                Debug.LogError("•s³‚È’l‚Å‚·");
                break;
                
        }

    }
}
