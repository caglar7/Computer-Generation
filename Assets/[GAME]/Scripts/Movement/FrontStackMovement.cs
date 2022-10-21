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
    public Transform target;  // later make it private, visualizing for now

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
        if(gameObject.layer == LayerMask.NameToLayer(LayerMaskName.CollectComputer.ToString()))
        {
            StartFollowing();
        }
    }

    #endregion

    #region Following Stack Methods
    private void StartFollowing()
    {
        // set layer mask
        gameObject.layer = LayerMask.NameToLayer("StackComputer");

        // set follow target and init front stack pos
        target = ComputerStack.instance.GetFrontComputer();
        Vector3 pos = target.position;
        pos.z += zOffset;
        transform.position = pos;

        // add to the stack
        ComputerStack.instance.AddRemoveFromStack(transform, StackAddRemove.Add);

        // anim
        ComputerStack.instance.ScaleAnimation();
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

    private void StopFollowing()
    {
        // disable collider for duration, avoiding unnecessery collisions
        Utils.instance.EnableColliderForDuration(collider, false, .5f);

        // set layer mask
        gameObject.layer = LayerMask.NameToLayer("CollectComputer");

        // remove from stack
        ComputerStack.instance.AddRemoveFromStack(transform, StackAddRemove.Remove);

        // throw it to the front with DoJump
        // ...
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
