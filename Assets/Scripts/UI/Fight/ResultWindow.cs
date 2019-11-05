using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultWindow : MonoBehaviour
{
    private Action callback;

    public GameObject disableTouchArea;
    public Image background;

    public GameObject playerFigure;

    public Text stageName;
    public Slider playerHp;
    public Text getParts;
    public Button nextButton;

    private void Awake()
    {
        nextButton.onClick.AddListener(OnClickNextButton);
    }

    public void ShowResult(Action fucntion)
    {
        callback = fucntion;

        disableTouchArea.SetActive(true);

        StartCoroutine(UIEffect.Expand(background.gameObject));
        // 왼쪽에 player figure on
        // stage name on
        //StartCoroutine(UIEffect.Expand(stageName.gameObject));
        // hp on
        // 얻은 parts on
        // next button on
        nextButton.gameObject.SetActive(true);
    }

    public void OnClickNextButton()
    {
        callback();
    }

}
