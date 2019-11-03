using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

static class Constants
{
    public const string StartSceneName = "StartScene";
    public const string FightSceneName = "PlayerTestScene"; // "Fight"
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
        FightSceneStart();
    }
    public void ContinueGame()
    {
        DataManager.Instance.Load();
        UpgradeSceneStart();
    }

    public void FightSceneStart()
    {
        SceneManager.LoadScene(Constants.FightSceneName);
    }

    public void UpgradeSceneStart()
    {
        SceneManager.LoadScene(Constants.UpgradeSceneName);
    }
}
