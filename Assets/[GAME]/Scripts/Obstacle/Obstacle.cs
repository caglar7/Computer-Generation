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
    // collider computer has player component or not
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagNames.StackComputer.ToString())
        {
            // get components
            PlayerController player = other.GetComponent<PlayerController>();

            // throw computers
            if (player) player.PushBack(pushDistance, pushDuration, pushEase);
            ComputerStack.instance.ThrowFrontComputers(other.transform, player);
        }
    }
}
