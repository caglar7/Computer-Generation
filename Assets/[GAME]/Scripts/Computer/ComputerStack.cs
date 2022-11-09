using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// this will handle stacking
// holds data about computers

public class ComputerStack : MonoBehaviour
{
    #region Singleton
    public static ComputerStack instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
    #endregion

    [Header("Front Stack Settings")]    // other components get this data in their starts
    public float stackZOffset_min = 2f;     // diff between computers on front stack
    public float stackZOffset_vary = .5f;
    public float posUpdateSpeed = 5f;   // on x
    public float animScaleMult = 2f;    // scaling animations when stacking
    public bool animOn = false;

    [Header("Obstacle Throwing Computers Settings")]
    [SerializeField] float throwJumpDistanceZ = 5f;
    [SerializeField] float throwJumpVaryZ = 1f;     // value in range => throwDistanceZ + (-1f to +1f)
    [SerializeField] float throwJumpVaryX = 2f;     // value in range => -2f to +2f
    [SerializeField] float throwJumpTime = 1f;
    [SerializeField] float throwJumpPower = 1f;
    [SerializeField] int throwJumpCount = 2;
    [SerializeField] Ease throwJumpEase = Ease.Linear;

    [Header("Gate Settings")]
    public float enlargeMult = 1.2f;

    #region Start
    private void Start()
    {
        GetFields();
    } 
    #endregion

    #region Stack Methods

    public void AddRemoveFromStack(Transform obj, StackAddRemove mode)
    {
        obj.SetParent((mode == StackAddRemove.Add) ? transform : null);
    }

    public float GetZOffset()
    {
        float res = stackZOffset_min;
        for (int i = 0; i < transform.childCount; i++)
        {
            res += stackZOffset_vary;
        }
        return res;
    }

    // lose computer on front when hit
    public void ThrowFrontComputers(Transform hitComputer, PlayerController player)
    {
        // remove the hit computer, later with effect
        if(player == null) Destroy(hitComputer.gameObject);

        // get list of computer to throw, excluding player
        float zRef = hitComputer.position.z;
        List<Transform> listThrow = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<PlayerController>()) continue;

            float z = transform.GetChild(i).position.z;
            if(z > zRef)
            {
                listThrow.Add(transform.GetChild(i));
            }
        }

        // throw items
        foreach (Transform t in listThrow)
        {
            // find throw pos
            float throwDis = Random.Range(throwJumpDistanceZ - throwJumpVaryZ, throwJumpDistanceZ + throwJumpVaryZ);
            Vector3 next = t.position;
            next.x = Random.Range(-throwJumpVaryX, throwJumpVaryX);
            next.z = t.position.z + throwDis;

            // stop front stack follow
            t.GetComponent<FrontStackMovement>().StopFollowing();

            // set back to collectable parent
            t.SetParent(CollectablesParent.instance.transform);

            // tween do jump
            t.DOJump(next, throwJumpPower, throwJumpCount, throwJumpTime)
                .SetEase(throwJumpEase);
        }
    }

    #endregion

    #region Stack Animations

    // animate scaling, also to avoid bugs, make sure each iteration checks for childCount
    // if an object gone missing while animating
    bool canAnimate = true;
    public void ScaleAnimation(float animTime = .45f, float transitionTime = .2f, Ease ease = Ease.Linear)
    {
        if (!animOn) return;
        if (!canAnimate) return;
        canAnimate = false;
    
        StartCoroutine(ScaleAnimationCo(animTime, transitionTime, ease));
    }

    IEnumerator ScaleAnimationCo(float animTime, float transitionTime, Ease ease)
    {
        int count = transform.childCount;
        for (int i = count-1; i >= 0; i--)
        {
            if(i < transform.childCount)
            {
                // anim 
                ComputerAnimations anim = transform.GetChild(i).GetComponent<ComputerAnimations>();
                anim.Scaling(animScaleMult, animTime, ease);

                // transition delay
                yield return new WaitForSeconds(transitionTime);
            }
        }

        canAnimate = true;
    }

    #endregion

    #region Get Methods
    // if in any case we rotate the front stack
    // this mec might cause problems
    // might consider this in future, but it's a low possibility
    public Transform GetFrontComputer()
    {
        int count = transform.childCount;
        if (count == 0) return null;

        float maxZ = transform.GetChild(0).position.z;
        int index = 0;
        for (int i = 0; i < count; i++)
        {
            float tempZ = transform.GetChild(i).position.z;
            if(tempZ > maxZ)
            {
                maxZ = tempZ;
                index = i;
            }
        }
        return transform.GetChild(index);
    }

    public int GetTotalStackPrice()
    {
        int value = 0;
        foreach(Computer c in transform.GetComponentsInChildren<Computer>())
        {
            value += c.price;
        }
        return value;
    }

    #endregion

    #region Init

    private void GetFields()
    {

    }

    #endregion
}


// UPDATE ENUMS
// layer mask names for triggers and collision
public enum TagNames
{
    StackComputer,
    CollectComputer,
    AddingKeyboard,
    PlayerHand,
}

public enum StackAddRemove
{
    Add,
    Remove,
}

public enum ComputerAnimationType
{
    Scaling,
    JumpRotate,
}