using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

static class Constants
{
    public const string StartSceneName = "Start";
    public const string FightSceneName = "Fight";
    public const string UpgradeSceneName = "Upgrade";
}

public enum SceneType
{
    Start,
    Fight,
    Upgrade
}

public class GameManager : MonoSingleton<GameManager>
{
    private FightSceneHandler fightSceneHandler;

    private void Awake()
    {
        DontDestroyOnLoad(Instance);

        SceneManager.sceneLoaded += OnLoadCallback;
    }


    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        switch (scene.name)
        {
            case Constants.StartSceneName:
                break;
            case Constants.FightSceneName:
                //SetFightSceneMember();
                break;
            case Constants.UpgradeSceneName:
                break;
        }
    }

    // 플레이어 이동
    public void MovePlayer(Vector3 dir, float amount)
    {
        fightSceneHandler.MovePlayer(dir, amount);
    }
    // 플레이어 멈춤
    public void StopPlayer()
    {
        fightSceneHandler.StopPlayer();
    }
    // 플레이어 공격
    public void PlayerAttack(Vector3 dir)
    {
        fightSceneHandler.PlayerAttack(dir);
    }
    // 플레이어 공격 대기
    public void PlayerStandby()
    {
        fightSceneHandler.PlayerStandby();
    }


    //damage test, 나중에 삭제
    public void DamageToEnemy(GameObject target, float damage)
    {
        fightSceneHandler.DamageToCharacter(target, (int)damage);
    }
}
