using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// data about the individual computers
// price data here

// when crossing gates or going through some machine
// tags StackComputer or CollectComputer is important

public class Computer : MonoBehaviour
{
    public Transform objGFX;
    public int price = 0;
    public List<ComputerPart> listParts = new List<ComputerPart>();

    // bools
    [HideInInspector] public bool isEnlarged = false;

    private void Start()
    {
        price = 0;
        AddPartData(ComputerPart.Body);
        AddPartData(ComputerPart.Screen_Frame);
    }

    public void AddPartData(ComputerPart part)
    {
        // add to the part collection
        listParts.Add(part);

        // update total price of the computer
        price += ComputerPartPrices.GetPrice(part);

        // update stack total price 
        MoneyTag.instance.UpdateStackPrice();
    }
}
