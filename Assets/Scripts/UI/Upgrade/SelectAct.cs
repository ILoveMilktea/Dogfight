using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectAct: MonoBehaviour
{
    public Button rest;
    public Button eat;
    public Button training;
    public Button search;
    public Button retry;

    private void Awake()
    {
        rest.onClick.AddListener(OnClickRestButton);
        eat.onClick.AddListener(OnClickEatButton);
        training.onClick.AddListener(OnClickTrainingButton);
        search.onClick.AddListener(OnClickSearchButton);
        retry.onClick.AddListener(OnClickRetryButton);
    }

    public void OnClickRestButton()
    {
        UpgradeSceneController.Instance.OpenPopupYN(
            ConstDecriptions.Select_RestButton,
            ActRest,
            UpgradeSceneController.Instance.ClosePopupYN
            );
    }
    public void OnClickEatButton()
    {
        UpgradeSceneController.Instance.OpenPopupYN(
            ConstDecriptions.Select_EatButton,
            ActEat,
            UpgradeSceneController.Instance.ClosePopupYN
            );
    }
    public void OnClickTrainingButton()
    {
        UpgradeSceneController.Instance.OpenPopupYN(
            ConstDecriptions.Select_Training,
            ActTraining,
            UpgradeSceneController.Instance.ClosePopupYN
            );
    }
    public void OnClickSearchButton()
    {
        UpgradeSceneController.Instance.OpenPopupYN(
            ConstDecriptions.Select_Search,
            ActSearch,
            UpgradeSceneController.Instance.ClosePopupYN
            );
    }
    public void OnClickRetryButton()
    {
        UpgradeSceneController.Instance.OpenPopupYN(
            ConstDecriptions.Select_Retry,
            ActRetry,
            UpgradeSceneController.Instance.ClosePopupYN
            );
    }

    private void ActRest()
    {
        // 휴식, 체력증가 소, 체력회복 대
        DataManager.Instance.AddBuffHp(30);
        DataManager.Instance.AddRemainHp(10);

        UpgradeSceneController.Instance.OpenPopupResult("rest", UpgradeSceneController.Instance.ClosePopupResult);
    }

    private void ActEat()
    {
        // 식사, 체력증가 소, 체력회복 중, atk증가 소
        DataManager.Instance.AddBuffHp(10);
        DataManager.Instance.AddRemainHp(30);
        
        UpgradeSceneController.Instance.OpenPopupResult("eat", UpgradeSceneController.Instance.ClosePopupResult);
    }

    private void ActTraining()
    {
        // 단련, 체력증가 중, atk증가 소 || 체력증가 소, atk증가 중
        DataManager.Instance.AddBuffHp(10);
        DataManager.Instance.AddBuffAtk(5);
        
        UpgradeSceneController.Instance.OpenPopupResult("training", UpgradeSceneController.Instance.ClosePopupResult);
    }

    private void ActSearch()
    {
        UpgradeSceneController.Instance.OpenPopupResult("search", UpgradeSceneController.Instance.ClosePopupResult);
    }

    private void ActRetry()
    {
        UpgradeSceneController.Instance.OpenPopupResult("retry", UpgradeSceneController.Instance.ClosePopupResult);
    }
}
