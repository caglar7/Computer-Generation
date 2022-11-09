using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
                    EnlargeScreen(other.transform, .3f);

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

        // moving computer to the center further of the sell gate
        // after that, right after remove and sell

        // update UI 
        TotalMoneyUI.instance.UpdateRevenue(c.price);

        // remove computer
        Destroy(c.gameObject);

        // delay update stack price
        yield return new WaitForSeconds(.1f);

        // update stack money count
        MoneyTag.instance.UpdateStackPrice();
    }
    #endregion

    #region

    private void EnlargeScreen(Transform obj, float delay = .05f, float animTime = .3f)
    {
        StartCoroutine(EnlargeScreenCo(obj, delay, animTime));
    }

    IEnumerator EnlargeScreenCo(Transform obj, float delay, float animTime)
    {
        // get fields that will be scaled
        Transform objMain = obj;
        Transform objScreenParts = obj.GetComponentInChildren<ComputerMeshControl>().obj_ScreenParts;

        // values
        float mult = ComputerStack.instance.enlargeMult;
        float targetMain = objMain.localScale.x * mult;
        float targetScreenParts = objScreenParts.localScale.y * mult;   // on y will be scaled
        float targetPunch = targetMain * 1.3f;

        // delay
        yield return new WaitForSeconds(delay);

        // main obj scale
        objMain.DOScale(targetPunch, animTime / 3f)
            .OnComplete(() => {
                objMain.DOScale(targetMain, animTime * 2f / 3f);
            });

        // screen obj scale
        objScreenParts.DOScaleY(targetScreenParts, animTime)
            .OnComplete(() => {
                objMain.GetComponent<FrontStackMovement>().UpdateOffsetZ();

            });
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