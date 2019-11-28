using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ConstDecriptions
{
    public const string FightButton = "다음?";

    public const string Select_RestButton = "휴식?"; // "Fight"
    public const string Select_EatButton = "식사?";
    public const string Select_Training = "단련?";
    public const string Select_Search = "탐색?";
    public const string Select_Retry = "재전투?";
}

public class UpgradeSceneController : MonoSingleton<UpgradeSceneController>
{
    // Inactive on awake
    public PlayerStatusWindow statusWindow;
    public SelectAct selectAct;
    public WeaponWindow weaponWindow;
    public ExitButton exitButton;
    public PopupYN popupYN;
    public PopupResult popupResult;

    public GameObject loadingImage;
    public Button actButton;
    public Button upgradeButton;
    public Button fightButton;

    protected override void Init()
    {
        actButton.onClick.AddListener(OnClickActButton);
        upgradeButton.onClick.AddListener(OnClickUpgradeButton);
        fightButton.onClick.AddListener(OnClickFightButton);

        AwakeAllUIScript();
    }

    void Start()
    {
        StartUpgradeScene();
    }

    public void StartUpgradeScene()
    {
        StartCoroutine(UIEffect.AlphaOut(loadingImage.GetComponent<Image>()));

        // 시작시 할 명령들?
        if (DataManager.Instance.GetPlayInfo.AlreadyAct)
        {
            actButton.interactable = false;
        }

        DataManager.Instance.Save();
    }

    private void AwakeAllUIScript()
    {
        selectAct.gameObject.SetActive(true);
        weaponWindow.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        popupYN.gameObject.SetActive(true);
        popupResult.gameObject.SetActive(true);

        selectAct.gameObject.SetActive(false);
        weaponWindow.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        popupYN.gameObject.SetActive(false);
        popupResult.gameObject.SetActive(false);
    }
    //private void UIGrouping()
    //{
    //    // 전투 중 active한 UI들
    //    fightGroup.SetMember(joystickAttack.gameObject);
    //    fightGroup.SetMember(joystickMove.gameObject);

    //    // 일시정지 중 active한 UI들
    //    pauseGroup.SetMember(pauseImage.gameObject);


    //    SetStateChangeCallback(FightState.Pause, fightGroup.InactiveAllMembers);
    //    SetStateChangeCallback(FightState.Fight, fightGroup.ActiveAllMembers);

    //    SetStateChangeCallback(FightState.Pause, pauseGroup.ActiveAllMembers);
    //    SetStateChangeCallback(FightState.Fight, pauseGroup.InactiveAllMembers);
    //}

    public void RedrawStatusWindow()
    {
        statusWindow.RedrawWindow();
    }
    
    public void ActiveExitButton(GameObject targetUI)
    {
        if(!exitButton.gameObject.activeSelf)
        {
            exitButton.gameObject.SetActive(true);
        }
        exitButton.PushUI(targetUI);
    }
    public void OpenPopupYN(string description, Action yesFunc, Action noFunc)
    {
        popupYN.gameObject.SetActive(true);
        popupYN.SetDescription(description);
        popupYN.SetCallback(yesFunc, noFunc);

        //ActiveExitButton(popupYN.gameObject);
    }

    public void OpenPopupResult(string description, Action okFunc)
    {
        ClosePopupYN();
        CloseSelectActWindow();
        DataManager.Instance.SetAlreadyAct(true);
        DataManager.Instance.Save();

        popupResult.gameObject.SetActive(true);
        popupResult.SetDescription(description);
        popupResult.SetCallback(okFunc);
    }

    public void OnClickActButton()
    {
        selectAct.gameObject.SetActive(true);
        
        ActiveExitButton(selectAct.gameObject);
    }

    public void CloseSelectActWindow()
    {
        exitButton.ClearStack();
        actButton.interactable = false;
    }
    public void CloseSelectWeaponWindow()
    {
        weaponWindow.gameObject.SetActive(false);
    }


    public void OnClickUpgradeButton()
    {
        weaponWindow.gameObject.SetActive(true);
    }

    public void OnClickFightButton()
    {
        OpenPopupYN(ConstDecriptions.FightButton, EndUpgrade, ClosePopupYN);
    }

    public void EndUpgrade()
    {
        DataManager.Instance.Save();
        DataManager.Instance.SetAlreadyAct(false);
        DataManager.Instance.SetStage(DataManager.Instance.GetPlayInfo.Stage + 1);

        popupYN.gameObject.SetActive(false);
        StartCoroutine(UIEffect.AlphaIn(loadingImage.GetComponent<Image>()));
        GameManager.Instance.SceneStart(Constants.FightSceneName);
    }

    public void ClosePopupYN()
    {
        //exitButton.PullUI();
        popupYN.gameObject.SetActive(false);
    }
    
    public void ClosePopupResult()
    {
        popupResult.gameObject.SetActive(false);
    }
}
