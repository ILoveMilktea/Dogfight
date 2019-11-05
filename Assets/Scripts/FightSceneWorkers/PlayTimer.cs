using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTimer : MonoBehaviour
{
    private float playtime;
    private bool isFight;
    private IEnumerator timer;

    private void Start()
    {
        playtime = 0f;
        isFight = false;
        timer = Timer();
    }

    public IEnumerator Timer()
    {
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

        DataManager.Instance.AddPlayTime(playtime);
    }

}
