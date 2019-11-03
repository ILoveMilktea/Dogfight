using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneController : MonoSingleton<StartSceneController>
{
    public Button newButton;
    public Button continueButton;

    private void Awake()
    {
        newButton.onClick.AddListener(OnClickNewButton);
        continueButton.onClick.AddListener(OnClickContinueButton);
    }
    // Start is called before the first frame update
    void Start()
    {
        if(!DataManager.Instance.CheckSaveData())
        {
            continueButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickNewButton()
    {
        GameManager.Instance.NewStartGame();
    }

    public void OnClickContinueButton()
    {
        GameManager.Instance.ContinueGame();
    }
}
