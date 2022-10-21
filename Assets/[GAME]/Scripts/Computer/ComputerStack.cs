using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    #region Stack Methods

    public void AddRemoveFromStack(Transform obj, StackAddRemove mode)
    {
        obj.SetParent((mode == StackAddRemove.Add) ? transform : null);
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