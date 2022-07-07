using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    Text _text = null;
    [SerializeField]
    GameObject _hideObject;

    [SerializeField]
    CellType _cellType = CellType.None;

    Minesweeper _minesweeper;
    public CellType CellTypeValue
    {
        get => _cellType;

        set
        {
            if (_cellType != CellType.Mine)
            {
                _cellType = value;
                SelectCellType();
            }
        }
    }

    Vector2Int _cellIndex = new Vector2Int();
    public Vector2Int CellIndex
    {
        get => _cellIndex;
        set => _cellIndex = value;
    }
    bool isHide = true;


    void Awake()
    {
        if (!_text)
        {
            _text = GetComponentInChildren<Text>();
        }
        if (!_hideObject)
        {
            _hideObject = GetComponentInChildren<Image>().gameObject;
        }
        _minesweeper = FindObjectOfType<Minesweeper>();
    }

    private void OnValidate()
    {
        SelectCellType();
    }
    void Start()
    {
        SelectCellType();
        if (isHide)
        {
            _hideObject.SetActive(true);
        }
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_minesweeper.IsInit)
        {
            _minesweeper.SetMine(_cellIndex.x,_cellIndex.y);
        }
        OpenCell();
    }

    private void OpenCell()
    {
        if (isHide)
        {
            _hideObject.SetActive(false);
            isHide = false;
        }
        if (_cellType == CellType.Mine)
        {
            _minesweeper.OnGameEnd("îöî≠");
        }
        else
        {
            _minesweeper.Count++;
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
    Eight = 8,
}
