using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownTimer : MonoBehaviour
{
    public Image skillImage;

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
    }

    public void SkillUse()
    {
        updateTime = 0f;
        skillOn = false;
    }

    public void StartFight()
    {
        updateTime = cooldownTime;
        skillOn = true;
        StartCoroutine(SkillTimer());
    }

    public IEnumerator SkillTimer()
    {
        while(true)
        {
            if(updateTime < cooldownTime)
            {
                updateTime += Time.deltaTime;
                skillImage.fillAmount = updateTime / cooldownTime;
                if (updateTime >= cooldownTime)
                {
                    skillOn = true;
                }
            }
        }
    }
}
