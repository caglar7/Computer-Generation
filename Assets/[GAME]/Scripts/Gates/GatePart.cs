using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatePart : MonoBehaviour
{
    public GatePart otherPart;

    public void EnableColliders(bool value)
    {
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = value;
        }
    }
}
