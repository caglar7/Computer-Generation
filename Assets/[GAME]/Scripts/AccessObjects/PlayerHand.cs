using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    #region Singleton
    public static PlayerHand instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    public Transform followPoint;
}
