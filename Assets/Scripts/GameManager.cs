using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoSingleton<GameManager>
{
    public PlayStateHandler playStateHandler;
    public IPlayer playerCtrl;

    private void Awake()
    {
        DontDestroyOnLoad(Instance);

        playStateHandler = PlayStateHandler.Instance;
        playerCtrl = PlayerCtrl.Instance;
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    private PlayState test;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(test == PlayState.Pause)
            {
                test = PlayState.Fight;
                playStateHandler.SetCurrentPlayState(test);

            }
            else
            {
                test = PlayState.Pause;
                playStateHandler.SetCurrentPlayState(test);
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
}
