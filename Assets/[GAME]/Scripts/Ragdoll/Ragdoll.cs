using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public enum RagdollType
    {
        WholeBody,
        Partial
    }

    public RagdollType ragdollType;

    [Header("Parts affected by ragdoll")]
    [SerializeField] bool headRagdoll;
    [SerializeField] bool spineRagdoll;
    [SerializeField] bool hipsRagdoll;
    [SerializeField] bool armsRagdoll;
    [SerializeField] bool legsRagdoll;


    [Header("General parts")]
    [SerializeField] Rigidbody hips;
    [SerializeField] Rigidbody spine;
    [SerializeField] public Rigidbody head;

    [Header("Arms")]
    [SerializeField] Rigidbody armLeftUpper;
    [SerializeField] Rigidbody armLeftLower;
    [SerializeField] Rigidbody armRightUpper;
    [SerializeField] Rigidbody armRightLower;

    [Header("Legs")]
    [SerializeField] Rigidbody legLeftUpper;
    [SerializeField] Rigidbody legLeftLower;
    [SerializeField] Rigidbody legRightUpper;
    [SerializeField] Rigidbody legRightLower;



    [Header("Sub-Components")]
    Collider[] subCollider;
    Rigidbody[] subRigidbody;

    [Header("Main Components")]
    Animator animatorOfTransform;
    Rigidbody rigidbodyOfTransform;
    Collider mainColliderOfTransform;

    [Header("Spines-Neck-Head")]
    [SerializeField] Transform[] spinesAndNeck;

    [Header("Needed Variables")]
    bool freeze;

    //private void FixedUpdate()
    //{
    //    if (freeze)
    //    {
    //        head.position = obj.position;
    //        head.velocity = Vector3.zero;
    //    }
    //}

    public void SetUp(Transform ragdollParent, Animator anim, Rigidbody rbTransform, Collider colliderMain)
    {
        animatorOfTransform = anim;
        rigidbodyOfTransform = rbTransform;
        mainColliderOfTransform = colliderMain;

        subCollider = ragdollParent.GetComponentsInChildren<Collider>(true);
        subRigidbody = ragdollParent.GetComponentsInChildren<Rigidbody>(true);

        WholeBodyRagdoll(false);
    }

    public void RagdollController(bool ragdollState)
    {
        switch (ragdollType)
        {
            case RagdollType.WholeBody:
                WholeBodyRagdoll(ragdollState);
                break;

            case RagdollType.Partial:
                PartialRagdoll(ragdollState);
                break;
        }
    }


    public void WholeBodyRagdoll(bool ragdollState)
    {
        foreach (Collider collider in subCollider)
        {
            collider.enabled = ragdollState;
        }

        foreach (Rigidbody npcRB in subRigidbody)
        {
            npcRB.isKinematic = !ragdollState;
        }

        rigidbodyOfTransform.isKinematic = ragdollState; // main rigidbody on player

        mainColliderOfTransform.enabled = !ragdollState; // main collider on player
        animatorOfTransform.enabled = !ragdollState; //
    }

    void PartialRagdoll(bool ragdollState)
    {
        freeze = true;

        if (ragdollState)
        {
            hips.transform.localEulerAngles = Vector3.right * 180;
            spine.transform.localEulerAngles = Vector3.zero;
            head.transform.localEulerAngles = Vector3.zero;
        }
       


        if (headRagdoll)
        {
            head.isKinematic = !ragdollState;
            head.GetComponent<Collider>().enabled = ragdollState;
        }

        if (hipsRagdoll)
        {
            hips.isKinematic = !ragdollState;
            hips.transform.GetComponent<Collider>().enabled = ragdollState;
        }

        if (spineRagdoll)
        {
            spine.isKinematic = !ragdollState;
            spine.GetComponent<Collider>().enabled = ragdollState;
        }

        if (legsRagdoll)
        {
            legLeftUpper.isKinematic = !ragdollState;
            legLeftLower.isKinematic = !ragdollState;
            legRightUpper.isKinematic = !ragdollState;
            legRightLower.isKinematic = !ragdollState;

            legLeftUpper.GetComponent<Collider>().enabled = ragdollState;
            legLeftLower.GetComponent<Collider>().enabled = ragdollState;
            legRightUpper.GetComponent<Collider>().enabled = ragdollState;
            legRightLower.GetComponent<Collider>().enabled = ragdollState;
        }

        if (armsRagdoll)
        {
            armLeftUpper.isKinematic = !ragdollState;
            armLeftLower.isKinematic = !ragdollState;
            armRightUpper.isKinematic = !ragdollState;
            armRightLower.isKinematic = !ragdollState;

            armLeftUpper.GetComponent<Collider>().enabled = ragdollState;
            armLeftLower.GetComponent<Collider>().enabled = ragdollState;
            armRightUpper.GetComponent<Collider>().enabled = ragdollState;
            armRightLower.GetComponent<Collider>().enabled = ragdollState;
        }



        rigidbodyOfTransform.isKinematic = ragdollState; // main rigidbody on player

        mainColliderOfTransform.isTrigger = ragdollState; // main collider on player
        animatorOfTransform.enabled = !ragdollState; //

        LookForward();

    }

    public void AddForceToRagdoll()
    {
        foreach (Rigidbody npcRB in subRigidbody)
        {
            npcRB.AddForce(new Vector3(0,.1f,1) * 50, ForceMode.Impulse);
        }
    }

    public void LookForward()
    {
        for (int i = 0; i < spinesAndNeck.Length; i++)
        {
            spinesAndNeck[i].localEulerAngles = Vector3.zero;
        }
    }
}