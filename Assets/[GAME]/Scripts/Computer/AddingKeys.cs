using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// adding keys from left to right 
// or from right to left

public class AddingKeys : MonoBehaviour
{
    [SerializeField] float period = .1f;
    float timer = 0f;
    int keyIndex = 0;
    int dir = 1;
    List<Transform> listCheck = new List<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagNames.AddingKeyboard.ToString())
        {
            if(other.transform.position.x <= 0f)    // add from right to left
            {
                keyIndex = transform.childCount - 1;
                dir = -1;
            }
            else    // add from left to right
            {
                keyIndex = 0;
                dir = 1;
            }
        }
    }

    // add keys while on robot range
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == TagNames.AddingKeyboard.ToString())
        {
            timer += Time.deltaTime;
            if(timer > period)
            {
                timer = 0f;

                // check
                Transform keyColomn = transform.GetChild(keyIndex);
                if (listCheck.Contains(keyColomn)) return;
                listCheck.Add(keyColomn);
                keyIndex = Mathf.Clamp(keyIndex + dir, 0, transform.childCount - 1);

                // move keys
                StartCoroutine(AddKeysCo(keyColomn));
            }
        }
    }

    IEnumerator AddKeysCo(Transform obj)
    {
        // set start pos
        Vector3 pos = obj.localPosition;
        Vector3 init = obj.localPosition;
        init.z += .025f;
        obj.localPosition = init;
        obj.gameObject.SetActive(true);

        // move it down
        obj.DOLocalMove(pos, .15f).SetEase(Ease.Linear);

        yield return 0;
    }

}
