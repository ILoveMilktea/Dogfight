using System;
using System.Linq;
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

public class FightSceneController : MonoSingleton<FightSceneController>
{
    private FightStatus fightStatus;
    private PlayTimer playTimer;

    private FightStateObserver fightStateObserver;
    private FightScheduler fightScheduler;
    private Player player;

    // UI 관련
    private PlayerCharacterUI playerCharacterUI;
    private Dictionary<GameObject, CharacterUI> enemyCharacterUIs;
    private JoystickAttack joystickAttack;
    private JoystickMove joystickMove;

    public ResultWindow resultWindow;

    public Transform characterUIGroup;
    public GameObject standbyImage;
    public GameObject pauseImage;
    public GameObject loadingImage;
    public Button pauseButton;
    public Text parts;


    private UIGroup fightGroup = new UIGroup();
    private UIGroup pauseGroup = new UIGroup();

    private void Awake()
    {
        playTimer = FindObjectOfType<PlayTimer>();
        
        fightStateObserver = FindObjectOfType<FightStateObserver>();
        fightScheduler = FindObjectOfType<FightScheduler>();
        player = FindObjectOfType<Player>();

        enemyCharacterUIs = new Dictionary<GameObject, CharacterUI>();
        joystickAttack = FindObjectOfType<JoystickAttack>();
        joystickMove = FindObjectOfType<JoystickMove>();

        resultWindow = FindObjectOfType<ResultWindow>();
        pauseButton.onClick.AddListener(OnClickPauseButton);
    }

    void Start()
    {
        StartFightScene();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            GameObject[] enemies = fightStatus.enemies.Keys.ToArray();
            foreach(var enemy in enemies)
            {
                EnemyDead(enemy); 
            }
        }
    }

    public void StartFightScene()
    {
        loadingImage.SetActive(true);
        StartCoroutine(UIEffect.FadeIn(loadingImage.GetComponent<Image>()));

        SetTimer();
        SetStage();
        fightScheduler.StageStart();
    }
    private void SetStage()
    {
        fightStatus = new FightStatus();

        SetPlayerOnStage();
        SetEnemyOnStage();
        SetUIToAllCharacters();
        UIGrouping();
    }
    private void SetTimer()
    {
        playTimer.StartTimer();

        SetStateChangeCallback(FightState.Standby, playTimer.FreezeTimer);
        SetStateChangeCallback(FightState.Fight, playTimer.ReleaseTimer);
        SetStateChangeCallback(FightState.Pause, playTimer.FreezeTimer);
        SetStateChangeCallback(FightState.Standby, playTimer.FreezeTimer);
    }
    // player status 등록
    private void SetPlayerOnStage()
    {
        PlayerStatusInfo statusFromData = DataManager.Instance.GetPlayerStatus;
        CharacterStatus status = new CharacterStatus("Player", statusFromData.maxHp, statusFromData.remainHp, statusFromData.atk);

        fightStatus.SetPlayerStatus(status);
    }
    // stage에 해당하는 몹 생성, 위치지정
    private void SetEnemyOnStage()
    {
        int stageNumber = DataManager.Instance.GetPlayInfo.stage;
        int enemyCount = 0;

        StageEnemyTable stageTable = Tables.Instance.StageEnemyTables[stageNumber];
        foreach(var stageEnemyInfo in stageTable.GetTable())
        {
            EnemyStatusInfo enemyInfo = Tables.Instance.EnemyStatus.GetTuple(stageEnemyInfo.Value.m_serialNumber);
            GameObject enemy = ObjectPoolManager.Instance.Get(enemyInfo.m_name);
            //GameObject enemy = Instantiate(Resources.Load("Prefab/Enemy/" + enemyInfo.m_name)) as GameObject;
            enemy.SetActive(true);
            EnemyCharacterStatus status = new EnemyCharacterStatus(enemyInfo.m_name, enemyInfo.m_hp, enemyInfo.m_hp, enemyInfo.m_atk, enemyInfo.m_dropParts);

            fightStatus.AddEnemyInstance(enemy, status);
            enemy.transform.position = new Vector3(stageEnemyInfo.Value.m_posX, 0, stageEnemyInfo.Value.m_posY);
            enemyCount++;
        }

        fightStatus.SetRemainEnemy(enemyCount);
    }
    // 모든 character(player, enemy) hp바 붙이기
    private void SetUIToAllCharacters()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject playerCharacterUIprefab = Instantiate(Resources.Load("Prefab/UI/PlayerCharacterUI"), characterUIGroup) as GameObject;
        playerCharacterUI = playerCharacterUIprefab.GetComponent<PlayerCharacterUI>();

        playerCharacterUI.SetName(fightStatus.playerStatus.name);
        playerCharacterUI.SetMaxHp(fightStatus.playerStatus.maxHp);
        playerCharacterUI.SetTarget(player);
        playerCharacterUI.ResizeUI();

        GameObject[] enemies = fightStatus.enemies.Keys.ToArray();
        foreach(var enemy in enemies)
        {
            GameObject EnemyCharacterUIprefab = Instantiate(Resources.Load("Prefab/UI/EnemyCharacterUI"), characterUIGroup) as GameObject;
            enemyCharacterUIs.Add(enemy, EnemyCharacterUIprefab.GetComponent<CharacterUI>());

            enemyCharacterUIs[enemy].SetName(fightStatus.enemies[enemy].name);
            enemyCharacterUIs[enemy].SetMaxHp(fightStatus.enemies[enemy].maxHp);
            enemyCharacterUIs[enemy].SetTarget(enemy);
            enemyCharacterUIs[enemy].ResizeUI();

        }
    }
    // 일시 정지할때 표시 안할 ui grouping
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


    // pause
    public void OnClickPauseButton()
    {
        if (fightStateObserver.curFightState == FightState.Pause)
        {
            fightStateObserver.SetCurrentFightState(FightState.Fight);
        }
        else if(fightStateObserver.curFightState == FightState.Fight)
        {
            fightStateObserver.SetCurrentFightState(FightState.Pause);
        }
    }

    public void ChangeFightState(FightState state)
    {
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
        if (fightStateObserver.curFightState == FightState.Fight)
        {
            player.Move(dir, amount);
        }
    }
    // 플레이어 멈춤
    public void StopPlayer()
    {
        if (fightStateObserver.curFightState == FightState.Fight)
        {
            player.StopMove();
        }
    }
    // 플레이어 공격
    public void PlayerAttack(Vector3 dir)
    {
        if (fightStateObserver.curFightState == FightState.Fight)
        {
            player.Attack(dir);
        }
    }
    // 플레이어 공격 대기
    public void PlayerStandby()
    {
        if (fightStateObserver.curFightState == FightState.Fight)
        {
            player.Standby();
        }
    }
    // 무기 변경
    public void SwapWeapon()
    {
        WeaponType weapon = player.SwapWeapon();
        joystickAttack.WeaponImageSwap(weapon);
    }

    // hp
    public void DamageToCharacter(GameObject character, int damage)
    {
        if(character == player.gameObject)
        {
            playerCharacterUI.HpDown(damage);
            bool isCharacterDead = fightStatus.playerStatus.DamageToCharacter(damage);
            if(isCharacterDead)
            {
                PlayerDead();
            }
        }
        else if(fightStatus.enemies.ContainsKey(character))
        {
            if(enemyCharacterUIs.ContainsKey(character))
            {
                enemyCharacterUIs[character].HpDown(damage);
            }
            bool isCharacterDead = fightStatus.enemies[character].DamageToCharacter(damage);
            if (isCharacterDead)
            {
                EnemyDead(character);
            }
        }
    }
    public void DamageToCharacter(GameObject source, GameObject target)
    {
        if (target == player.gameObject)
        {
            int damage = fightStatus.enemies[source].atk;

            playerCharacterUI.HpDown(damage);
            bool isCharacterDead = fightStatus.playerStatus.DamageToCharacter(damage);
            if (isCharacterDead)
            {
                PlayerDead();
            }
        }
        else if (fightStatus.enemies.ContainsKey(target))
        {
            int damage = fightStatus.playerStatus.atk;

            if (enemyCharacterUIs.ContainsKey(target))
            {
                enemyCharacterUIs[target].HpDown(damage);
            }
            bool isCharacterDead = fightStatus.enemies[target].DamageToCharacter(damage);
            if (isCharacterDead)
            {
                EnemyDead(target);
            }
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
        // gain parts
        int dropParts = fightStatus.enemies[enemy].dropParts;
        fightStatus.AddParts(dropParts);
        parts.text = fightStatus.gainParts.ToString();

        // enemy ui, character offf
        fightStatus.RemoveEnemyInstance(enemy);
        enemyCharacterUIs[enemy].gameObject.SetActive(false);
        enemyCharacterUIs.Remove(enemy);

        enemy.SetActive(false);
    }

    public int RemainEnemyNumber()
    {
        return fightStatus.remainEnemy;
    }

    public void ShowResult()
    {
        ChangeFightState(FightState.Standby);
        playTimer.StopTimer();
        DataManager.Instance.Save();

        //resultWindow.ShowResult(EndFight);
        EndFight();
    }

    public void EndFight()
    {
        // save playtime
        DataManager.Instance.AddPlayTime(playTimer.GetPlaytime());
        // save gain parts
        DataManager.Instance.AddGainParts(fightStatus.gainParts);
        // save player reaminHp
        DataManager.Instance.SetRemainHp(fightStatus.playerStatus.remainHp);

        StartCoroutine(UIEffect.FadeOut(loadingImage.GetComponent<Image>()));
        GameManager.Instance.SceneStart(Constants.UpgradeSceneName);
    }
}
