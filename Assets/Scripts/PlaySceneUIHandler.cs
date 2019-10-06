using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGroup
{
    // 여러 UI를 한번에 동작시키기 위한 class
    private List<GameObject> members = new List<GameObject>();

    public void SetMember(GameObject member)
    {
        if (!members.Contains(member))
        {
            members.Add(member);
        }
    }

    public void RemoveMember(GameObject member)
    {
        if (members.Contains(member))
        {
            members.Remove(member);
        }
    }

    public void ActiveAllMembers()
    {
        foreach (var member in members)
        {
            member.SetActive(true);
        }
    }

    public void InactiveAllMembers()
    {
        foreach (var member in members)
        {
            member.SetActive(false);
        }
    }
}

public class PlaySceneUIHandler : MonoBehaviour
{
    private Dictionary<GameObject, CharacterUI> characterUIs;
    private JoystickAttack joystickAttack;
    private JoystickMove joystickMove;

    public GameObject pauseImage;
    public Button pauseButton;
    public Text energy;

    private UIGroup fightGroup = new UIGroup();
    private UIGroup pauseGroup = new UIGroup();

    private void Awake()
    {
        characterUIs = new Dictionary<GameObject, CharacterUI>();
        joystickAttack = FindObjectOfType<JoystickAttack>();
        joystickMove = FindObjectOfType<JoystickMove>();

        pauseButton.onClick.AddListener(PauseGame);
    }

    void Start()
    {
        UIGrouping();
    }

    // UI Grouping
    private void UIGrouping()
    {
        // 전투 중 active한 UI들
        fightGroup.SetMember(joystickAttack.gameObject);
        fightGroup.SetMember(joystickMove.gameObject);

        // 일시정지 중 active한 UI들
        pauseGroup.SetMember(pauseImage.gameObject);


        GameManager.Instance.SetStateChangeCallback(PlayState.Pause, fightGroup.InactiveAllMembers);
        GameManager.Instance.SetStateChangeCallback(PlayState.Fight, fightGroup.ActiveAllMembers);

        GameManager.Instance.SetStateChangeCallback(PlayState.Pause, pauseGroup.ActiveAllMembers);
        GameManager.Instance.SetStateChangeCallback(PlayState.Fight, pauseGroup.InactiveAllMembers);
    }

    
    // pause
    public void PauseGame()
    {
        GameManager.Instance.ChangePlayState();
    }
    // hp
    public void DamageToCharacter(GameObject character, int value)
    {

    }
    public void HealCharacter(GameObject character, int value)
    { }
    public void OffCharacterUI(GameObject character) // 죽었을 때
    { }
}
