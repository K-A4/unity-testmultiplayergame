using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private AnimationCurve tweenEaseFunction;

    public void ChangeBar(float alpha)
    {
        StopAllCoroutines();
        StartCoroutine(TweenHealth(alpha));
    }

    private IEnumerator TweenHealth(float alpha)
    {
        var previousFill = bar.fillAmount;
        var timeElapsed = 0.0f;

        while (timeElapsed <= 1.0f)
        {
            timeElapsed += Time.deltaTime;
            bar.fillAmount = Mathf.Lerp(previousFill, alpha, tweenEaseFunction.Evaluate(timeElapsed));
            Debug.Log(bar.fillAmount);
            yield return null;
        }

        bar.fillAmount = alpha;
    }

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
