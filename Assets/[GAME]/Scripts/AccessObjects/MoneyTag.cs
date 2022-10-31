using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyTag : MonoBehaviour
{
    #region Singleton
    public static MoneyTag instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    [Header("Fields")]
    public TextMeshProUGUI text;

    // values
    private int totalStackPrice = 0;

    private void Start()
    {
        // adjust rot for camera
        transform.eulerAngles = Camera.main.transform.eulerAngles;
    }

    public void UpdateStackPrice()
    {
        text.text = (ComputerStack.instance.GetTotalStackPrice()).ToString();
    }

}
