using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagNames.StackComputer.ToString())
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if(player)
            {
                GameManager.instance.EndGame(2);
            }
        }
    }
}
