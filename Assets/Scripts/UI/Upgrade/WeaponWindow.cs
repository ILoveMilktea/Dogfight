﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillTexts
{
    public string name;
    public string description;

    public SkillTexts(string p_name, string p_description)
    {
        name = p_name;
        description = p_description;
    }
}

public class WeaponWindow : MonoBehaviour
{
    public List<Button> weapons;
    [HideInInspector]
    public Button selectedWeapon;

    public Image weaponImage;
    public List<SkillNode> skillButtons;
    public Dictionary<string, SkillNode> skillNodes;
    [HideInInspector]
    public Button selectedSkill;
    private Dictionary<string, SkillTexts> skillDict;

    public Text parts;
    public GameObject skillTextWindow;
    public Text skillName;
    public Text skillDescription;

    public Button exit;
    public Button upgrade;
    public Text upgradeButtonText;

    private void Awake()
    {
        skillDict = new Dictionary<string, SkillTexts>();

        foreach (var weapon in weapons)
        {
            weapon.onClick.AddListener(OnClickWeaponButton);
        }

        skillNodes = new Dictionary<string, SkillNode>();
        foreach(var button in skillButtons)
        {
            skillNodes.Add(button.gameObject.name, button);
        }

        SetParts();
        SetOnAwake();
    }

    private void Start()
    {
        SetSkillTree();
    }

    private void SetOnAwake()
    {
        selectedWeapon = weapons[0];
        selectedWeapon.interactable = false;

        skillTextWindow.SetActive(false);
        upgrade.gameObject.SetActive(false);
    }

    public void OnClickWeaponButton()
    {
        selectedWeapon.interactable = true;
        selectedWeapon = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        selectedWeapon.interactable = false;

        // replace background image
        weaponImage.sprite = Resources.Load<Sprite>("Image/Weapon/" + selectedWeapon.image.name);
        SetSkillTree();
    }

    public void ResetSkillWindow()
    {
        skillTextWindow.SetActive(false);
        upgrade.gameObject.SetActive(false);
    }

    public void SetSkillTree()
    {
        skillDict.Clear();

        // replace all nodes
        foreach (var node in skillNodes)
        {
            if(weaponImage.sprite.name == "LinearGun")
            {
                LinearGunInfo skill = Tables.Instance.LinearGun.GetTuple(node.Key);
                // skill image
                node.Value.button.image.sprite = Resources.Load<Sprite>("Image/Skill/" + skill.m_spriteName);
                // skill text
                SetSkillTexts(node.Key, skill.m_skillName, skill.m_description);
                // skill path
                if(node.Key != "0")
                {
                    SetSkillNode(skill.m_prevPath, node.Value);
                }
            }
            else if(weaponImage.sprite.name == "Shotgun")
            {
                ShotGunInfo skill = Tables.Instance.ShotGun.GetTuple(node.Key);
                node.Value.button.image.sprite = Resources.Load<Sprite>("Image/Skill/" + skill.m_spriteName);

                SetSkillTexts(node.Key, skill.m_skillName, skill.m_description);
                if (node.Key != "0")
                {
                    SetSkillNode(skill.m_prevPath, node.Value);
                }
            }
            else if(weaponImage.sprite.name == "EnergySphereGun")
            {
                EnergySphereGunInfo skill = Tables.Instance.EnergySphereGun.GetTuple(node.Key);
                node.Value.button.image.sprite = Resources.Load<Sprite>("Image/Skill/" + skill.m_spriteName);

                SetSkillTexts(node.Key, skill.m_skillName, skill.m_description);
                if (node.Key != "0")
                {
                    SetSkillNode(skill.m_prevPath, node.Value);
                }
            }
            else
            {
                Debug.Log("no weapon");
            }

            node.Value.button.onClick.AddListener(SetSkillDescription);
        }
    }

    private void SetSkillTexts(string key, string name, string description)
    {
        SkillTexts skillTexts = new SkillTexts(name, description);
        skillDict.Add(key, skillTexts);
    }

    private void SetSkillNode(string prevPath, SkillNode node)
    {
        WeaponType weaponType = (WeaponType)Enum.Parse(typeof(WeaponType), weaponImage.sprite.name);
        int prevNodeKey = int.Parse(prevPath);
        int curNodeKey = int.Parse(node.gameObject.name);

        if (DataManager.Instance.GetWeapons[weaponType].SkillTree[prevNodeKey].IsActivated)
        {
            if(DataManager.Instance.GetWeapons[weaponType].SkillTree[curNodeKey].IsActivated)
            {
                node.LightOn();
            }
            else
            {
                node.LightOff();
            }
        }
        else
        {
            node.LightOff();
        }
    }
    
    public void SetSkillDescription()
    {
        skillTextWindow.SetActive(true);

        selectedSkill = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        skillName.text = skillDict[selectedSkill.gameObject.name].name;
        skillDescription.text = skillDict[selectedSkill.gameObject.name].description;

        SetUpgradeButton();
    }

    public void SetUpgradeButton()
    {
        WeaponType weaponType = (WeaponType)Enum.Parse(typeof(WeaponType), weaponImage.sprite.name);
        int curNodeKey = int.Parse(selectedSkill.gameObject.name);

        if (DataManager.Instance.GetWeapons[weaponType].SkillTree[curNodeKey].IsActivated)
        {
            upgrade.gameObject.SetActive(false);
        }
        else
        {
            upgrade.gameObject.SetActive(true);
            upgradeButtonText.text = Tables.Instance.LinearGun.GetTuple(selectedSkill.gameObject.name).m_needParts.ToString();
        }

        upgrade.onClick.RemoveAllListeners();
        upgrade.onClick.AddListener(UpgradeWeapon);
    }

    private void UpgradeWeapon()
    {
        int need = int.Parse(upgradeButtonText.text);
        int have = int.Parse(parts.text);

        if(have > need)
        {
            WeaponType weaponType = (WeaponType)Enum.Parse(typeof(WeaponType), weaponImage.sprite.name);
            int curNodeKey = int.Parse(selectedSkill.gameObject.name);

            DataManager.Instance.AddParts(-need);
            DataManager.Instance.GetWeapons[weaponType].SkillTree[curNodeKey].SetIsActivated(true);
            skillNodes[selectedSkill.gameObject.name].LightOn();
        }
        else
        {
            //nooooooo
        }
    }
    
    public void SetParts()
    {
        int value = DataManager.Instance.GetPlayInfo.Parts;
        parts.text = value.ToString();
    }
}
