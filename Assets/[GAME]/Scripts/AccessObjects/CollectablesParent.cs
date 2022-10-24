using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesParent : MonoBehaviour
{
    public static CollectablesParent instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
}
