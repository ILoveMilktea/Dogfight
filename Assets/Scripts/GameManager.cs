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
    private PlayStateObserver playStateObserver;
    private PlayTimer playTimer;
    private Player player;
    private PlayState playState = PlayState.Fight;
    public PlayState CurrentPlayState { get { return playState; } }

    private void Awake()
    {
        DontDestroyOnLoad(Instance);

        playTimer = new PlayTimer();

        SceneManager.sceneLoaded += OnLoadCallback;
        SetFightSceneMember(); // 이거 테스트, 나중에 지워주세요
    }

    // 현재 업데이트 내 키입력들은 모두 테스트용임니다. 지워야함
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(playState == PlayState.Pause)
            {
                playState = PlayState.Fight;
                playStateObserver.SetCurrentPlayState(playState);

            }
            else
            {
                playState = PlayState.Pause;
                playStateObserver.SetCurrentPlayState(playState);
            }
        }

        // save load test
        if(Input.GetKeyUp(KeyCode.S))
        {
            DataManager.Instance.Save();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            DataManager.Instance.Load();
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            DataManager.Instance.levelup(1);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            DataManager.Instance.levelup(2);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            DataManager.Instance.levelup(3);
        }
    }

    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        switch (scene.name)
        {
            case Constants.StartSceneName:
                break;
            case Constants.FightSceneName:
                SetFightSceneMember();
                break;
            case Constants.UpgradeSceneName:
                break;
        }
    }

    private void SetFightSceneMember()
    {
        playStateObserver = FindObjectOfType<PlayStateObserver>();
        player = FindObjectOfType<Player>();
    }

    // 플레이어 이동
    public void MovePlayer(Vector3 dir, float amount)
    {
        if(playState == PlayState.Fight)
        {
            player.Move(dir, amount);
        }
    }
    // 플레이어 멈춤
    public void StopPlayer()
    {
        if (playState == PlayState.Fight)
        {
            player.StopMove();
        }
    }
    // 플레이어 공격
    public void PlayerAttack(Vector3 dir)
    {
        if (playState == PlayState.Fight)
        {
            player.Attack(dir);
        }
    }
    // 플레이어 공격 대기
    public void PlayerStandby()
    {
        if (playState == PlayState.Fight)
        {
            player.Standby();
        }
    }

    // play state change
    public void ChangePlayState()
    {
        if (playState == PlayState.Pause)
        {
            playState = PlayState.Fight;
            playStateObserver.SetCurrentPlayState(playState);

        }
        else
        {
            playState = PlayState.Pause;
            playStateObserver.SetCurrentPlayState(playState);
        }
    }
    // 전투 중 일시정지시에 처리할 콜백함수들 등록
    public void SetStateChangeCallback(PlayState playState, Action func)
    {
        playStateObserver.SetStateChangeCallback(playState, func);
    }
    public void RemoveStateChangeCallback(PlayState playState, Action func)
    {
        playStateObserver.RemoveStateChangeCallback(playState, func);
    }
}
