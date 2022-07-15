using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace LifeGame
{
    [RequireComponent(typeof(Image))]
    public class Cell : MonoBehaviour, IPointerDownHandler
    {
        bool _isAlive = true;
        public bool IsAlive 
        {
            get { return _isAlive; }
            set
            {
                _isAlive = value;
                if (_isAlive)
                {
                    _image.color = Color.white;
                }
                else
                {
                    _image.color = Color.black;
                }
            }
        }

        Image _image = null;

        private void Awake()
        {
            if (!_image)
            {
                _image = GetComponent<Image>();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ChangeState();
        }

        void ChangeState()
        {
            IsAlive = !IsAlive;//”½“]
        }
    }
}

