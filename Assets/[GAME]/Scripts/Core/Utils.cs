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
        col.enabled = state;
        yield return new WaitForSeconds(duration);
        col.enabled = !state;
    }
    #endregion

}

