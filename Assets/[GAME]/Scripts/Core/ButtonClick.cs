using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonType
{
    Start,
    Retry,
    NextLevel,
}

[RequireComponent(typeof(Button))]
public class ButtonClick : MonoBehaviour
{
    [SerializeField] ButtonType buttonType;
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonMethod);
    }

    void ButtonMethod()
    {
        switch(buttonType)
        {
            case ButtonType.Start:
                UIManager.instance.OpenClosePanels(0);
                break;

        }

    }
}
