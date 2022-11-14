using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour
{
    public float speedMult = 1.5f;
    bool isConsumed = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagNames.PlayerHand.ToString())
        {
            if (isConsumed) return;
            isConsumed = true;

            other.GetComponent<PlayerController>().forwardSpeed *= speedMult;
        }
    }
}
