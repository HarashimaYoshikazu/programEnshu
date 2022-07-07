using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Minesweeper : MonoBehaviour
{
    [Header("�Q�[���̐ݒ�")]
    [SerializeField]
    int _rows = 10;
    [SerializeField]
    int _colums = 10;

    [SerializeField, Range(0, 99)]
    int _mineCount = 5;

    [Header("���\�[�X")]
    [SerializeField]
    Cell _cellPrefab = null;

    [SerializeField]
    GridLayoutGroup _gridLayoutGroup = null;

    Cell[,] _cells = null;

    private void Awake()
    {
        if (!_cellPrefab)
        {
            _cellPrefab = Resources.Load<Cell>("cell");
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
        _gridLayoutGroup.constraintCount = 10;

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
            _cells[row - 1, col].CellTypeValue++;//���^��
            if (col > 0)
            {
                _cells[row - 1, col - 1].CellTypeValue++;//����
            }
            if (col < _colums-1)
            {
                _cells[row - 1, col + 1].CellTypeValue++;//����
            }
        }
        if (row < _rows-1)
        {
            _cells[row + 1, col].CellTypeValue++; //�E�^��
            if (col > 0)
            {
                _cells[row + 1, col - 1].CellTypeValue++;//�E��
            }
            if (col < _colums-1)
            {
                _cells[row + 1, col + 1].CellTypeValue++;//�E��
            }
        }

        if (col>0)
        {
            _cells[row, col - 1].CellTypeValue++; //�^�񒆏�
        }
        if (col<_colums-1)
        {
            _cells[row, col + 1].CellTypeValue++;�@//�^�񒆉�
        }
    }
}
