using DG.Tweening;
using Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerController : SwipeMecLast
{
    [Header("Settings")]
    [SerializeField] float forwardSpeed = 5f;

    [Header("User Control")]
    [HideInInspector] public bool userActive;

    [Header("Init Fields")]
    float initForwardSpeed = 0f;

    public override void Start()
    {
        base.Start();   // don't delete       

        GetFields();
    }

    void Update()
    {
        if (!userActive) return;
        transform.position += (forwardSpeed * Vector3.forward * Time.deltaTime);
        Swipe();
    }

    #region Trigger Enter
    private void OnTriggerEnter(Collider other)
    {
        // end game call
        //else if (other.CompareTag("EndGame"))
        //{
        //    GameManager.instance.EndGame(2);
        //}
    }
    #endregion

    #region Movement Methods

    // push player back if obstacle hit, adjust settings
    public void PushBack(float dis, float duration, Ease ease)
    {
        SetForwardSpeed(0f);
        float nextZ = transform.position.z - dis;

        transform.DOMoveZ(nextZ, duration)
            .SetEase(ease)
            .OnComplete(() => {
                UserActiveController(true);
                SetForwardSpeed(1f);
            });

    }

    // setting speed based on init forward speed, 0f mult => setting to 0f
    private void SetForwardSpeed(float mult = 1f)
    {
        forwardSpeed = initForwardSpeed * mult;
    }

    #endregion

    #region Set User Active
    public void UserActiveController(bool desiredVal)
    {
        userActive = desiredVal;
    }
    #endregion

    #region Init Methods
    private void GetFields()
    {
        initForwardSpeed = forwardSpeed;
    }

    private void GetComponents()
    {

    } 
    #endregion
}
