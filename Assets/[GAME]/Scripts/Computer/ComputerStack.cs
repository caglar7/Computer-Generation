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
    public float stackZOffset = 2f;     // diff between computers on front stack
    public float posUpdateSpeed = 5f;   // on x
    public float animScaleMult = 2f;    // scaling animations when stacking
    Vector3 initScale;

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

    #endregion

    #region Stack Animations

    // animate scaling, also to avoid bugs, make sure each iteration checks for childCount
    // if an object gone missing while animating
    public void ScaleAnimation(float animTime = .4f, float transitionTime = .1f, Ease ease = Ease.Linear)
    {
        StartCoroutine(ScaleAnimationCo(animTime, transitionTime, ease));
    }

    IEnumerator ScaleAnimationCo(float animTime, float transitionTime, Ease ease)
    {
        int count = transform.childCount;
        for (int i = count-1; i >= 0; i--)
        {
            if(i < transform.childCount)
            {
                // anim with tween
                Transform obj = transform.GetChild(i).GetComponent<Computer>().objGFX;
                obj.DOKill();
                obj.DOScale(initScale * animScaleMult, animTime / 3f).SetEase(ease)
                    .OnComplete(() => {

                        obj.DOScale(initScale, animTime * 2f / 3f).SetEase(ease);

                    });

                // transition delay
                yield return new WaitForSeconds(transitionTime);

                // tsetin
                print("animating");
                print("scale target: " + initScale * animScaleMult);

            }
        }
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

    #endregion

    #region Init

    private void GetFields()
    {
        // try getting from player computer that also has a computer component
        initScale = GetComponentInChildren<Computer>().transform.localScale;
    }

    #endregion
}


// UPDATE ENUMS
// layer mask names for triggers and collision
public enum LayerMaskName
{
    StackComputer,
    CollectComputer,
}

public enum StackAddRemove
{
    Add,
    Remove,
}