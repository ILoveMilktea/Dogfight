using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadWindow : MonoBehaviour
{
    public Image background;
    public Text gameover;
    public Button backToStart;

    public IEnumerator DeadHandler()
    {
        background.gameObject.SetActive(true);
        StartCoroutine(UIEffect.AlphaIn(background, null));
        while(background.color.a <1.0f)
        {
            yield return new WaitForEndOfFrame();
        }

        gameover.gameObject.SetActive(true);
        backToStart.gameObject.SetActive(true);
        backToStart.onClick.AddListener(BackToStartScene);
    }

    private void BackToStartScene()
    {
        GameManager.Instance.LoadNextScene(Constants.FightSceneName, Constants.StartSceneName);
    }
}
