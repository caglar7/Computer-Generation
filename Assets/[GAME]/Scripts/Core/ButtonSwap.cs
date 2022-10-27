using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwap : MonoBehaviour
{
    [SerializeField] Button target;
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonMethod);
    }

    private void ButtonMethod()
    {
        target.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
