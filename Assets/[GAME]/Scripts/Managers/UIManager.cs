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

    [Header("Panels")]
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject gameInPanel;
    [SerializeField] GameObject retryPanel;
    [SerializeField] GameObject nextLevelPanel;

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI diamondNumText;
    [SerializeField] TextMeshProUGUI gameInPanelLevelText;
    [SerializeField] TextMeshProUGUI retryPanelLevelText;
    [SerializeField] TextMeshProUGUI successPanelLevelText;

    [Header("Diamond Variables")]
    [SerializeField] Image diamondImage;
    [SerializeField] GameObject diamond;
    [HideInInspector] public int diamondNum;
    Vector2 anchoredDiamondPos;

    [Header("Level Progress")]
    [SerializeField] Image levelProgressBar;
    [SerializeField] Transform endOfLevel;
    Transform player;
    float distToEnd;
    float currentDistToEnd;

    [Header("Scipt References")]
    PlayerController playerController;

    [Header("References")]
    [SerializeField] Camera cam;


    private void Start()
    {       
        playerController = FindObjectOfType<PlayerController>();
        player = playerController.transform;

        DiamondFirstSet();
        StartCalculate();
    }

    public void UpdateLevelText()
    {
        levelText.text = "LEVEL " + PlayerPrefs.GetInt("FakeLevel", 1).ToString();
        gameInPanelLevelText.text = "LEVEL " + PlayerPrefs.GetInt("FakeLevel", 1).ToString();
        retryPanelLevelText.text = "LEVEL " + PlayerPrefs.GetInt("FakeLevel", 1).ToString();
        successPanelLevelText.text = "LEVEL " + PlayerPrefs.GetInt("FakeLevel", 1).ToString();
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
        startPanel.SetActive(startPanelVal);
        gameInPanel.SetActive(gameInPanelVal);
        retryPanel.SetActive(retryPanelVal);
        nextLevelPanel.SetActive(nextPanelVal);
    }

    private void DiamondFirstSet() // initial uı adjust
    {
        diamondNum = PlayerPrefs.GetInt("Diamond", 0);
        diamondNumText.text = diamondNum.ToString();
        anchoredDiamondPos = diamondImage.GetComponent<RectTransform>().anchoredPosition;
    }

    public void DiamondCollectAnim(Vector3 diamondPos) // just call this method to increase diamond count "1" 
    {
        Vector2 screenPos = cam.WorldToScreenPoint(diamondPos);
        GameObject clone = PoolManager.Instance.moneyPool.PullObjFromPool(); /*Instantiate(diamond, screenPos, Quaternion.identity, gameInPanel.transform);*/

        clone.transform.localScale = Vector3.one * .5f; 

        RectTransform rectClone = clone.GetComponent<RectTransform>();
        rectClone.anchoredPosition = screenPos;

        clone.transform.parent = gameInPanel.transform;

        rectClone.DOAnchorPos(anchoredDiamondPos, .5f)
            .OnComplete(() => 
            { 
                clone.SetActive(false);
                UpdateDiamondText();
            });
    }

    void UpdateDiamondText()
    {
        diamondNum++;
        PlayerPrefs.SetInt("Diamond", diamondNum);
        diamondNumText.text = PlayerPrefs.GetInt("Diamond").ToString();
    }

    #region Level Progress Bar

    void StartCalculate()
    {
        distToEnd = (player.position - endOfLevel.position).sqrMagnitude;
    }

    public void UpdateProgressBar()
    {
        currentDistToEnd = (player.position - endOfLevel.position).sqrMagnitude;
        levelProgressBar.fillAmount = currentDistToEnd / distToEnd;
    }

    #endregion

}
