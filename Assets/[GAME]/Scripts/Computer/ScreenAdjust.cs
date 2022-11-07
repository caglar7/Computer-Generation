using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAdjust : MonoBehaviour
{
    [SerializeField] Transform screenFrame;

    private void Start()
    {
        transform.position = screenFrame.position;
        transform.rotation = screenFrame.rotation;
    }
}
