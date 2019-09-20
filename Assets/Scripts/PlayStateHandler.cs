using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayState
{
    Fight,
    Pause,
    Dead,
}

public class PlayStateHandler : MonoSingleton<PlayStateHandler>
{
    public delegate void Callback();
    private Dictionary<PlayState, List<Callback>> stateChangeCallback = new Dictionary<PlayState, List<Callback>>();


    private PlayState prevPlayState;
    private PlayState curPlayState;
    // Start is called before the first frame update
    void Start()
    {
        prevPlayState = PlayState.Fight;
        curPlayState = PlayState.Fight;
    }

    // Update is called once per frame
    void Update()
    {
        // Game state is changed
        if (prevPlayState != curPlayState)
        {
            List<Callback> callbacks = stateChangeCallback[curPlayState];

            foreach (var callback in callbacks)
            {
                callback();
            }

            prevPlayState = curPlayState;
        }
    }

    // GameManager에서 전투 상황 변경시 호출됩니다.
    public void SetCurrentPlayState(PlayState playState)
    {
        curPlayState = playState;
    }

    // 전투 상황이 변경 될 때에 부를 Callback 함수들을 등록합니다.
    public void SetStateChangeCallback(PlayState playState, Callback function)
    {
        if (stateChangeCallback.ContainsKey(playState))
        {
            stateChangeCallback[playState].Add(function);
        }
        else
        {
            List<Callback> callbackList = new List<Callback>();
            callbackList.Add(function);

            stateChangeCallback.Add(playState, callbackList);
        }
    }
}
