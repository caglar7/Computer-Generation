using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField] ComputerPart addedPart;

    [Header("Animation Jump Rotate")]
    [SerializeField] bool enableAnimation = false;
    [SerializeField] float animationDelay = .5f;
    [SerializeField] ComputerAnimationType animationType;
        
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagNames.StackComputer.ToString())
        {
            // check
            ComputerMeshControl meshControl = other.GetComponentInChildren<ComputerMeshControl>();
            if (meshControl == null) return;

            // add computer part
            meshControl.AddComputerPart(addedPart);

            // animation
            if (enableAnimation)
                other.GetComponent<ComputerAnimations>().JumpRotateY(animationDelay);


            // data update in Computer class
        }
    }
}
