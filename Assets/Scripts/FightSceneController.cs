using System;
using System.Reflection;
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

public class CharacterStatus
{
    public int maxHp { get; private set; }
    public int remainHp { get; private set; }
    public int atk { get; private set; }

    public void SetCharacterStatus(int p_maxHp, int p_remainHp, int p_Atk)
    {
        maxHp = p_maxHp;
        remainHp = p_remainHp;
        atk = p_Atk;
    }
}

public class FightStatus
{
    public int remainEnemy { get; private set; }
    public int gainParts { get; private set; }

    public CharacterStatus player { get; private set; }
    public Dictionary<GameObject, CharacterStatus> enemies { get; private set; }
}

public class FightSceneController : MonoSingleton<FightSceneController>
{
    private FightStatus fightStatus;
    private PlayTimer playTimer;

    private FightStateObserver fightStateObserver;
    private Player player;
    private FightState fightState = FightState.Fight;
    public FightState CurrentFightState { get { return fightState; } }

    // UI 관련
    private PlayerCharacterUI playerCharacterUI;
    private Dictionary<GameObject, CharacterUI> enemyCharacterUIs;
    private JoystickAttack joystickAttack;
    private JoystickMove joystickMove;

    public Transform characterUIGroup;
    public GameObject pauseImage;
    public Button pauseButton;
    public Text energy;

    private UIGroup fightGroup = new UIGroup();
    private UIGroup pauseGroup = new UIGroup();

    private void Awake()
    {
        
        fightStateObserver = FindObjectOfType<FightStateObserver>();
        player = FindObjectOfType<Player>();

        enemyCharacterUIs = new Dictionary<GameObject, CharacterUI>();
        joystickAttack = FindObjectOfType<JoystickAttack>();
        joystickMove = FindObjectOfType<JoystickMove>();

        pauseButton.onClick.AddListener(PauseGame);
    }

    void Start()
    {
        //StartTimer();
        SetUIToAllCharacters();
        UIGrouping();
        SetEnemyOnStage(); // test
    }

    public void StartFightScene()
    {
        SetStage();
        StartTimer();
    }
    private void SetStage()
    {
        SetPlayerOnStage();
        SetEnemyOnStage();
        SetUIToAllCharacters();
        UIGrouping();
    }
    private void SetPlayerOnStage()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerStatusInfo status = DataCenter.Instance.GetPlayerStatus;

        fightStatus.player.SetCharacterStatus(status.maxHp, status.remainHp, status.atk);

    }
    private void SetEnemyOnStage()
    {
        int stageNumber = DataCenter.Instance.GetPlayInfo.stage;

        StageEnemyTable table = Tables.Instance.StageEnemyTables[stageNumber];
        foreach(var tuple in table.GetTable())
        {
            Debug.Log(tuple.Value.m_serialNumber + " --- " + EnemyStatusTable.GetTuple(tuple.Value.m_serialNumber).m_name);
        }
    }
    private void SetUIToAllCharacters()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject playerCharacterUIprefab = Instantiate(Resources.Load("Prefab/UI/PlayerCharacterUI"), characterUIGroup) as GameObject;
        playerCharacterUI = playerCharacterUIprefab.GetComponent<PlayerCharacterUI>();
        playerCharacterUI.SetTarget(player);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var enemy in enemies)
        {
            GameObject EnemyCharacterUIprefab = Instantiate(Resources.Load("Prefab/UI/EnemyCharacterUI"), characterUIGroup) as GameObject;
            enemyCharacterUIs.Add(enemy, EnemyCharacterUIprefab.GetComponent<CharacterUI>());
            enemyCharacterUIs[enemy].SetTarget(enemy);
        }
    }
    private void UIGrouping()
    {
        // 전투 중 active한 UI들
        fightGroup.SetMember(joystickAttack.gameObject);
        fightGroup.SetMember(joystickMove.gameObject);

        // 일시정지 중 active한 UI들
        pauseGroup.SetMember(pauseImage.gameObject);


        SetStateChangeCallback(FightState.Pause, fightGroup.InactiveAllMembers);
        SetStateChangeCallback(FightState.Fight, fightGroup.ActiveAllMembers);

        SetStateChangeCallback(FightState.Pause, pauseGroup.ActiveAllMembers);
        SetStateChangeCallback(FightState.Fight, pauseGroup.InactiveAllMembers);
    }

    private void StartTimer()
    {
        SetStateChangeCallback(FightState.Pause, playTimer.FreezeTimer);
        SetStateChangeCallback(FightState.Dead, playTimer.FreezeTimer);
        SetStateChangeCallback(FightState.Fight, playTimer.ReleaseTimer);
    }

    // pause
    public void PauseGame()
    {
        if (fightState == FightState.Pause)
        {
            ChangePlayState(FightState.Fight);
        }
        else if(fightState == FightState.Fight)
        {
            ChangePlayState(FightState.Pause);
        }
    }


    // play state change
    public void ChangePlayState(FightState state)
    {
        fightState = state;
        fightStateObserver.SetCurrentFightState(state);
    }

    // 전투 중 일시정지시에 처리할 콜백함수들 등록
    public void SetStateChangeCallback(FightState fightState, Action func)
    {
        fightStateObserver.SetStateChangeCallback(fightState, func);
    }
    public void RemoveStateChangeCallback(FightState fightState, Action func)
    {
        fightStateObserver.RemoveStateChangeCallback(fightState, func);
    }


    // player control
    // 플레이어 이동
    public void MovePlayer(Vector3 dir, float amount)
    {
        if (fightState == FightState.Fight)
        {
            player.Move(dir, amount);
        }
    }
    // 플레이어 멈춤
    public void StopPlayer()
    {
        if (fightState == FightState.Fight)
        {
            player.StopMove();
        }
    }
    // 플레이어 공격
    public void PlayerAttack(Vector3 dir)
    {
        if (fightState == FightState.Fight)
        {
            player.Attack(dir);
        }
    }
    // 플레이어 공격 대기
    public void PlayerStandby()
    {
        if (fightState == FightState.Fight)
        {
            player.Standby();
        }
    }

    // hp
    public void DamageToCharacter(GameObject character, int value)
    {
        if(character.tag == "Player")
        {
            playerCharacterUI.HpDown(value);
        }
        else if(character.tag == "Enemy")
        {
            if(enemyCharacterUIs.ContainsKey(character))
            {
                enemyCharacterUIs[character].HpDown(value);
            }
        }
    }
    public void DamageToCharacter(GameObject source, GameObject target)
    {
        if(target.tag == Constants.PlayerTag)
        {
            int damage = 0;
            playerCharacterUI.HpDown(damage);
        }
        else if(target.tag == Constants.EnemyTag)
        {
            int damage = 0;
            enemyCharacterUIs[target].HpDown(damage);
        }
    }

    public void HealToCharacter(GameObject character, int value)
    {
        if (character.tag == "Player")
        {
            playerCharacterUI.HpUp(value);
        }
        else if (character.tag == "Enemy")
        {
            if (enemyCharacterUIs.ContainsKey(character))
            {
                enemyCharacterUIs[character].HpUp(value);
            }
        }
    }
    public void OffCharacterUI(GameObject character) // 죽었을 때
    {
        if (character.tag == "Player")
        {
            playerCharacterUI.gameObject.SetActive(false);
        }
        else if (character.tag == "Enemy")
        {
            if (enemyCharacterUIs.ContainsKey(character))
            {
                enemyCharacterUIs[character].gameObject.SetActive(false);
            }
        }
    }
    
    public void PlayerDead()
    {
        // 게임정지, 
    }
    public void EnemyDead(GameObject enemy)
    {

    }

    public int RemainEnemyNumber()
    {
        return fightStatus.remainEnemy;
    }

    private void OnDestroy()
    {
        //playTimer.StopTimer();
    }
}
