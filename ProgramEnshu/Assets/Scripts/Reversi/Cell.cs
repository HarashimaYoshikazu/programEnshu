using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Reversi
{
    public class Cell : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {
        ReversiManager _manager;

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
            _manager = FindObjectOfType<ReversiManager>();
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
                    _currentCellState = CellState.White;
                    _piece.color = Color.white;
                    break;
                case CellState.White:
                    _currentCellState = CellState.Black;
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
            switch (_currentCellState)
            {
                case CellState.Black:
                    _piece.color = Color.black;
                    break;
                case CellState.White:
                    _piece.color = Color.white;
                    break;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log(_manager.GetCurrentTurnPlayer);
            ChangeCellState(_manager.GetCurrentTurnPlayer);
            _manager.NextTurn();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            
        }
    }
    public enum CellState
    {
        Black,
        White,
        None
    }
}



