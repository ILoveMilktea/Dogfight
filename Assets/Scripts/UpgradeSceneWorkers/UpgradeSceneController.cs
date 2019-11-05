using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSceneController : MonoSingleton<UpgradeSceneController>
{
    private SelectAction selectAction;
    private UpgradeWeapon upgradeWeapon;
    public PopupYN popupYN;

    public GameObject loadingImage;
    public Button fightButton;

    private void Awake()
    {
        selectAction = FindObjectOfType<SelectAction>();
        upgradeWeapon = FindObjectOfType<UpgradeWeapon>();

        fightButton.onClick.AddListener(OnClickFightButton);
    }

    void Start()
    {
        StartUpgradeScene();
    }

    public void StartUpgradeScene()
    {
        // 시작시 할 명령들?
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


    // hp
    //public void DamageToCharacter(GameObject character, int value)
    //{
    //    if (character.tag == "Player")
    //    {
    //        playerCharacterUI.HpDown(value);
    //    }
    //    else if (character.tag == "Enemy")
    //    {
    //        if (enemyCharacterUIs.ContainsKey(character))
    //        {
    //            enemyCharacterUIs[character].HpDown(value);
    //        }
    //    }
    //}
    //public void DamageToCharacter(GameObject source, GameObject target)
    //{
    //    if (target.tag == Constants.PlayerTag)
    //    {
    //        int damage = 0;
    //        playerCharacterUI.HpDown(damage);
    //    }
    //    else if (target.tag == Constants.EnemyTag)
    //    {
    //        int damage = 0;
    //        enemyCharacterUIs[target].HpDown(damage);
    //    }
    //}

    //public void HealToCharacter(GameObject character, int value)
    //{
    //    if (character.tag == "Player")
    //    {
    //        playerCharacterUI.HpUp(value);
    //    }
    //    else if (character.tag == "Enemy")
    //    {
    //        if (enemyCharacterUIs.ContainsKey(character))
    //        {
    //            enemyCharacterUIs[character].HpUp(value);
    //        }
    //    }
    //}


    public void OnClickFightButton()
    {
        popupYN.gameObject.SetActive(true);
        popupYN.SetCallback(EndUpgrade, ClosePopupYN);
    }

    public void EndUpgrade()
    {
        DataCenter.Instance.SetStage(DataCenter.Instance.GetPlayInfo.stage + 1);

        popupYN.gameObject.SetActive(false);
        UIEffect.FadeIn(loadingImage.GetComponent<Image>());
        GameManager.Instance.FightSceneStart();
    }

    public void ClosePopupYN()
    {
        popupYN.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        //playTimer.StopTimer();
    }
}
