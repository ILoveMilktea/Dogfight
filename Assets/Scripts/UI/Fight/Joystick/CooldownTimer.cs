using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownTimer : MonoBehaviour
{
    public Image skillImage;
    private Color defaultSkillImageColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);

    private bool skillOn;
    private float updateTime;
    private float cooldownTime;
    
    public bool IsSkillReady()
    {
        return skillOn;
    }
    public void SetCooldownTime(float time)
    {
        cooldownTime = time;
        updateTime = 0f;
    }

    public void SkillUse()
    {
        skillImage.color = defaultSkillImageColor;
        updateTime = 0f;
        skillOn = false;
    }

    public void StartFight()
    {
        updateTime = cooldownTime;
        skillOn = true;
        skillImage.color = Color.white;
        StartCoroutine(SkillTimer());
    }

    public IEnumerator SkillTimer()
    {
        while(FightSceneController.Instance.GetCurrentFightState() != FightState.End)
        {
            if(updateTime < cooldownTime)
            {
                updateTime += Time.deltaTime;
                skillImage.fillAmount = updateTime / cooldownTime;
                if (updateTime >= cooldownTime)
                {
                    skillImage.color = Color.white;
                    skillOn = true;
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
