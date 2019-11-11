using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

static class Constants
{
    public const string StartSceneName = "StartScene";
    public const string FightSceneName = "FightScene"; // "Fight"
    public const string UpgradeSceneName = "Upgrade";

    public const string PlayerTag = "Player";
    public const string EnemyTag = "Enemy";
}

public enum SceneType
{
    Start,
    Fight,
    Upgrade
}

public class GameManager : MonoSingleton<GameManager>
{

    private void Awake()
    {
        
        DontDestroyOnLoad(Instance);

        SceneManager.sceneLoaded += OnLoadCallback;
        OnLoadCallback(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }


    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        switch (scene.name)
        {
            case Constants.StartSceneName:
                break;
            case Constants.FightSceneName:
                //FightSceneController.Instance.StartFightScene();
                break;
            case Constants.UpgradeSceneName:
                break;
        }
    }

    public void NewStartGame()
    {
        DataManager.Instance.Save();
        FightSceneStart();
    }
    public void ContinueGame()
    {
        DataManager.Instance.Load();
        UpgradeSceneStart();
    }

    public void FightSceneStart()
    {
        StartCoroutine(LoadScene(Constants.FightSceneName));
    }

    public void UpgradeSceneStart()
    {
        StartCoroutine(LoadScene(Constants.UpgradeSceneName));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        //float timer = 0.0f;
        while (!operation.isDone)
        {
            yield return new WaitForEndOfFrame();

            if(operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            //timer += Time.deltaTime;
            //if (oper.progress >= 0.9f)
            //{
            //    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
            //    //Image가 아니라 Slider의 경우 progressBar.value로 간단히 구현이 가능합니다만
            //    // 이미지가 찌그러진 것이 펴지는 것처럼 나오기 때문에 비추천하는 방법입니다.

            //    if (progressBar.fillAmount == 1.0f)
            //        oper.allowSceneActivation = true;
            //}
            //else
            //{
            //    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, oper.progress, timer);
            //    if (progressBar.fillAmount >= oper.progress)
            //    {
            //        timer = 0f;
            //    }
            //}
        }
    }
    
}
