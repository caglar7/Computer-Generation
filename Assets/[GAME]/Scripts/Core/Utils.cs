using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    #region Singleton
    public static Utils instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
    #endregion 

    #region Collider Enable Disable
    public void EnableColliderForDuration(Collider col, bool state, float duration)
    {
        StartCoroutine(EnableColliderForDurationCo(col, state, duration));
    }

    IEnumerator EnableColliderForDurationCo(Collider col, bool state, float duration)
    {
        if(col) col.enabled = state;
        yield return new WaitForSeconds(duration);
        if(col) col.enabled = !state;
    }
    #endregion

    #region Rotation Adjusting
    
    public float GetRotation360(float value)
    {
        if (value < 0) value += 360f;
        if (value >= 360f) value -= 360f;
        return value;
    }
    #endregion

    #region Convert Numbers

    public string ConvertedNumber(float value)
    {
        if (value >= 1000000f) return "1M";
        else if (value >= 1000f)
        {
            float kValue = value / 1000f;
            return kValue.ToString("0.0") + "K";
        }
        else return ((int)value).ToString();
    }

    #endregion
}

