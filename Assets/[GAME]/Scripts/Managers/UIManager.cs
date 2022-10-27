using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

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

    [Header("Transform References")]
    Transform objPlayer;
    Transform objLevelEnd;

    [Header("Values")]
    float levelLength;

    private void Start()
    {
        FindObjects();
        PanelController(false, true, false, false);
    }

    private void FindObjects()
    {
        playerController = FindObjectOfType<PlayerController>();
        objPlayer = playerController.transform;
        objLevelEnd = FindObjectOfType<EndGame>().transform;
        levelLength = Vector3.Distance(objPlayer.position, objLevelEnd.position);
    }

    private void Update()
    {
        UpdateProgressBar();
    }

    // updating slider
    float remainingDis;
    private void UpdateProgressBar()
    {
        if(ProgressBarUI.instance)
        {
            remainingDis = Vector3.Distance(objPlayer.position, objLevelEnd.position);
            ProgressBarUI.instance.slider.value = Mathf.Clamp(1f - (remainingDis / levelLength), 0f, 1f);

        }
    }

    // 0 => start button
    // 1 => fail UI
    // 2 => success next level UI
    public void OpenClosePanels(int panelType) //  0 game in // 1 fail // 2 next level
    {
        switch (panelType)
        {
            case 0:
                //PanelController(false, true, false, false);

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
        CanvasSingleton.instance.panelStart.SetActive(startPanelVal);
        CanvasSingleton.instance.panelGameIn.SetActive(gameInPanelVal);
        CanvasSingleton.instance.panelRetry.SetActive(retryPanelVal);
        CanvasSingleton.instance.panelNextLevel.SetActive(nextPanelVal);
    }
}
