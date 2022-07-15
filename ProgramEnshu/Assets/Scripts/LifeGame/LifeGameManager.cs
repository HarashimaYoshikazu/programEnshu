using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LifeGame;

[RequireComponent(typeof(GridLayoutGroup))]
public class LifeGameManager : MonoBehaviour
{
    [Header("初期化情報")]
    [SerializeField, Tooltip("セルの行数"),Range(1,100)]
    int _rows = 5;
    [SerializeField, Tooltip("セルの列数"), Range(1, 100)]
    int _colums = 5;

    [Header("必要なコンポーネント")]
    [SerializeField,Tooltip("GridLayoutGroupコンポーネント")]
    GridLayoutGroup _gridLayoutGroup = null;

    [Header("プレハブ")]
    [SerializeField,Tooltip("Cellクラスを持ったセルのプレハブ")]
    Cell _cellPrefab = null;

    /// <summary>セルを格納する二次元配列</summary>
    Cell[,] _cells = null; 
    private void Awake()
    {
        Init();
    }
    
    void Init()
    {
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
        for (int r = 0;r<_rows;r++)
        {
            for (int c =0;c<_colums;c++)
            {
                var cell = Instantiate(_cellPrefab,_gridLayoutGroup.transform);
                _cells[r, c] = cell;
            }
        }
    }
}
