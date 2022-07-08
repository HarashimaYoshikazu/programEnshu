using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof (Button))]
public class SceneChangeButton : MonoBehaviour
{
    Button _button = null;
    [SerializeField]
    string _sceneName = "MineSweeper";
    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(_sceneName);
        }
        );
    }
}
