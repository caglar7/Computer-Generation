using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerMeshControl : MonoBehaviour
{
    [HideInInspector] public List<Transform> listActiveMeshes;

    [Header("Fields")]
    public Transform obj_BodyParts;
    public Transform obj_ScreenParts;

    public void AddComputerPart(ComputerPart part)
    {
        // get next meshes
        List<Transform> nextMeshes = new List<Transform>();
        foreach(Transform t1 in listActiveMeshes)
        {
            nextMeshes.AddRange(t1.GetComponent<ComputerMesh>().nextMeshes);
        }

        // add the part if next mesh has the part
        foreach(Transform t2 in nextMeshes)
        {
            if(t2.GetComponent<ComputerMesh>().parts.Contains(part))
            {
                t2.gameObject.SetActive(true);
                break;
            }
        }
    }
}

//// this gonna change when there are blendshapes
//public enum ComputerMeshType
//{
//    Body_Nothing,
//    Body_KeyboardFrame,
//    Body_MousepadFrame,
//    Body_KeyboardFrame_MousepadFrame,
//    Screen,
//}

public enum ComputerPart
{
    Body,
    Keyboard_Frame,
    Mousepad_Frame,
    Keyboard,
    Mousepad,
    Screen_Frame,
    Screen,
    Graphic_Card,
    Ram,
    Camera,
    Protection_Sleeve,
    Screen_Larger,
    Style,
}
