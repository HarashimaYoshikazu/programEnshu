using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField]
    Text _text = null;

    [SerializeField]
    CellType _cellType = CellType.None;
    public CellType CellTypeValue
    {
        get => _cellType;

        set
        {
            if (_cellType!=CellType.Mine)
            {
                _cellType = value;
                SelectCellType();
            }           
        }
    }


    void Awake()
    {
        if (!_text)
        {
            _text = GetComponentInChildren<Text>();
        }
    }

    private void OnValidate()
    {
        SelectCellType();
    }
    void Start()
    {
        SelectCellType();
    }

    private void SelectCellType()
    {
        if (_cellType == CellType.Mine)
        {
            _text.text = "Åö";
        }
        else if (_cellType == CellType.None)
        {
            _text.text = "";
        }
        else
        {
            _text.text = ((int)_cellType).ToString();
        }
    }
}

public enum CellType
{
    Mine = -1,//ínóãÉZÉã

    None = 0,//âΩÇ‡Ç»Ç¢ÉZÉã
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8
}
