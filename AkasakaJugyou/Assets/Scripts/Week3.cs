using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Week3 : MonoBehaviour, IPointerClickHandler
{
    [SerializeField,Tooltip("セル配列の列の配列長")]
    int _xLength = 5;
    [SerializeField, Tooltip("セル配列の行の配列長")]
    int _yLength = 5;

    [SerializeField]
    Image[,] _cells = null;
    int count = 0;

    bool isGameEnd = false;

    bool[,] _isBlack = null;
    private void Start()
    {
        
        //配列の初期化
        _cells = new Image[_yLength, _xLength];
        _isBlack = new bool[_yLength, _xLength];

        //配列に代入
        for (var r = 0; r < _yLength; r++)
        {
            for (var c = 0; c < _xLength; c++)
            {
                var cell = new GameObject($"Cell({r}, {c})");

                cell.transform.parent = transform;
                _cells[r, c] = cell.AddComponent<Image>();
                int n = Random.Range(0,2);
                if(n==0)
                {
                    _cells[r, c].color = Color.black;
                }
                else
                {
                    _cells[r, c].color = Color.white;
                }

                var data = cell.AddComponent<CellData>();
                data.X = c;
                data.Y = r;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isGameEnd)
        {
            count++;
            var data = eventData.pointerPressRaycast.gameObject.GetComponent<CellData>();
            InversionColor(data.Y, data.X);
            Check();
        }
    }

    private void InversionColor(int c,int r)
    {

        _cells[c,r].color = _isBlack[c, r] ? Color.white : Color.black;
        _isBlack[c, r] = !_isBlack[c, r];

        if (c!=0)
        {
            _cells[c-1,r].color = _isBlack[c-1,r]? Color.white: Color.black;
            _isBlack[c - 1, r] = !_isBlack[c - 1, r];
        }
        if(c!=_yLength-1)
        {
            _cells[c + 1, r].color = _isBlack[c + 1, r] ? Color.white : Color.black;
            _isBlack[c + 1, r] = !_isBlack[c + 1, r];
        }

        if(r!=0)
        {
            _cells[c, r-1].color = _isBlack[c, r-1] ? Color.white : Color.black;
            _isBlack[c, r-1] = !_isBlack[c, r-1];
        }
        if(r != _xLength-1)
        {
            _cells[c, r + 1].color = _isBlack[c, r+1] ? Color.white : Color.black;
            _isBlack[c, r + 1] = !_isBlack[c, r + 1];
        }
    }

    [SerializeField]
    GameObject _panel;

    [SerializeField]
    Text _text;
    
    void Check()
    {
        bool prevBlack = true;
        bool isClear = true;
        for (var r = 0; r < _yLength; r++)
        {
            for (var c = 0; c < _xLength; c++)
            {
                if(r==0 && c==0)
                {
                    if (_cells[r,c].color ==Color.white)
                    {
                        prevBlack = false;
                    }
                    else
                    {
                        prevBlack = true;
                    }
                }
                if (_cells[r, c].color == Color.white && prevBlack)
                {
                    isClear = false;
                    break;
                }
                else if(_cells[r, c].color == Color.black && !prevBlack)
                {
                    isClear = false;
                    
                    break;
                }
                if (!isClear)
                {
                    
                    break;
                }
            }
        }
        if (isClear)
        {
            isGameEnd = true;
            _text.text = count.ToString();
            _panel.SetActive(true);
        }
    }
}
