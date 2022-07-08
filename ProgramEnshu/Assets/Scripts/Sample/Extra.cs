using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Extra : MonoBehaviour
{
    [SerializeField]
    int arrayLength = 5;
    GameObject[] gos;
    int currentred = 0;
    bool[] isRed;
    private void Start()
    {
        gos = new GameObject[arrayLength];
        isRed = new bool[arrayLength];
        for (var i = 0; i < gos.Length; i++)
        {
            gos[i] = new GameObject($"Cell{i}");
            gos[i].transform.parent = transform;
            var image = gos[i].AddComponent<Image>();

            int num = Random.Range(0,2);
            if(num ==0)
            {
                isRed[i] = true;
                image.color = Color.red;
            }
            else
            {
                isRed[i] = false;
                image.color = Color.white;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
