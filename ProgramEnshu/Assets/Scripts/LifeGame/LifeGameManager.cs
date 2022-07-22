using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LifeGame;

[RequireComponent(typeof(GridLayoutGroup))]
public class LifeGameManager : MonoBehaviour
{
    [Header("���������")]
    [SerializeField, Tooltip("�Z���̍s��"), Range(1, 100)]
    int _rows = 5;
    [SerializeField, Tooltip("�Z���̗�"), Range(1, 100)]
    int _colums = 5;

    [SerializeField, Tooltip("���������邩�ǂ���")]
    bool _isAuto = false;
    [SerializeField, Tooltip("�Z�����؂�ւ�鎞��")]
    float _timeLimit = 0.5f;
    float _timer = 0f;

    [SerializeField, Tooltip("�������������_���ɂ��邩")]
    bool _isRandom = false;

    GridLayoutGroup _gridLayoutGroup = null;
    

    [Header("�v���n�u")]
    [SerializeField, Tooltip("Cell�N���X���������Z���̃v���n�u")]
    Cell _cellPrefab = null;

    /// <summary>�Z�����i�[����񎟌��z��</summary>
    Cell[,] _cells = null;
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (_isAuto)
        {
            _timer += Time.deltaTime;
            if (_timer>_timeLimit)
            {
                Alternation();
                _timer = 0f;
            }
        }

        if (_isRandom)
        {
            ResetCells();
            _isRandom = false;
        }
    }

    void Init()
    {
        //null�`�F�b�N
        if (!_gridLayoutGroup)
        {
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        }

        if (!_cellPrefab)
        {
            _cellPrefab = Resources.Load<Cell>("LifeGame/cell");
        }

        //GridLayoutGroup�R���|�[�l���g�̐ݒ�
        _gridLayoutGroup.constraint
           = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _colums;

        CreateField();
    }

    void CreateField()
    {
        _cells = new Cell[_rows, _colums]; //�Z���̓񎟌��z��̏�����
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _colums; c++)
            {
                var cell = Instantiate(_cellPrefab, _gridLayoutGroup.transform);
                _cells[r, c] = cell;
            }
        }
    }

    void ResetCells()
    {
        for (int r = 0; r < _rows; r++)//�Z���𐶐����Ĕz��ɑ��
        {
            for (int c = 0; c < _colums; c++)
            {

                if (_isRandom)
                {
                    var cell = _cells[r, c];
                    int rand = Random.Range(0, 2); //�����_���ɏ�Ԃ�������
                    if (rand == 0)
                    {
                        cell.IsAlive = true;
                    }
                    else
                    {
                        cell.IsAlive = false;
                    }
                }
            }
        }
    }

    /// <summary>
    /// ���̐���ɂ���A�{�^����z��
    /// </summary>
    public void Alternation()
    {
        foreach (var i in GetChangeCells())
        {
            i.ChangeState();
        }
    }

    /// <summary>
    /// ��ԕω����N����Z���z���Ԃ�
    /// </summary>
    /// <returns></returns>
    Cell[] GetChangeCells()
    {
        List<Cell> tempCellList = new List<Cell>();//���̐���ŏ�Ԃ��ς��Z�����i�[�������X�g
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _colums; c++)
            {
                if (CheckCell(r, c))
                {
                    tempCellList.Add(_cells[r, c]);
                }               
            }
        }
        Debug.Log(tempCellList.Count);
        return tempCellList.ToArray();
    }


    /// <summary>
    /// �Z�������C�t�Q�[���̏����ɓ��Ă͂܂邩���肷��֐�
    /// </summary>
    bool CheckCell(int r, int c)
    {
        int livingCellCount = 0;

        var isTop = r == 0;
        var isButtom = r == _colums - 1;
        var isLeft = c == 0;
        var isRight = c == _rows - 1;

        // ����
        if (!isTop && !isLeft)
        {
            if (_cells[r - 1, c - 1].IsAlive)
            {
                livingCellCount++;
            }
        }
        // ��
        if (!isTop)
        {
            if (_cells[r - 1, c].IsAlive)
            {
                livingCellCount++;
            }
        }
        // �E��
        if (!isTop && !isRight)
        {
            if (_cells[r - 1, c + 1].IsAlive)
            {
                livingCellCount++;
            }
        }
        // �E
        if (!isRight)
        {
            if (_cells[r, c + 1].IsAlive)
            {
                livingCellCount++;
            }
        }
        // �E��
        if (!isButtom && !isRight)
        {
            if (_cells[r + 1, c + 1].IsAlive)
            {
                livingCellCount++;
            }
        }
        // ��
        if (!isButtom)
        {
            if (_cells[r + 1, c].IsAlive)
            {
                livingCellCount++;
            }
        }
        // ����
        if (!isButtom && !isLeft)
        {
            if (_cells[r + 1, c - 1].IsAlive)
            {
                livingCellCount++;
            }
        }
        // ��
        if (!isLeft)
        {
            if (_cells[r, c - 1].IsAlive)
            {
                livingCellCount++;
            }
        }

        if (livingCellCount == 3 && !_cells[r, c].IsAlive)
        {
            return true;
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

