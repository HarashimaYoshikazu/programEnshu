using UnityEngine;
using UnityEngine.UI;

public class TwoDimensionalArray : MonoBehaviour
{
    [SerializeField, Tooltip("çsÇÃëÂÇ´Ç≥")]
    int _yCount = 5;
    [SerializeField, Tooltip("óÒÇÃëÂÇ´Ç≥")] 
    int _xCount = 5;

    /// <summary>ImageÇÃîzóÒ </summary>
    Image[,] _imageArray;
    /// <summary>è¡Ç¶ÇΩÇ©ë∂ç›Ç∑ÇÈÇ©ÇÃîzóÒ </summary>
    bool[,] _isDestroyed;

    /// <summary>åªç›ÇÃê‘Y </summary>
     int _currentY = 0;
    /// <summary>åªç›ÇÃê‘X </summary>
     int _currentX = 0;

    [Tooltip("GridLayoutGroup")] GridLayoutGroup _gridLayoutGroup;
    private void Start()
    {
        _gridLayoutGroup = this.gameObject.GetComponent<GridLayoutGroup>();
        _gridLayoutGroup.constraintCount = _xCount;

        _imageArray = new Image[_yCount, _xCount];
        _isDestroyed = new bool[_yCount, _xCount];

        for (var r = 0; r < _imageArray.GetLength(0); r++)
        {
            for (var c = 0; c < _imageArray.GetLength(1); c++)
            {
                var obj = new GameObject($"Cell({r}, {c})");
                obj.transform.parent = transform;

                var image = obj.AddComponent<Image>();
                if (r == 0 && c == 0)
                {
                    _imageArray[r, c] = image;
                    image.color = Color.red;
                }
                else
                {
                    _imageArray[r, c] = image;
                    image.color = Color.white;
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_currentX > 0)
            {
                if (!_isDestroyed[_currentY, _currentX - 1])
                {
                    _currentX--;
                    ChangeColor();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_currentX < _xCount - 1)
            {
                if (!_isDestroyed[_currentY, _currentX + 1])
                {
                    _currentX++;
                    ChangeColor();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            if (_currentY > 0)
            {
                if (!_isDestroyed[_currentY - 1, _currentX])
                {
                    _currentY--;
                    ChangeColor();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_currentY < _yCount - 1)
            {
                if (!_isDestroyed[_currentY + 1, _currentX])
                {
                    _currentY++;
                    ChangeColor();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyBlock();
        }
    }

    void ChangeColor()
    {
        for (int i = 0; i < _yCount; i++)
        {
            for (int j = 0; j < _xCount; j++)
            {
                if (_isDestroyed[i, j])
                {
                    continue;
                }

                if (i == _currentY && j == _currentX)
                {
                    _imageArray[i, j].color = Color.red;
                }
                else
                {
                    _imageArray[i, j].color = Color.white;
                }

            }
        }
    }

    void DestroyBlock()
    {
        _imageArray[_currentY, _currentX].color = Color.clear;
        _isDestroyed[_currentY, _currentX] = true;

        for (int i = 0; i < _yCount; i++)
        {
            for (int j = 0; j < _xCount; j++)
            {
                if (!_isDestroyed[i, j])
                {
                    _currentX = j;
                    _currentY = i;
                    ChangeColor();
                    return;
                }

            }
        }
    }

}