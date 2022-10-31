using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// mec
// every computer follows the one on the back

// joining the front stack
// on front stack
// exiting front stack when obstacle hit
// and setup to enter again

// change tags when needed
// from collect to stack and vice versaa

public class FrontStackMovement : MonoBehaviour
{
    [HideInInspector] public Transform target;  // later make it private, visualizing for now

    [Header("Settings")]
    float zOffset, updateSpeed;

    [Header("Components")]
    Collider collider;

    #region Start
    private void Start()
    {
        GetSettings();
        GetComponents();
    }
    #endregion

    #region Update
    private void Update()
    {
        FollowStack();
    } 
    #endregion

    #region Trigger Enter

    // collect computer becomes stack computer
    // and stack can becom collect computer as well
    // this switch is controlled with LayerMasks
    // using LayerMask implementation can save us from dealing with many annoying toggles
    private void OnTriggerEnter(Collider other)
    {
        // check to join stack
        if(transform.tag == TagNames.CollectComputer.ToString()
            && other.tag == TagNames.StackComputer.ToString())
        {
            StartFollowing();
        }
    }

    #endregion

    #region Following Stack Methods
    private void StartFollowing()
    {
        // tag adjust
        transform.tag = TagNames.StackComputer.ToString();

        // set follow target
        target = ComputerStack.instance.GetFrontComputer();

        // add to the stack
        ComputerStack.instance.AddRemoveFromStack(transform, StackAddRemove.Add);

        // anim
        ComputerStack.instance.ScaleAnimation();

        // update stack price
        MoneyTag.instance.UpdateStackPrice();
    } 

    private void FollowStack()
    {
        if(target)
        {
            // x lerp
            Vector3 pos = transform.position;
            pos.z = (target.position.z + zOffset);
            pos.x = Mathf.Lerp(pos.x, target.position.x, Time.deltaTime * updateSpeed);
            transform.position = pos;
        }
    }

    public void StopFollowing()
    {
        // tag adjust
        transform.tag = TagNames.CollectComputer.ToString();

        // disable collider for duration, avoiding unnecessery collisions
        Utils.instance.EnableColliderForDuration(collider, false, .5f);

        // clean target
        target = null;

        // remove from stack
        ComputerStack.instance.AddRemoveFromStack(transform, StackAddRemove.Remove);

        // update stack price
        MoneyTag.instance.UpdateStackPrice();
    }

    #endregion

    #region Init
    private void GetSettings()
    {
        zOffset = ComputerStack.instance.stackZOffset;
        updateSpeed = ComputerStack.instance.posUpdateSpeed;
    } 

    private void GetComponents()
    {
        collider = GetComponent<Collider>();
    }
    #endregion
}
