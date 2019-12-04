using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadWindow : MonoBehaviour
{
    public Image background;
    public Text gameover;
    public Button backToStart;

    public Material peng;

    public IEnumerator DeadHandler(Action offEnemies)
    {
        background.gameObject.SetActive(true);
        StartCoroutine(UIEffect.AlphaIn(background));
        while(background.color.a <1.0f)
        {
            peng.color = new Color(1, 1, 1, 1 - background.color.a);
            yield return new WaitForEndOfFrame();
        }

        offEnemies.Invoke();
        string[] deadCullingLayer = new string[] { "UI_Dead", "Player" };
        Camera.main.cullingMask = LayerMask.GetMask(deadCullingLayer);

        ObjectPoolManager.Instance.SetActvieFalseAllPrefabs();
        gameover.gameObject.SetActive(true);
        backToStart.gameObject.SetActive(true);
        backToStart.onClick.AddListener(BackToStartScene);
    }

    private void BackToStartScene()
    {
        GameManager.Instance.LoadNextScene(Constants.FightSceneName, Constants.StartSceneName);
    }
}
