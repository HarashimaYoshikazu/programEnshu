using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Minesweeper : MonoBehaviour
{
    [Header("ゲームの設定")]
    [SerializeField]
    int _rows = 10;
    [SerializeField]
    int _colums = 10;

    [SerializeField, Range(0, 99)]
    int _mineCount = 5;

    [Header("リソース")]
    [SerializeField]
    Cell _cellPrefab = null;

    [SerializeField]
    GridLayoutGroup _gridLayoutGroup = null;

    Cell[,] _cells = null;

    [SerializeField]
    GameObject _gameEndPanel = null;

    int _count = 0;
    public int Count
    {
        get { return _count; }
        set
        {
            if (value>=(_rows*_colums)-_mineCount)
            {
                OnGameEnd("クリア");
            }
            else
            {
                _count = value;
            }
        }
    }

    private void Awake()
    {
        if (!_cellPrefab)
        {
            _cellPrefab = Resources.Load<Cell>("cell");
        }
        if (!_gameEndPanel)
        {
            _gameEndPanel = Resources.Load<GameObject>("GameOverPanel");
        }
    }

    private void Start()
    {
        CreateCells();
        SetMine();

    }

    private void SetMine()
    {
        int counts = 0;
        while (counts < _mineCount)
        {
            int row = Random.Range(0, _rows);
            int col = Random.Range(0, _colums);
            if (_cells[row, col].CellTypeValue == CellType.Mine)
            {
                continue;
            }
            else
            {
                _cells[row, col].CellTypeValue = CellType.Mine;
                SetNumberAroundMine(row,col);
                counts++;
            }


        }
    }

    private void CreateCells()
    {
        _gridLayoutGroup.constraint
                   = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _colums;

        _cells = new Cell[_rows, _colums];

        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _colums; c++)
            {
                var cell = Instantiate(_cellPrefab, _gridLayoutGroup.transform);
                _cells[r, c] = cell;
            }
        }
    }

    private void SetNumberAroundMine(int row, int col)
    {
        if (row > 0)
        {
            _cells[row - 1, col].CellTypeValue++;//左真ん中
            if (col > 0)
            {
                _cells[row - 1, col - 1].CellTypeValue++;//左下
            }
            if (col < _colums-1)
            {
                _cells[row - 1, col + 1].CellTypeValue++;//左上
            }
        }
        if (row < _rows-1)
        {
            _cells[row + 1, col].CellTypeValue++; //右真ん中
            if (col > 0)
            {
                _cells[row + 1, col - 1].CellTypeValue++;//右下
            }
            if (col < _colums-1)
            {
                _cells[row + 1, col + 1].CellTypeValue++;//右上
            }
        }

        if (col>0)
        {
            _cells[row, col - 1].CellTypeValue++; //真ん中上
        }
        if (col<_colums-1)
        {
            _cells[row, col + 1].CellTypeValue++;　//真ん中下
        }
    }

    public void OnGameEnd(string gameEndText)
    {
        var go = Instantiate(_gameEndPanel,_gridLayoutGroup.transform.parent);
        go.GetComponentInChildren<Text>().text = gameEndText;
    }
}
