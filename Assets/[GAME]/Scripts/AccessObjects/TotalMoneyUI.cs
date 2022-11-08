using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalMoneyUI : MonoBehaviour
{
    #region Singleton
    public static TotalMoneyUI instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    } 
    #endregion

    public TextMeshProUGUI text;
    public int revenue = 0;
    
    public void UpdateRevenue(int added = 0)
    {
        // value
        revenue = PlayerPrefs.GetInt("Revenue", 0);
        revenue += added;
        PlayerPrefs.SetInt("Revenue", revenue);

        // UI
        text.text = revenue.ToString() + "$";
    }
}
