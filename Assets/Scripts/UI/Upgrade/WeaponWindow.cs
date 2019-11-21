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
    public List<Button> skillButtons;
    [HideInInspector]
    public Button selectedSkill;
    private Dictionary<string, SkillTexts> skillDict;

    public Text parts;
    public GameObject skillTextWindow;
    public Text skillName;
    public Text skillDescription;

    public Button exit;
    public Button upgrade;

    private void Awake()
    {
        skillDict = new Dictionary<string, SkillTexts>();

        foreach (var weapon in weapons)
        {
            weapon.onClick.AddListener(OnClickWeaponButton);
        }
        
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
        foreach (var node in skillButtons)
        {
            string key = node.gameObject.name;
            switch (weaponImage.sprite.name)
            {
                // 1. 스킬트리 저장데이터 가져와서 적용해야함
                case "LinearGun":
                    LinearGunInfo LG_skill = Tables.Instance.LinearGun.GetTuple(key);
                    node.image.sprite = Resources.Load<Sprite>("Image/Skill/" + LG_skill.m_spriteName);

                    SkillTexts LG_skillTexts = new SkillTexts(LG_skill.m_skillName, LG_skill.m_description);
                    skillDict.Add(node.gameObject.name, LG_skillTexts);
                    break;
                case "ShotGun":
                    ShotGunInfo SG_skill = Tables.Instance.ShotGun.GetTuple(key);
                    node.image.sprite = Resources.Load<Sprite>("Image/Skill/" + SG_skill.m_spriteName);

                    SkillTexts SG_skillTexts = new SkillTexts(SG_skill.m_skillName, SG_skill.m_description);
                    skillDict.Add(node.gameObject.name, SG_skillTexts);
                    break;
                case "EnergySphereGun":
                    EnergySphereGunInfo ESG_skill = Tables.Instance.EnergySphereGun.GetTuple(key);
                    node.image.sprite = Resources.Load<Sprite>("Image/Skill/" + ESG_skill.m_spriteName);

                    SkillTexts ESG_skillTexts = new SkillTexts(ESG_skill.m_skillName, ESG_skill.m_description);
                    skillDict.Add(node.gameObject.name, ESG_skillTexts);
                    break;
            }
            //node.image = Resources.Load("Image/Skill/" + )

            node.onClick.AddListener(SetSkillDescription);
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
        upgrade.gameObject.SetActive(true);

        upgrade.onClick.RemoveAllListeners();
        //upgrade.onClick.AddListener()
    }

    private void CheckPartsForUpgrade()
    {

    }

    public void SetParts()
    {
        int value = DataManager.Instance.GetPlayInfo.parts;
        parts.text = value.ToString();
    }

    public void SetSkillName()
    {
        //string name = Tables.Instance.
    }
}
