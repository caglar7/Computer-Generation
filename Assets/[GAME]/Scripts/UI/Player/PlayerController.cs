using DG.Tweening;
using Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SwipeMecLast
{
    [Header("Settings")]
    [SerializeField] float forwardSpeed = 5f;

    [Header("User Control")]
    [HideInInspector] public bool userActive;

    public override void Start()
    {
        base.Start();   // don't delete       
    }

    void Update()
    {
        if (!userActive) return;
        transform.position += (forwardSpeed * Vector3.forward * Time.deltaTime);
        Swipe();
    }

    //private void FixedUpdate()
    //{
    //    if (!userActive) return;

    //    rb.velocity = forwardSpeed * Time.deltaTime * transform.forward;
    //}

    private void OnTriggerEnter(Collider other)
    {
        // end game call
        //else if (other.CompareTag("EndGame"))
        //{
        //    GameManager.instance.EndGame(2);
        //}
    }

    public void UserActiveController(bool desiredVal)
    {
        userActive = desiredVal;
    }
}
