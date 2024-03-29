using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Minesweeper;

public class MinesweeperManager : MonoBehaviour
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

    bool isInit = false;
    public bool IsInit => isInit;

    int _count = 0;
    public int Count
    {
        get { return _count; }
        set
        {
            if (value >= (_rows * _colums) - _mineCount)
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
    }

    public void SetMine(int firstRow, int firstCol)
    {
        int counts = 0;
        while (counts < _mineCount)
        {
            int row = Random.Range(0, _rows);
            int col = Random.Range(0, _colums);
            if (row == firstRow && col == firstCol)
            {
                continue;
            }
            else if (_cells[row, col].CellTypeValue == CellType.Mine)
            {
                continue;
            }
            else
            {
                _cells[row, col].CellTypeValue = CellType.Mine;
                SetNumberAroundMine(row, col);
                counts++;
            }
        }
        isInit = true;
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
                cell.CellIndex = new Vector2Int(r, c);
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
            if (col < _colums - 1)
            {
                _cells[row - 1, col + 1].CellTypeValue++;//左上
            }
        }
        if (row < _rows - 1)
        {
            _cells[row + 1, col].CellTypeValue++; //右真ん中
            if (col > 0)
            {
                _cells[row + 1, col - 1].CellTypeValue++;//右下
            }
            if (col < _colums - 1)
            {
                _cells[row + 1, col + 1].CellTypeValue++;//右上
            }
        }

        if (col > 0)
        {
            _cells[row, col - 1].CellTypeValue++; //真ん中上
        }
        if (col < _colums - 1)
        {
            _cells[row, col + 1].CellTypeValue++;　//真ん中下
        }
    }

    public void OnGameEnd(string gameEndText)
    {
        var go = Instantiate(_gameEndPanel, _gridLayoutGroup.transform.parent);
        go.GetComponentInChildren<Text>().text = gameEndText;
    }


    /**
     隣接セル判定のみサイトを参考にしました（コピペではない）
     **/

    private Cell[] GetAdjoinCells(int r, int c)
    {
        var cells = new List<Cell>();

        var isTop = r == 0;
        var isButtom = r == _colums - 1;
        var isLeft = c == 0;
        var isRight = c == _rows - 1;

        // 左上
        if (!isTop && !isLeft)
        {
            cells.Add(_cells[r - 1, c - 1]);
        }
        // 上
        if (!isTop)
        {
            cells.Add(_cells[r - 1, c]);
        }
        // 右上
        if (!isTop && !isRight)
        {
            cells.Add(_cells[r - 1, c + 1]);
        }
        // 右
        if (!isRight)
        {
            cells.Add(_cells[r, c + 1]);
        }
        // 右下
        if (!isButtom && !isRight)
        {
            cells.Add(_cells[r + 1, c + 1]);
        }
        // 下
        if (!isButtom)
        {
            cells.Add(_cells[r + 1, c]);
        }
        // 左下
        if (!isButtom && !isLeft)
        {
            cells.Add(_cells[r + 1, c - 1]);
        }
        // 左の判定
        if (!isLeft)
        {
            cells.Add(_cells[r, c - 1]);
        }

        return cells.ToArray();
    }

    public void OpenAdjoinCells(int r, int c)
    {
        foreach (var cell in GetAdjoinCells(r, c))
        {
            if (cell.IsHide)
            {
                cell.OpenCell();
            }
        }
    }
}
