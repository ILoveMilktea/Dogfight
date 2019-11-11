using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusWindow : MonoBehaviour
{
    public Text stage;
    public Text time;
    public Text parts;
    public Text hp;
    public Text atk;
    public Text w1;
    public Text w2;
    public Text w3;

    // Start is called before the first frame update
    void Start()
    {
        SetWindow();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha8))
        {
            SetWindow();
        }
    }

    private void SetWindow()
    {
        stage.text = "Stage : " + DataManager.Instance.GetPlayInfo.stage.ToString();
        time.text = "Time : " + DataManager.Instance.GetPlayInfo.playtime.ToString("00:00");
        parts.text = "Parts : " + DataManager.Instance.GetPlayInfo.parts.ToString();

        hp.text = "HP : " + DataManager.Instance.GetPlayerStatus.remainHp.ToString() + "/" + DataManager.Instance.GetPlayerStatus.maxHp.ToString();
        atk.text = "ATK : " + DataManager.Instance.GetPlayerStatus.atk.ToString();

        //w1.text = "w1 : " + DataManager.Instance.GetWeapons;

    }

    public void RedrawWindow()
    {
        SetWindow();
    }
}
