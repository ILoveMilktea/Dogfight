using System.Collections;
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

    public void StageStart(int stageNumber)
    {
        StartCoroutine(StandbyPhase());
    }

    private IEnumerator StandbyPhase()
    {
        // 맵한번 둘러보는 연출??

        float temptimer = 0;
        while(temptimer < 1f)
        {
            temptimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(FightPhase());
    }

    private IEnumerator FightPhase()
    {
        while(FightSceneController.Instance.RemainEnemyNumber() > 0)
        {
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(EndPhase());
    }

    private IEnumerator EndPhase()
    {
        // 결과창 띄우기??

        float temptimer = 0;
        while (temptimer < 1f)
        {
            temptimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        GameManager.Instance.UpgradeSceneStart();
    }
}
