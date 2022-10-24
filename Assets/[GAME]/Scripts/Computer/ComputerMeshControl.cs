using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerMeshControl : MonoBehaviour
{
    // follow what meshes object has
    // depending on what components we got
    // => body, keyboard frame, mousepad frame, keyboard, mousepad, screen frame, screen

    public Transform body_Nothing;
    public Transform body_KeyboardFrame;
    public Transform body_MousepadFrame;
    public Transform body_KeyboardFrame_MousepadFrame;
    public Transform body_Keyboard;
    public Transform screen_Frame;
    public Transform screen_Filled;

    // IMPLEMENT
    // methods like this are gonna be used by the robots
    public void AddKeyboardFrame()
    {

    }

    public void AddMousepadFrame()
    {

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
