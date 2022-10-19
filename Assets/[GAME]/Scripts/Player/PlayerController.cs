using DG.Tweening;
using Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("User Control")]
    [HideInInspector] public bool userActive;

    private void Start()
    {
        
    }

    void Update()
    {
        if (!userActive) return;

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
