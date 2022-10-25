using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
