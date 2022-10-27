using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonType
{
    Start,
    Retry,
    NextLevel,
    Settings,
}

[RequireComponent(typeof(Button))]
public class ButtonClick : MonoBehaviour
{
    [SerializeField] ButtonType buttonType;
    Button button;
    bool isSettingsShown = false;

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

            case ButtonType.NextLevel:
                GameManager.instance.NextLevel();
                break;

            case ButtonType.Retry:
                GameManager.instance.RetryLevel();
                break;

            case ButtonType.Settings:

                if(isSettingsShown == false && SettingsUI.instance)
                {
                    SettingsUI.instance.GetComponent<Animator>().SetBool("Show", true);
                    SettingsUI.instance.GetComponent<Animator>().SetBool("Hide", false);
                }else if(SettingsUI.instance)
                {
                    SettingsUI.instance.GetComponent<Animator>().SetBool("Show", false);
                    SettingsUI.instance.GetComponent<Animator>().SetBool("Hide", true);
                }
                isSettingsShown = !isSettingsShown;

                break;
        }

    }
}
