using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LifeGame;

[RequireComponent(typeof(GridLayoutGroup))]
public class LifeGameManager : MonoBehaviour
{
    [Header("���������")]
    [SerializeField, Tooltip("�Z���̍s��"),Range(1,100)]
    int _rows = 5;
    [SerializeField, Tooltip("�Z���̗�"), Range(1, 100)]
    int _colums = 5;

    [Header("�K�v�ȃR���|�[�l���g")]
    [SerializeField,Tooltip("GridLayoutGroup�R���|�[�l���g")]
    GridLayoutGroup _gridLayoutGroup = null;

    [Header("�v���n�u")]
    [SerializeField,Tooltip("Cell�N���X���������Z���̃v���n�u")]
    Cell _cellPrefab = null;

    /// <summary>�Z�����i�[����񎟌��z��</summary>
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

        //GridLayoutGroup�R���|�[�l���g�̐ݒ�
        _gridLayoutGroup.constraint�@�@                    
           = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _colums;

        CreateField();
    }

    void CreateField()
    {
        _cells = new Cell[_rows, _colums];�@//�Z���̓񎟌��z��̏�����
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
