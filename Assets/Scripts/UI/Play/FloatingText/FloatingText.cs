using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloatingText : MonoBehaviour
{
    private RectTransform rectTransform;
    private Text damageText;

    private float upMoveAmount;
    private float downMoveAmount;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        damageText = GetComponent<Text>();

        upMoveAmount = 50;
        downMoveAmount = 20;
    }


    public IEnumerator DisplayDamage(int damage, int xStack, int yStack, Action RemoveFloatingText)
    {
        rectTransform.anchoredPosition += new Vector2(rectTransform.sizeDelta.x * 0.5f * xStack, rectTransform.sizeDelta.y * 0.5f * yStack);
        damageText.text = damage.ToString();
        damageText.enabled = true;

        // Floating Text start floating
        Vector2 startPosition = rectTransform.anchoredPosition;
        Vector2 destination = rectTransform.anchoredPosition + new Vector2(0f, upMoveAmount);

        float timer = 0f;
        while (rectTransform.anchoredPosition.y < destination.y)
        {
            //scale
            rectTransform.localScale = Vector2.Lerp(new Vector2(0f, 0f), new Vector2(1f, 1f), timer);
            //position
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, destination, timer);

            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime * 4;
        }

        // Wait a moment in character's head
        //yield return new WaitForSeconds(0.1f);

        // Sink and destroy floating text
        startPosition = rectTransform.anchoredPosition;
        destination = rectTransform.anchoredPosition + new Vector2(0f, -downMoveAmount);
        timer = 0;

        while (rectTransform.anchoredPosition.y > destination.y)
        {
            //alpha
            damageText.color = Color.Lerp(new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 0f), timer);
            //position
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, destination, timer);

            yield return new WaitForEndOfFrame();

            timer += Time.deltaTime * 2;
        }

        RemoveFloatingText();
        Destroy(gameObject);
    }
}
