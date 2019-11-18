using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusWindow : MonoBehaviour
{
    public Text dungeonName;
    public Text hp;
    public Text atk;
    public Text parts;
    public Text time;

    public Text prevStage;
    public Text nextStage;

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
        dungeonName.text = DataManager.Instance.GetPlayInfo.curDungeon;
        hp.text = DataManager.Instance.GetPlayerStatus.remainHp.ToString() + "/" + DataManager.Instance.GetPlayerStatus.maxHp.ToString();
        atk.text = DataManager.Instance.GetPlayerStatus.atk.ToString();
        parts.text = DataManager.Instance.GetPlayInfo.parts.ToString();
        time.text = DataManager.Instance.GetPlayInfo.playtime.ToString("00:00");
        
        prevStage.text = "Stage " + DataManager.Instance.GetPlayInfo.stage.ToString();
        nextStage.text = "Stage " + (DataManager.Instance.GetPlayInfo.stage + 1).ToString();


        //w1.text = "w1 : " + DataManager.Instance.GetWeapons;

    }

    public void RedrawWindow()
    {
        SetWindow();
    }
}
