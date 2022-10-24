using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerMesh : MonoBehaviour
{
    [SerializeField] Transform[] deactivateMeshes;

    private void Start()
    {
        foreach(Transform t in deactivateMeshes)
        {
            t.gameObject.SetActive(false);
        }
    }
}
