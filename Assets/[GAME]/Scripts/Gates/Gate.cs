using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] Transform objIcon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagNames.StackComputer.ToString())
        {
            objIcon.gameObject.SetActive(false);

            SellComputer(other.GetComponent<Computer>(), .3f);
        }
    }

    private void SellComputer(Computer c, float delay)
    {
        if (c == null) return;

        StartCoroutine(SellComputerCo(c, delay));
    }

    IEnumerator SellComputerCo(Computer c, float delay)
    {
        yield return new WaitForSeconds(delay);

        // selling effect, or effect combinations
        // ...

        // update UI 
        TotalMoneyUI.instance.UpdateRevenue(c.price);

        // remove computer
        Destroy(c.gameObject);
    }
}
