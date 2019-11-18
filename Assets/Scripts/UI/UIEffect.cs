using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UIEffect
{

    public static IEnumerator Expand(GameObject target)
    {
        Vector3 originalScale = target.transform.localScale;
        target.transform.localScale = Vector3.zero;

        if (target.gameObject.activeSelf == false)
        {
            target.gameObject.SetActive(true);
        }

        float timer = 0f;
        while (timer < 1.0f)
        {
            target.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, timer);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime * 2;
        }

        target.transform.localScale = originalScale;
    }

    public static IEnumerator FadeOut(Image target)
    {
        Color originalColor = target.color;
        Color alpha0 = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        target.color = alpha0;

        if (target.gameObject.activeSelf == false)
        {
            target.gameObject.SetActive(true);
        }

        float timer = 0f;
        while (timer < 1.0f)
        {
            target.color = Color.Lerp(alpha0, originalColor, timer);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }

        target.color = originalColor;
    }

    public static IEnumerator FadeIn(Image target)
    {
        Color originalColor = target.color;
        Color alpha0 = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        float timer = 0f;
        while (timer < 1.0f)
        {
            target.color = Color.Lerp(originalColor, alpha0, timer);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }

        target.gameObject.SetActive(false);
        target.color = originalColor;
    }

    public static IEnumerator CutOut(Material target)
    {
        float startRadius = target.GetFloat("_DefaultRadius");
        float lerpRadius = startRadius;

        float timer = 0f;
        while(lerpRadius > 0)
        {
            lerpRadius = Mathf.Lerp(startRadius, 0, timer);
            target.SetFloat("_Radius", lerpRadius);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }

        target.SetFloat("_Radius", 0);
        GameManager.Instance.EndUIEffect();
    }

    public static IEnumerator CutIn(Material target)
    {
        float destinationRadius = target.GetFloat("_DefaultRadius");
        float lerpRadius = 0;

        float timer = 0f;
        while (lerpRadius < destinationRadius)
        {
            lerpRadius = Mathf.Lerp(0, destinationRadius, timer);
            target.SetFloat("_Radius", lerpRadius);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        target.SetFloat("_Radius", destinationRadius);
        GameManager.Instance.EndUIEffect();
    }
}
