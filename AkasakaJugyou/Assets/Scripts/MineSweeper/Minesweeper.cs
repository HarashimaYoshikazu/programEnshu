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
                OnGameEnd("�N���A");
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
            _cells[row - 1, col].CellTypeValue++;//���^��
            if (col > 0)
            {
                _cells[row - 1, col - 1].CellTypeValue++;//����
            }
            if (col < _colums - 1)
            {
                _cells[row - 1, col + 1].CellTypeValue++;//����
            }
        }
        if (row < _rows - 1)
        {
            _cells[row + 1, col].CellTypeValue++; //�E�^��
            if (col > 0)
            {
                _cells[row + 1, col - 1].CellTypeValue++;//�E��
            }
            if (col < _colums - 1)
            {
                _cells[row + 1, col + 1].CellTypeValue++;//�E��
            }
        }

        if (col > 0)
        {
            _cells[row, col - 1].CellTypeValue++; //�^�񒆏�
        }
        if (col < _colums - 1)
        {
            _cells[row, col + 1].CellTypeValue++;�@//�^�񒆉�
        }
    }

    public void OnGameEnd(string gameEndText)
    {
        var go = Instantiate(_gameEndPanel, _gridLayoutGroup.transform.parent);
        go.GetComponentInChildren<Text>().text = gameEndText;
    }


    /**
     �אڃZ������̂݃T�C�g���Q�l�ɂ��܂����i�R�s�y�ł͂Ȃ��j
     **/

    private Cell[] GetAdjoinCells(int r, int c)
    {
        var cells = new List<Cell>();

        var isTop = r == 0;
        var isButtom = r == _colums - 1;
        var isLeft = c == 0;
        var isRight = c == _rows - 1;

        // ����
        if (!isTop && !isLeft)
        {
            cells.Add(_cells[r - 1, c - 1]);
        }
        // ��
        if (!isTop)
        {
            cells.Add(_cells[r - 1, c]);
        }
        // �E��
        if (!isTop && !isRight)
        {
            cells.Add(_cells[r - 1, c + 1]);
        }
        // �E
        if (!isRight)
        {
            cells.Add(_cells[r, c + 1]);
        }
        // �E��
        if (!isButtom && !isRight)
        {
            cells.Add(_cells[r + 1, c + 1]);
        }
        // ��
        if (!isButtom)
        {
            cells.Add(_cells[r + 1, c]);
        }
        // ����
        if (!isButtom && !isLeft)
        {
            cells.Add(_cells[r + 1, c - 1]);
        }
        // ���̔���
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
