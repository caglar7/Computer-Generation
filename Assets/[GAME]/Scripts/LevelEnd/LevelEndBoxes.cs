using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelEndBoxes : MonoBehaviour
{
    #region singleton
    public static LevelEndBoxes instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    } 
    #endregion

    [Header("Generate Settings")]
    [SerializeField] Transform startPos;
    [SerializeField] float offsetY = 6f;
    [SerializeField] int howMany = 20;
    Vector3 nextLocal;

    [Header("Box Objects")]
    [SerializeField] GameObject[] boxes;

    [Header("Text Settings")]
    [SerializeField] int startMoney = 200;
    [SerializeField] int increase = 200;
    int currentMoney = 200;

    private void Start()
    {
        nextLocal = startPos.localPosition;
        currentMoney = startMoney;
        GenerateBoxes();
    }

    void GenerateBoxes()
    {
        for (int i = 0; i < howMany; i++)
        {
            // get obj, set pos
            int index = i % 2;
            GameObject copy = Instantiate(boxes[index], transform);
            copy.transform.localPosition = nextLocal;
            nextLocal.y += offsetY;

            // set text
            copy.GetComponentInChildren<TextMeshPro>().text = Utils.instance.ConvertedNumber(currentMoney);
            currentMoney += increase;
        }
    }

    public void AssignStopPoint(int stackMoney)
    {
        int checkValue = startMoney;
        foreach (LevelEndCube cube in transform.GetComponentsInChildren<LevelEndCube>())
        {
            if (checkValue >= stackMoney) cube.isStopPoint = true;
            checkValue += increase;
        }
    }
}
