using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardAdjust : MonoBehaviour
{
    [SerializeField] Color keyColors;

    char[] keys = {'1', 'Q', 'A', 'Z', '2', 'W', 'S', 'X', '3', 'E', 'D', 'C',
                    '4', 'R', 'F', 'V', '5', 'T', 'G', 'B', '6', 'Y', 'H', 'N',
                    '7', 'U', 'J', 'M', '8', 'I', 'K', '<', '9', 'O', 'L', '>',
                    '0', 'P', ':', '?', ':', ':', ':'};
       
    private void Start()
    {
        int index = 0;
        foreach (TextMeshPro t in GetComponentsInChildren<TextMeshPro>(true))
        {
            t.text = keys[index].ToString();
            t.color = keyColors;
            t.fontSize = 6f;
            index++;
        }
    }
}
