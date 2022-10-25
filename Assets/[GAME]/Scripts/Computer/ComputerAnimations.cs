using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ComputerAnimations : MonoBehaviour
{
    public Transform objGFX;
    Vector3 initScale;

    private void Start()
    {
        initScale = objGFX.localScale;
    }

    public void PlayAnimation(ComputerAnimationType type)
    {
        switch(type)
        {
            case ComputerAnimationType.Scaling:
                Scaling();
                break;

            case ComputerAnimationType.JumpRotate:
                JumpRotateY();
                break;
        }
    }

    public void Scaling(float mult = 1.5f, float duration = .5f, Ease ease = Ease.Linear)
    {
        objGFX.DOKill();
        objGFX.DOScale(initScale * mult, duration / 3f).SetEase(ease)
            .OnComplete(() => {

                objGFX.DOScale(initScale, duration * 2f / 3f).SetEase(ease);

            });
    }

    public void JumpRotateY(float initDelay = .5f)
    {
        // settings
        float jumpTime = 1f;
        float startRotDelay = 0f;
        float rotationTime = 1f;
        float jumpPower = 2.5f;

        // jump 
        Vector3 pos = objGFX.localPosition;
        objGFX.DOLocalJump(pos, jumpPower, 1, jumpTime).SetDelay(initDelay);

        // rotate y
        float currentRotY = Utils.instance.GetRotation360(objGFX.localEulerAngles.y);
        float targetY = currentRotY + 360f;
        DOTween.To(() => currentRotY, y => currentRotY = y, targetY, rotationTime)
            .SetDelay(initDelay + startRotDelay)
            .OnUpdate(() => {
                Vector3 rot = objGFX.localEulerAngles;
                rot.y = currentRotY;
                objGFX.localEulerAngles = rot;
            });
    }

    public void JumpShake(float initDelay = .5f)
    {
        // implement later
        // ...
    }
    
}
