using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiControl : MonoBehaviour
{
    #region Singleton
    public static ConfettiControl instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    [SerializeField] GameObject confetti1;
    [SerializeField] GameObject confetti2;

    public void Blast()
    {
        confetti1.SetActive(true);
        confetti2.SetActive(true);
    }
}
