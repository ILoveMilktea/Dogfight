﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightScheduler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StageStart()
    {
        StartCoroutine(StandbyPhase());
    }

    private IEnumerator StandbyPhase()
    {
        // 맵한번 둘러보는 연출??
        //FightSceneController.Instance.standbyImage.SetActive(true);

        float timer = 0f;
        while(timer < 1.5f)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }

        //FightSceneController.Instance.standbyImage.SetActive(false);
        StartCoroutine(FightPhase());
        //FightSceneController.Instance.ChangeFightState(FightState.Fight);
    }

    private IEnumerator FightPhase()
    {
        FightSceneController.Instance.ChangeFightState(FightState.Fight);
        while(FightSceneController.Instance.RemainEnemyNumber() > 0)
        {

            yield return new WaitForEndOfFrame();
        }

        FightSceneController.Instance.ChangeFightState(FightState.Clear);

        if (DataManager.Instance.GetPlayInfo.Stage == 5)
        {
            // 보스전
            FightSceneController.Instance.DungeonClear();
        }
    }
}
