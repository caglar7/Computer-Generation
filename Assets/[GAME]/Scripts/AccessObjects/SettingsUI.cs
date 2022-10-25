using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
}
