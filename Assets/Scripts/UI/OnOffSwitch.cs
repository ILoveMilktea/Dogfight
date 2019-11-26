using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGroup
{
    // 여러 UI를 한번에 동작시키기 위한 class
    private List<GameObject> members = new List<GameObject>();

    public void SetMember(GameObject member)
    {
        members.Add(member);
    }

    public void ActiveAllMembers()
    {
        foreach(var member in members)
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

public class OnOffSwitch : MonoSingleton<OnOffSwitch>
{
    // UI 끄고 키기
    private UIGroup fightUIGroup = new UIGroup();

    private void Start()
    {
        GameManager.Instance.SetStateChangeCallback(PlayState.Pause, fightUIGroup.InactiveAllMembers);
        GameManager.Instance.SetStateChangeCallback(PlayState.Fight, fightUIGroup.ActiveAllMembers);
    }
    private void Update()
    {
        
    }

    public void AddFightUIGroupMember(GameObject member)
    {
        fightUIGroup.SetMember(member);
    }
}
