using DG.Tweening;
using Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SwipeMecLast
{
    [Header("Forward Movement")]
    [SerializeField] float forwardSpeed;

    [Header("User Control")]
    [HideInInspector] public bool userActive;

    [Header("Player Components")]
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Start()
    {
        base.Start(); // BU SATIRI SAKIN SÝLME ----------------------------
    }


    void Update()
    {
        if (!userActive) return;
        transform.position += forwardSpeed * Time.deltaTime * transform.forward;
        Swipe();
        UIManager.instance.UpdateProgressBar();
    }

    //private void FixedUpdate()
    //{
    //    if (!userActive) return;

    //    rb.velocity = forwardSpeed * Time.deltaTime * transform.forward;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Diamond"))
        {
            other.gameObject.SetActive(false);
            UIManager.instance.DiamondCollectAnim(other.transform.position);
        }
        else if (other.CompareTag("EndGame"))
        {
            GameManager.instance.EndGame(2);
        }
    }

    public void UserActiveController(bool desiredVal)
    {
        userActive = desiredVal;
    }
}
