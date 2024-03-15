using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class FadeSystem : MonoBehaviour
{
    [SerializeField] private Image fade;
    [SerializeField] private float timer = 1.0f;
    private Coroutine fadeRoutine;
    private Action onFadeEnd;

    public void ToggleFade(bool toggle)
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(Fade(toggle));
    }

    public void ToggleFade(bool toggle, Action onFadeEnd)
    {
        this.onFadeEnd = onFadeEnd;
        ToggleFade(toggle);
    }

    private IEnumerator Fade(bool toggle)
    {
        float elapsed = 0.0f;

        while (elapsed < timer)
        {
            elapsed = Mathf.Min(elapsed + Time.deltaTime, timer);

            float factor = toggle ? elapsed / timer : 1 - elapsed / timer;
            factor = Easing.InOutSine(factor);

            Color tempColor = fade.color;
            tempColor.a = factor;
            fade.color = tempColor;

            yield return null;
        }

        onFadeEnd?.Invoke();
    }
}
