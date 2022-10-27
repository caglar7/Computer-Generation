using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum ButtonType
{
    Start,
    Retry,
    NextLevel,
    Settings,
}

[RequireComponent(typeof(Button))]
public class ButtonClick : MonoBehaviour, IPointerDownHandler
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

    // start is exception from switch case
    // just getting pointer down to start
    public void OnPointerDown(PointerEventData eventData)
    {
        if(buttonType == ButtonType.Start)
        {
            UIManager.instance.OpenClosePanels(0);
            CanvasSingleton.instance.objShowGo.SetActive(true);
        }

    }
}
