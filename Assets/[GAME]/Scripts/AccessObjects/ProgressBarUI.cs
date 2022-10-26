using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBarUI : MonoBehaviour
{
    #region Singleton
    public static ProgressBarUI instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    public Slider slider;
    public TextMeshProUGUI textCurrentLevel;
    public TextMeshProUGUI textNextLevel;

    private void Start()
    {
        UpdateLevelTexts();
    }

    private void UpdateLevelTexts()
    {
        int fakeLevel = PlayerPrefs.GetInt("FakeLevel", 1);
        textCurrentLevel.text = fakeLevel.ToString();
        textNextLevel.text = (fakeLevel + 1).ToString();
    }
}
