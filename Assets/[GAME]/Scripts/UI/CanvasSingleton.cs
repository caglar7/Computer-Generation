using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All UI elements are accessed from here


public class CanvasSingleton : MonoBehaviour
{
    #region Singleton
    public static CanvasSingleton instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    } 
    #endregion

    [Header("Panels")]
    public GameObject panelStart;
    public GameObject panelGameIn;
    public GameObject panelRetry;
    public GameObject panelNextLevel;

    [Header("Game In")]
    public GameObject objShowGo;
}
