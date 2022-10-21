using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // 2 cases
    // check for player controller component
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagNames.StackComputer.ToString())
        {
            // get components
            PlayerController player = other.GetComponent<PlayerController>();

            // if computer has player component, push back
            if(player)
            {
                
            }
            else // no player component, throw front mechanic
            {

            }
            
        }
    }
}
