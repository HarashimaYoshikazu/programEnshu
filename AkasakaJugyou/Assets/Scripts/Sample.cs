using UnityEngine;
using UnityEngine.UI;
public class Sample : MonoBehaviour
{
    [SerializeField]
    int arrayLength = 5;
    GameObject[] gos;
    int currentred = 0;
    bool[] isDestroyed;
    private void Start()
    {
        gos = new GameObject[arrayLength];
        isDestroyed = new bool[arrayLength];
        for (var i = 0; i < gos.Length; i++)
        {
            gos[i] = new GameObject($"Cell{i}");
            gos[i].transform.parent = transform;
            var image = gos[i].AddComponent<Image>();
            if (i == 0) { image.color = Color.red; }
            else { image.color = Color.white; }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // 左キーを押した
        {
            Left();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // 右キーを押した
        {
            Right();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Space();
        }
    }

    void Left()
    {
        if (currentred != 0 && isDestroyed[currentred - 1] && !isDestroyed[currentred])
        {
            for (int i = currentred - 2; i > -1; i--)
            {
                if (!isDestroyed[i])
                {
                    gos[currentred].GetComponent<Image>().color = Color.white;
                    currentred = i;
                    gos[currentred].GetComponent<Image>().color = Color.red;
                    break;
                }

            }
        }

        else if (gos[currentred] && currentred != 0)
        {
            gos[currentred].GetComponent<Image>().color = Color.white;
            currentred--;
            gos[currentred].GetComponent<Image>().color = Color.red;
        }
    }

    void Right()
    {
        if (currentred != gos.Length - 1 && isDestroyed[currentred + 1] && !isDestroyed[currentred])
        {
            for (int i = currentred + 2; i < gos.Length; i++)
            {
                if (!isDestroyed[i])
                {
                    gos[currentred].GetComponent<Image>().color = Color.white;
                    currentred = i;
                    gos[currentred].GetComponent<Image>().color = Color.red;
                    break;
                }

            }
        }
        else if (gos[currentred] && currentred != gos.Length - 1)
        {
            gos[currentred].GetComponent<Image>().color = Color.white;
            currentred++;
            gos[currentred].GetComponent<Image>().color = Color.red;
        }
    }

    void Space()
    {
        Destroy(gos[currentred]);
        isDestroyed[currentred] = true;
        for (int i = 0; i < arrayLength; i++)
        {
            if (!isDestroyed[i])
            {
                currentred = i;
                gos[currentred].GetComponent<Image>().color = Color.red;
                break;
            }
        }
    }
}