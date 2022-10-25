using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField] ComputerPart addedPart;

    [Header("Animation Check")]
    [SerializeField] bool playAnimationAfter = false;
    [SerializeField] float animationDelay = .5f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagNames.StackComputer.ToString())
        {
            // check
            ComputerMeshControl meshControl = other.GetComponentInChildren<ComputerMeshControl>();
            if (meshControl == null) return;

            // add computer part
            meshControl.AddComputerPart(addedPart);

            // data update in Computer class
        }
    }
}
