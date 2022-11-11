using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StyleControl : MonoBehaviour
{
    public Transform[] styleMeshes;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // disable mesh render of main obj, show one of the style mesh on child
    public void AddRandomStyle()
    {
        if (styleMeshes.Length == 0) return;

        int index = Random.Range(0, styleMeshes.Length);
        meshRenderer.enabled = false;
        transform.GetChild(index).gameObject.SetActive(true);
    }
}
