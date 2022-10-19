using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [Header("Sky Components")]
    [SerializeField] Color fogColor;
    [SerializeField] Material skyMaterial;

    void Awake()
    {
        SetSkyboxSettings();
    }

    void SetSkyboxSettings()
    {
        RenderSettings.skybox = skyMaterial;
        RenderSettings.fogColor = fogColor;
    }
}
