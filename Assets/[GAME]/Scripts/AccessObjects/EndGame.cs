using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagNames.StackComputer.ToString())
        {
            // just remove object
            other.gameObject.SetActive(false);

        }
        else if(other.tag == TagNames.PlayerHand.ToString())
        {
            // end game
            ComputerLevelEnd.instance.Activate();

            // remove hand
            StartCoroutine(RemovePlayerHand(other.transform, .2f));
        }
    }

    IEnumerator RemovePlayerHand(Transform obj, float delay)
    {
        yield return new WaitForSeconds(delay);

        obj.gameObject.SetActive(false);
    }
}
