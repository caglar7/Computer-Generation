﻿using System.Collections;
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

                case GateType.Rotate:

                    RotateLaptop(other.transform);

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

    #region Enlarge Screen

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

    #region Rotate Laptop

    private void RotateLaptop(Transform obj, float duration = .5f, Ease ease = Ease.Linear)
    {
        float rotY = obj.eulerAngles.y;
        float nextY = rotY + 180f;
        Vector3 rot = obj.eulerAngles;

        obj.DORotate(Vector3.up * 180f, duration).SetEase(ease);

        //DOTween.To(() => rotY, y => rotY = y, nextY, duration).SetEase(ease)
        //    .OnUpdate(() => {

        //        if (rotY < 0f) rotY += 360f;
        //        rot.y = rotY;
        //        obj.eulerAngles

        //    });

        
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