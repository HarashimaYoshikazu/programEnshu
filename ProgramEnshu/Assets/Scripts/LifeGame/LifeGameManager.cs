using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LifeGame;

[RequireComponent(typeof(GridLayoutGroup))]
public class LifeGameManager : MonoBehaviour
{
    [Header("初期化情報")]
    [SerializeField, Tooltip("セルの行数"), Range(1, 100)]
    int _rows = 5;
    [SerializeField, Tooltip("セルの列数"), Range(1, 100)]
    int _colums = 5;

    GridLayoutGroup _gridLayoutGroup = null;

    [Header("プレハブ")]
    [SerializeField, Tooltip("Cellクラスを持ったセルのプレハブ")]
    Cell _cellPrefab = null;

    /// <summary>セルを格納する二次元配列</summary>
    Cell[,] _cells = null;
    private void Awake()
    {
        Init();
    }

    void Init()
    {
        //nullチェック
        if (!_gridLayoutGroup)
        {
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        }

        if (!_cellPrefab)
        {
            _cellPrefab = Resources.Load<Cell>("LifeGame/cell");
        }

        //GridLayoutGroupコンポーネントの設定
        _gridLayoutGroup.constraint
           = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _colums;

        CreateField();
    }

    void CreateField()
    {
        _cells = new Cell[_rows, _colums];　//セルの二次元配列の初期化

        for (int r = 0; r < _rows; r++)//セルを生成して配列に代入
        {
            for (int c = 0; c < _colums; c++)
            {
                var cell = Instantiate(_cellPrefab, _gridLayoutGroup.transform);

                //int rand = Random.Range(0, 2); //ランダムに状態を初期化
                //if (rand == 0)
                //{
                //    cell.IsAlive = true;
                //}
                //else
                //{
                //    cell.IsAlive = false;
                //}

                _cells[r, c] = cell;
            }
        }
    }

    public void Alternation()
    {
        foreach (var i in GetChangeCells())
        {
            i.ChangeState();
        }
    }
    Cell[] GetChangeCells()
    {
        List<Cell> tempCellList = new List<Cell>();//次の世代で状態が変わるセルを格納したリスト
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _colums; c++)
            {
                CheckCell(r, c);
            }
        }
        Debug.Log(tempCellList.Count);
        return tempCellList.ToArray();
    }

    bool CheckCell(int r, int c)
    {
        int livingCellCount = 0;

        var isTop = r == 0;
        var isButtom = r == _colums - 1;
        var isLeft = c == 0;
        var isRight = c == _rows - 1;

        // 左上
        if (!isTop && !isLeft)
        {
            if (_cells[r - 1, c - 1].IsAlive)
            {
                livingCellCount++;
            }
        }
        // 上
        if (!isTop)
        {
            if (_cells[r - 1, c].IsAlive)
            {
                livingCellCount++;
            }
        }
        // 右上
        if (!isTop && !isRight)
        {
            if (_cells[r - 1, c + 1].IsAlive)
            {
                livingCellCount++;
            }
        }
        // 右
        if (!isRight)
        {
            if (_cells[r, c + 1].IsAlive)
            {
                livingCellCount++;
            }
        }
        // 右下
        if (!isButtom && !isRight)
        {
            if (_cells[r + 1, c + 1].IsAlive)
            {
                livingCellCount++;
            }
        }
        // 下
        if (!isButtom)
        {
            if (_cells[r + 1, c].IsAlive)
            {
                livingCellCount++;
            }
        }
        // 左下
        if (!isButtom && !isLeft)
        {
            if (_cells[r + 1, c - 1].IsAlive)
            {
                livingCellCount++;
            }
        }
        // 左
        if (!isLeft)
        {
            if (_cells[r, c - 1].IsAlive)
            {
                livingCellCount++;
            }
        }

        //FIXME
        if (livingCellCount == 3 && !_cells[r, c].IsAlive)
        {
            return true;
        }
        else if ((livingCellCount == 3 || livingCellCount == 2) && _cells[r, c].IsAlive)
        {
            return false;
        }
        else if (livingCellCount <= 1 && _cells[r, c].IsAlive)
        {
            return true;
        }
        else if (livingCellCount >= 4 && _cells[r, c].IsAlive)
        {
            return true;
        }
        return false;
    }
}

