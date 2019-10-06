using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTimer : MonoBehaviour
{
    private float playtime;
    private bool isFight;
    private IEnumerator timer;

    public PlayTimer()
    {
        playtime = 0f;
        isFight = false;
        timer = Timer();

        //DontDestroyOnLoad(gameObject);
    }

    public IEnumerator Timer()
    {
        //GameManager.Instance.SetStateChangeCallback(PlayState.NonFight, FreezeTimer);
        GameManager.Instance.SetStateChangeCallback(PlayState.Pause, FreezeTimer);
        GameManager.Instance.SetStateChangeCallback(PlayState.Dead, FreezeTimer);
        GameManager.Instance.SetStateChangeCallback(PlayState.Fight, ReleaseTimer);
        isFight = false;

        while(true)
        {
            yield return new WaitForEndOfFrame();

            if(isFight)
            {
                playtime += Time.deltaTime;
            }
        }
    }

    public void FreezeTimer()
    {
        isFight = false;
    }

    public void ReleaseTimer()
    {
        isFight = true;
    }

    public float GetPlaytime()
    {
        return playtime;
    }

    public void ResetPlaytime()
    {
        playtime = 0f;
    }
    public void StartTimer()
    {
        StartCoroutine(timer);
    }
    public void StopTimer()
    {
        StopCoroutine(timer);

        //GameManager.Instance.RemoveStateChangeCallback(PlayState.NonFight, FreezeTimer);
        GameManager.Instance.RemoveStateChangeCallback(PlayState.Pause, FreezeTimer);
        GameManager.Instance.RemoveStateChangeCallback(PlayState.Dead, FreezeTimer);
        GameManager.Instance.RemoveStateChangeCallback(PlayState.Fight, ReleaseTimer);
    }

}
