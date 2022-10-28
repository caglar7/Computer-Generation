using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExitCheck : MonoBehaviour
{
    [SerializeField] MoneyAdded moneyAdded;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == TagNames.StackComputer.ToString())
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if(player != null) moneyAdded.Hide();
        }
    }
}
