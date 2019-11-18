using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapController : MonoSingleton<WorldMapController>
{
    public Button Dungeon_Griffon;
    public Button Dungeon_2;

    public PopupYN popupYN;

    private void Awake()
    {
        Dungeon_Griffon.onClick.AddListener(OnClickDungeon_Griffon);
        Dungeon_2.interactable = false;

        AwakeAllUIScript();
    }

    private void AwakeAllUIScript()
    {
        popupYN.gameObject.SetActive(true);
        
        popupYN.gameObject.SetActive(false);
    }

    public void OnClickDungeon_Griffon()
    {
        OpenPopupYN("Griffon Dungeon으로 배달을 시작하시겠습니까?",
            EnterDungeon_Griffon, ClosePopupYN);
    }

    public void EnterDungeon_Griffon()
    {
        ClosePopupYN();
        DataManager.Instance.SetDungeonName(Dungeon_Griffon.gameObject.name);
        GameManager.Instance.SceneStart(Constants.FightSceneName, true);
    }

    public void OpenPopupYN(string description, Action yesFunc, Action noFunc)
    {
        popupYN.gameObject.SetActive(true);
        popupYN.SetDescription(description);
        popupYN.SetCallback(yesFunc, noFunc);
    }

    public void ClosePopupYN()
    {
        popupYN.gameObject.SetActive(false);
    }
}
