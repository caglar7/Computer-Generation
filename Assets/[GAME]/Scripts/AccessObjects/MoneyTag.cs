using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyTag : MonoBehaviour
{
    #region Singleton
    public static MoneyTag instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    private void Start()
    {
        // adjust rot for camera
        transform.eulerAngles = Camera.main.transform.eulerAngles;
    }

}
