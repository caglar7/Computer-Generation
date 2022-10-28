using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [Header("Robot Settings")]
    [SerializeField] ComputerPart addedPart;

    [Header("Animation Settings")]
    [SerializeField] bool enableAnimation = false;
    [SerializeField] float animationDelay = .5f;
    [SerializeField] ComputerAnimationType animationType;

    [Header("Money Text")]
    [SerializeField] MoneyAdded moneyAdded;
    
    [Header("Values")]
    List<Transform> listComputers = new List<Transform>();


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagNames.StackComputer.ToString())
        {
            // check 1
            ComputerMeshControl meshControl = other.GetComponentInChildren<ComputerMeshControl>();
            if (meshControl == null) return;

            // check 2
            if (listComputers.Contains(other.transform)) return;
            listComputers.Add(other.transform);

            // add computer part
            meshControl.AddComputerPart(addedPart);

            // animation
            if (enableAnimation)
                other.GetComponent<ComputerAnimations>().JumpRotateY(animationDelay);

            // data update in Computer class
            other.GetComponent<Computer>().AddPartData(addedPart);

            // add money
            moneyAdded.Show();
            moneyAdded.Add(ComputerPartPrices.GetPrice(addedPart));
            moneyAdded.ScaleAnimation(2.5f);
        }
    }
}
