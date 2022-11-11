using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerMesh : MonoBehaviour
{
    [Header("Parts on This Mesh")]
    public List<ComputerPart> parts;

    [Header("Deactivate When Active")]
    [SerializeField] Transform[] deactivateMeshes;

    [Header("Next Possible Meshes")]
    public Transform[] nextMeshes;

    // control field
    ComputerMeshControl parentMeshControl;

    // computer mesh control list control
    private void OnEnable()
    {
        foreach (Transform t in deactivateMeshes)
        {
            t.gameObject.SetActive(false);
        }

        parentMeshControl = transform.parent.parent.parent.GetComponent<ComputerMeshControl>();
        if (parentMeshControl) parentMeshControl.listActiveMeshes.Add(transform);
    }

    private void OnDisable()
    {
        if (parentMeshControl) parentMeshControl.listActiveMeshes.Remove(transform);
    }
}
