using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// different methods for each
// gate since we might have different effects for them

public class Gate : MonoBehaviour
{
    [SerializeField] GateType gateType;
    [SerializeField] Transform objIcon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagNames.StackComputer.ToString())
        {
            objIcon.gameObject.SetActive(false);
            Utils.instance.EnableColliderForDuration(other.GetComponent<Collider>(), false, 1f);
            Computer computer = other.GetComponent<Computer>();

            // check 1
            if (computer == null) return;

            switch(gateType)
            {
                case GateType.Sell:
                    SellComputer(computer, .3f);
                    break;

                case GateType.Graphic_Card:

                    // might show a graphic card effect here
                    // ...

                    // or an effect on top of computer like an upgrade
                    // ...

                    // add
                    computer.AddPartData(ComputerPart.Graphic_Card);

                    break;

                case GateType.Ram:

                    // effects and anims
                    // ...

                    // add
                    computer.AddPartData(ComputerPart.Ram);

                    break;

                case GateType.Camera:

                    // effect anim ...

                    // add
                    computer.AddPartData(ComputerPart.Camera);

                    break;

                case GateType.Screen_Larger:

                    // enlarge screen with scaling, prob just scaling laptops
                    // ...

                    // add
                    computer.AddPartData(ComputerPart.Screen_Larger);

                    break;
            }
        }
    }

    #region Sell
    private void SellComputer(Computer c, float delay)
    {
        StartCoroutine(SellComputerCo(c, delay));
    }

    IEnumerator SellComputerCo(Computer c, float delay)
    {
        yield return new WaitForSeconds(delay);

        // selling effect, or effect combinations
        // ...

        // update UI 
        TotalMoneyUI.instance.UpdateRevenue(c.price);

        // remove computer
        Destroy(c.gameObject);
    }
    #endregion
}


public enum GateType
{
    Sell,
    Graphic_Card,
    Ram,
    Camera,
    Protection_Sleeve,
    Screen_Larger,
    Style,
    Rotate,
}