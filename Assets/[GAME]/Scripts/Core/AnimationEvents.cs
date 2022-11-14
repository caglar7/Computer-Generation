using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class AnimationEvents : MonoBehaviour
{
    private void PauseGameEvent()
    {
        GameManager.instance.PauseGame();
    }

    private void ResumeGameEvent()
    {
        GameManager.instance.ResumeGame();
    }

    private void PlayHaptic(HapticTypes type)
    {
        MMVibrationManager.Haptic(type);
    }
}
