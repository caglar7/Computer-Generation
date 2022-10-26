using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MoneyAdded : MonoBehaviour
{
    TextMeshPro text;
    Vector3 initScale;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
        transform.eulerAngles = Camera.main.transform.eulerAngles;
        initScale = transform.localScale;
        Hide();

        // adjust init position here depending on robot arm pos
        // ... or just write a public method and call it from robot on init
        // left up or right up pos should be dynamically set
    }

    public void Show(float duration = 0.5f)
    {
        Color next = text.color;
        next.a = 1f;
        text.DOColor(next, duration);
    }

    public void Hide(float duration = 0.5f)
    {
        Color next = text.color;
        next.a = 0f;
        text.DOColor(next, duration);
    }

    public void ScaleAnimation(float mult, float duration = 0.5f)
    {
        transform.DOKill(); // gonna check
        transform.DOScale(initScale * mult, duration / 2f)
            .SetEase(Ease.Linear)
            .OnComplete(() => {

                transform.DOScale(initScale, duration / 2f)
                .SetEase(Ease.Linear);

            });
    }
}
