using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonType
{
    Start,
    Retry,
    NextLevel,
    ShowSettings,
    HideSettings,
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

            case ButtonType.ShowSettings:
                if (SettingsUI.instance)
                {

                    SettingsUI.instance.GetComponent<Animator>().SetTrigger("Show");
                }
                break;

            case ButtonType.HideSettings:
                if (SettingsUI.instance)
                    SettingsUI.instance.GetComponent<Animator>().SetTrigger("Hide");
                break;
        }

    }
}
