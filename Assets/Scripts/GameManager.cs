﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

static class Constants
{
    public const string WorldMapSceneName = "WorldMap";
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
    public Material spriteCutOut;
    
    private bool IsUIEffectEnd;

    protected override void Init()
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
                //IsUIEffectEnd = false;
                //StartCoroutine(UIEffect.CutIn(spriteCutOut));
                break;
            case Constants.WorldMapSceneName:
                IsUIEffectEnd = false;
                StartCoroutine(UIEffect.CutIn(spriteCutOut));
                break;
            case Constants.FightSceneName:
                //FightSceneController.Instance.StartFightScene();
                break;
            case Constants.UpgradeSceneName:
                break;
        }
    }
    
    public void EndUIEffect()
    {
        IsUIEffectEnd = true;
    }

    public void NewStartGame()
    {
        DataManager.Instance.RemoveSaveData();
        SceneStart(Constants.WorldMapSceneName, true);
    }
    public void ContinueGame()
    {
        DataManager.Instance.Load();
        SceneStart(Constants.UpgradeSceneName);
    }

    public void SceneStart(string sceneName)
    {
        IsUIEffectEnd = true;
        StartCoroutine(LoadScene(sceneName));
    }
    public void SceneStart(string sceneName, bool cutoutEffect)
    {
        if(cutoutEffect)
        {
            IsUIEffectEnd = false;
            SceneEndCallback(SceneManager.GetActiveScene().name);
        }
        StartCoroutine(LoadScene(sceneName));
    }

    private void SceneEndCallback(string sceneName)
    {
        switch (sceneName)
        {
            case Constants.StartSceneName:
                StartCoroutine(UIEffect.CutOut(spriteCutOut));
                break;
            case Constants.WorldMapSceneName:
                StartCoroutine(UIEffect.CutOut(spriteCutOut));
                break;
            case Constants.FightSceneName:
                //FightSceneController.Instance.StartFightScene();
                break;
            case Constants.UpgradeSceneName:
                break;
        }
    }

    private IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0.0f;
        while (!operation.isDone)
        {
            yield return new WaitForEndOfFrame();

            if(operation.progress >= 0.9f && IsUIEffectEnd && timer > 2f)
            {
                operation.allowSceneActivation = true;
            }
            timer += Time.deltaTime;
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

    private void OnDestroy()
    {
        spriteCutOut.SetFloat("_Radius", spriteCutOut.GetFloat("_DefaultRadius"));
    }
}
