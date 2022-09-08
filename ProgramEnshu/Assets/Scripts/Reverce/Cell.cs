using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Reverce
{
    public class Cell : MonoBehaviour,IPointerClickHandler
    {
        [SerializeField]
        Image _piece;

        [SerializeField]
        CellState _currentCellState = CellState.None;
        public CellState GetCellState => _currentCellState;
        private void Awake()
        {
            if (!_piece)
            {
                Debug.LogError("セルにコマが存在していません。");
            }
        }
        private void OnValidate()
        {
            switch (_currentCellState)
            {
                case CellState.None:
                    _piece.color = new Color(0,0,0,0);
                    break;
                case CellState.Black:
                    _piece.color = Color.black;
                    break;
                case CellState.White:
                    _piece.color = Color.white;
                    break;
            }
        }

        /// <summary>
        /// 反転
        /// </summary>
        public void ChangeCellState()
        {
            switch (_currentCellState)
            {
                case CellState.Black:
                    _piece.color = Color.white;
                    break;
                case CellState.White:
                    _piece.color = Color.black;
                    break;
            }
        }

        /// <summary>
        /// 指定
        /// </summary>
        public void ChangeCellState(CellState cellState)
        {
            _currentCellState = cellState;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(this.gameObject.name);
        }
    }
    public enum CellState
    {
        Black,
        White,
        None
    }
}



