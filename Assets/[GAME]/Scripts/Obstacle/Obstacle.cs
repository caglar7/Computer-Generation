using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class Obstacle : MonoBehaviour
{
    [Header("Player Push Back Tween Settings")]
    [SerializeField] float pushDistance = 3f;
    [SerializeField] float pushDuration = 1f;
    [SerializeField] Ease pushEase = Ease.Linear;

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
                player.PushBack(pushDistance, pushDuration, pushEase);
            }
            else // no player component, remove the computer that hit, and throw the ones on front
            {
                ComputerStack.instance.ThrowFrontComputers(other.transform);
            }
        }
    }
}
