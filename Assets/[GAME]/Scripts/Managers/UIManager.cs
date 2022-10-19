using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;     
    }
    #endregion

    [Header("Scipt References")]
    PlayerController playerController;

    [Header("References")]
    [SerializeField] Camera cam;


    private void Start()
    {       
        playerController = FindObjectOfType<PlayerController>();
    }

    public void OpenClosePanels(int panelType) //  0 start // 1 fail // 2 nexxtLevel
    {
        switch (panelType)
        {
            case 0:
                PanelController(false, true, false, false);

                playerController.UserActiveController(true);

                GameManager.instance.SendLevelStartedEvent();
                GameManager.instance.ManageGameStatus(GameManager.GameSituation.isStarted);
                break;

            case 1:
                PanelController(false, false, true, false);

                playerController.UserActiveController(false);
                break;

            case 2:
                PanelController(false, false, false, true);

                playerController.UserActiveController(false);                
                break;
        }
    }

    void PanelController(bool startPanelVal, bool gameInPanelVal, bool retryPanelVal, bool nextPanelVal)
    {
        // canvas singleton implement
        //startPanel.SetActive(startPanelVal);
        //gameInPanel.SetActive(gameInPanelVal);
        //retryPanel.SetActive(retryPanelVal);
        //nextLevelPanel.SetActive(nextPanelVal);
    }
}
