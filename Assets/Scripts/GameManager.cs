using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoSingleton<GameManager>
{
    private PlayStateHandler playStateHandler;
    private Player player;
    private PlayState playState = PlayState.Fight;

    private void Awake()
    {
        DontDestroyOnLoad(Instance);

        playStateHandler = PlayStateHandler.Instance;
        player = FindObjectOfType<Player>();
    }

    // 현재 업데이트 내 키입력들은 모두 테스트용임니다. 지워야함
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(playState == PlayState.Pause)
            {
                playState = PlayState.Fight;
                playStateHandler.SetCurrentPlayState(playState);

            }
            else
            {
                playState = PlayState.Pause;
                playStateHandler.SetCurrentPlayState(playState);
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

    public void MovePlayer(Vector3 dir, float amount)
    {
        if(playState == PlayState.Fight)
        {
            player.Move(dir, amount);
        }
    }
    public void StopPlayer()
    {
        if (playState == PlayState.Fight)
        {
            player.StopMove();
        }
    }

    public void PlayerAttack(Vector3 dir)
    {
        if (playState == PlayState.Fight)
        {
            player.Attack(dir);
        }
    }

    public void PlayerStandby()
    {
        if (playState == PlayState.Fight)
        {
            player.Standby();
        }
    }

    public void SetStateChangeCallback(PlayState playState, Action func)
    {
        playStateHandler.SetStateChangeCallback(playState, func);
    }
}
