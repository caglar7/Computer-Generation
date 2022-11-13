using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StyleControl : MonoBehaviour
{
    public Transform[] styleMeshes;
    [SerializeField] MeshRenderer meshRenderer;

    // disable mesh render of main obj, show one of the style mesh on child
    public void AddStyle(int styleIndex)
    {
        if (styleMeshes.Length == 0) return;

        meshRenderer.enabled = false;
        styleMeshes[styleIndex].gameObject.SetActive(true);
    }
}
