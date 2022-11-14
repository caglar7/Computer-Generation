using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelEndCube : MonoBehaviour
{
    float punchMult = 1.1f;
    bool punchDone = false;
    public bool isStopPoint = false;

    public void Punch()
    {
        transform.DOPunchScale(Vector3.forward * punchMult, .5f, 0, .1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagNames.LevelEndComputer.ToString())
        {
            if (punchDone) return;
            punchDone = true;

            Punch();
        }
    }
}
