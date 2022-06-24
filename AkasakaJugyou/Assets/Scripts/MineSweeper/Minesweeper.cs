using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minesweeper : MonoBehaviour
{
    [SerializeField]
    Cell _cellPrefab = null;
    [SerializeField]
    GridLayoutGroup _gridLayoutGroup = null;
    private void Awake()
    {
        if (!_cellPrefab)
        {
            _cellPrefab = Resources.Load<Cell>("cell");
        }
    }

    private void Start()
    {
        _gridLayoutGroup.constraint
           = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = 10;

        for (var r = 0; r < 10; r++)
        {
            for (var c = 0; c < 10; c++)
            {
                var cell = Instantiate(_cellPrefab, _gridLayoutGroup.transform);
                cell.CellTypeValue = CellType.Mine;
            }
        }
    }
}
