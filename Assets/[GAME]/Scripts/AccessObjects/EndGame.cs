using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagNames.StackComputer.ToString())
        {
            // effect
            GameObject effect = PoolManager.Instance.smokeExplodePool.PullObjFromPool();
            effect.transform.position = other.transform.position + (Vector3.up * 1f);

            // just remove object
            other.gameObject.SetActive(false);

        }
        else if(other.tag == TagNames.PlayerHand.ToString())
        {
            // set position to stop going up
            LevelEndBoxes.instance.AssignStopPoint(MoneyTag.instance.totalStackPrice);

            // end game
            ComputerLevelEnd.instance.Activate(MoneyTag.instance.totalStackPrice);

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
